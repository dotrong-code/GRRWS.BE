using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.ErrorDetail;

namespace GRRWS.Application.Implement.Service
{
    public class ErrorDetailService : IErrorDetailService
    {
        private readonly UnitOfWork _unitOfWork;

        public ErrorDetailService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> CreateErrorDetailAsync(CreateErrorDetailRequest request)
        {
            // Validate Request exists
            var requestExists = await _unitOfWork.RequestRepository.GetByIdAsync(request.RequestId) != null;
            if (!requestExists)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request not found"));

            // Check if ErrorId list is null or empty
            if (request.ErrorId == null || !request.ErrorId.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("Bad Request", "At least one error must be specified"));

            // Get ReportId from RequestId
            var reportId = await _unitOfWork.ErrorDetailRepository.GetReportIdByRequestIdAsync(request.RequestId);
            if (reportId == Guid.Empty)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Report not found for this request"));

            // Create a list to hold all the new error details
            var newErrorDetails = new List<ErrorDetail>();
            var skippedErrors = new List<Guid>();
            var processedDetails = new List<object>();

            // Process each error ID
            foreach (var errorId in request.ErrorId)
            {
                // Validate Error exists
                var error = await _unitOfWork.ErrorRepository.GetByIdAsync(errorId);
                if (error == null)
                {
                    skippedErrors.Add(errorId);
                    continue;
                }

                // Check if ErrorDetail already exists for this Error and Report
                var exists = await _unitOfWork.ErrorDetailRepository.ErrorDetailExistsAsync(errorId, reportId);
                if (exists)
                {
                    skippedErrors.Add(errorId);
                    continue;
                }

                // Create new ErrorDetail
                var errorDetail = new ErrorDetail
                {
                    ErrorId = errorId,
                    ReportId = reportId
                };

                // Get the first available ErrorGuideline for this error
                var guideline = await _unitOfWork.ErrorGuidelineRepository.GetFirstByErrorIdAsync(errorId);

                if (guideline != null)
                {
                    // Assign the guideline to the error detail
                    errorDetail.ErrorGuideLineId = guideline.Id;

                    // Create progress records for each step in the guideline
                    var errorFixSteps = await _unitOfWork.ErrorFixStepRepository.GetByErrorGuidelineIdAsync(guideline.Id);
                    if (errorFixSteps != null && errorFixSteps.Any())
                    {
                        errorDetail.ProgressRecords = errorFixSteps.Select(step => new ErrorFixProgress
                        {
                            ErrorFixStepId = step.Id,
                            IsCompleted = false
                        }).ToList();
                    }

                    // Create spare part usage request if the guideline has spare parts
                    var errorSpareparts = await _unitOfWork.ErrorSparepartRepository.GetByErrorGuidelineIdAsync(guideline.Id);
                    if (errorSpareparts != null && errorSpareparts.Any())
                    {
                        var requestCode = $"REQ-{TimeHelper.GetHoChiMinhTime().Ticks}-{Guid.NewGuid().ToString().Substring(0, 8)}";
                        var newRequest = new RequestTakeSparePartUsage
                        {
                            RequestCode = requestCode,
                            RequestDate = TimeHelper.GetHoChiMinhTime(),
                            Status = Domain.Enum.SparePartRequestStatus.Unconfirmed,
                            SparePartUsages = errorSpareparts.Select(sp => new SparePartUsage
                            {
                                SparePartId = sp.SparepartId,
                                QuantityUsed = sp.QuantityNeeded ?? 0,
                                IsTakenFromStock = false
                            }).ToList()
                        };

                        await _unitOfWork.RequestTakeSparePartUsageRepository.CreateAsync(newRequest);
                        errorDetail.RequestTakeSparePartUsageId = newRequest.Id;
                    }
                }

                newErrorDetails.Add(errorDetail);
                processedDetails.Add(new
                {
                    ErrorId = errorId,
                    HasGuideline = guideline != null,
                    GuidelineId = guideline?.Id
                });
            }

            // If no valid error details could be created
            if (!newErrorDetails.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Bad Request",
                    "Could not create any error details. All errors either don't exist or already have error details."));

            // Add all new error details in one batch
            await _unitOfWork.ErrorDetailRepository.CreateRangeAsync(newErrorDetails);
            await _unitOfWork.SaveChangesAsync();

            // Create repair task group and auto-assign mechanics
            var taskGroupResult = await CreateRepairTaskGroupAsync(reportId, request.RequestId);
            if (taskGroupResult.IsSuccess && taskGroupResult.Object != null)
            {
                var taskGroupId = (Guid)taskGroupResult.Object;
                await AutoAssignedTaskAsync(taskGroupId);
            }

            // Return success with information about created and skipped errors
            return Result.SuccessWithObject(new
            {
                Message = "ErrorDetails created successfully!",
                CreatedCount = newErrorDetails.Count,
                SkippedCount = skippedErrors.Count,
                SkippedErrorIds = skippedErrors,
                ProcessedDetails = processedDetails,
                TaskGroupId = taskGroupResult.IsSuccess ? taskGroupResult.Object : null
            });
        }

        private async Task<Result> CreateRepairTaskGroupAsync(Guid reportId, Guid requestId)
        {
            try
            {
                // Get system user for task creation
                var users = await _unitOfWork.UserRepository.GetUsersByRole(2); // System admin role
                var systemUserId = users?.FirstOrDefault()?.Id ?? Guid.Parse("43333333-3333-3333-3333-333333333333");

                // Get report and request information
                var report = await _unitOfWork.ReportRepository.GetByIdAsync(reportId);
                if (report == null)
                {
                    throw new InvalidOperationException("Report not found");
                }

                // Get request to verify it exists
                var request = await _unitOfWork.RequestRepository.GetRequestByIdAsync(requestId);
                if (request == null)
                {
                    throw new InvalidOperationException("Request not found");
                }

                // Create a new task group for repair
                var taskGroup = new TaskGroup
                {
                    Id = Guid.NewGuid(),
                    ReportId = reportId,
                    GroupType = TaskType.Repair,
                    GroupName = $"Repair - {report.Location}"
                };

                await _unitOfWork.TaskGroupRepository.CreateAsync(taskGroup);

                // Create repair task
                var repairRequest = new Infrastructure.DTOs.Task.Repair.CreateRepairTaskRequest
                {
                    RequestId = requestId,
                    AssigneeId = null // Will be assigned by AutoAssignedTask
                };

                await _unitOfWork.TaskRepository.CreateRepairTaskWithGroup(repairRequest, systemUserId, taskGroup.Id, 1);
                await _unitOfWork.SaveChangesAsync();

                return Result.SuccessWithObject(taskGroup.Id);
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Error", $"Failed to create repair task group: {ex.Message}"));
            }
        }

        private async Task AutoAssignedTaskAsync(Guid taskGroupId)
        {
            try
            {
                // Get all tasks in the task group
                var tasks = await _unitOfWork.TaskRepository.GetTasksByGroupIdAsync(taskGroupId);

                if (!tasks.Any())
                {
                    throw new InvalidOperationException("No tasks found in the task group");
                }

                // Get available mechanics using the existing recommendation system
                var currentTime = TimeHelper.GetHoChiMinhTime();
                var availableMechanics = await _unitOfWork.UserRepository.GetRecommendedMechanicsAsync(currentTime, 1, 10);

                if (!availableMechanics.Any())
                {
                    throw new InvalidOperationException("No available mechanics for auto-assignment");
                }

                // Select the best available mechanic
                var bestMechanic = availableMechanics.First();
                var mechanicId = bestMechanic.MechanicId;

                foreach (var task in tasks)
                {
                    if (!task.AssigneeId.HasValue)
                    {
                        task.AssigneeId = mechanicId;
                        task.Status = Status.Pending;
                        task.ModifiedDate = TimeHelper.GetHoChiMinhTime();
                        task.ExpectedTime = bestMechanic.ExpectedTime;

                        await _unitOfWork.TaskRepository.UpdateAsync(task);

                        // Create mechanic shift for the task
                        await _unitOfWork.MechanicShiftRepository.CreateAsync(new MechanicShift
                        {
                            MechanicId = mechanicId,
                            TaskId = task.Id,
                            StartTime = TimeHelper.GetHoChiMinhTime(),
                            EndTime = TimeHelper.GetHoChiMinhTime().AddHours(2), // Estimated 2 hours
                            IsAvailable = false
                        });
                    }
                }

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to auto-assign tasks: {ex.Message}", ex);
            }
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            var errorDetail = await _unitOfWork.ErrorDetailRepository.GetByIdWithDetailsAsync(id);
            if (errorDetail == null || errorDetail.IsDeleted)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "ErrorDetail not found"));

            var resultDto = new
            {
                Id = errorDetail.Id,
                ReportId = errorDetail.ReportId,
                ErrorId = errorDetail.ErrorId,
                ErrorGuideLineId = errorDetail.ErrorGuideLineId,
                TaskId = errorDetail.TaskId,
                RequestTakeSparePartUsage = new
                {
                    Id = errorDetail.RequestTakeSparePartUsage?.Id,
                    RequestCode = errorDetail.RequestTakeSparePartUsage?.RequestCode,
                    Status = errorDetail.RequestTakeSparePartUsage?.Status,
                    SparePartUsages = errorDetail.RequestTakeSparePartUsage?.SparePartUsages.Select(spu => new
                    {
                        Id = spu.Id,
                        SparePartId = spu.SparePartId,
                        QuantityUsed = spu.QuantityUsed,
                        IsTakenFromStock = spu.IsTakenFromStock
                    }).ToList()
                },
                ProgressRecords = errorDetail.ProgressRecords?.Select(pr => new
                {
                    Id = pr.Id,
                    ErrorFixStepId = pr.ErrorFixStepId,
                    IsCompleted = pr.IsCompleted,
                    StepName = pr.ErrorFixStep?.StepDescription // Gi? ??nh ErrorFixStep có thu?c tính Title
                }).ToList()
            };

            return Result.SuccessWithObject(resultDto);
        }

        public async Task<Result> GetByRequestIdAsync(Guid requestId)
        {
            var requestExists = await _unitOfWork.RequestRepository.GetByIdAsync(requestId) != null;
            if (!requestExists)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request not found"));

            var errorDetails = await _unitOfWork.ErrorDetailRepository.GetByRequestIdAsync(requestId);
            return Result.SuccessWithObject(errorDetails);
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var errorDetail = await _unitOfWork.ErrorDetailRepository.GetByIdAsync(id);
            if (errorDetail == null /*||errorDetail.IsDeleted*/)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "ErrorDetail not found"));

            // errorDetail.IsDeleted = true;
            // errorDetail.ModifiedDate = TimeHelper.GetHoChiMinhTime();

            await _unitOfWork.ErrorDetailRepository.UpdateAsync(errorDetail);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "ErrorDetail deleted successfully!" });
        }

        public async Task<Result> UpdateErrorGuidelineAsync(Guid id, UpdateErrorGuidelineRequest request)
        {
            // 1?? Ki?m tra t?n t?i c?a ErrorDetail
            var errorDetail = await _unitOfWork.ErrorDetailRepository.GetByIdAsync(id);
            if (errorDetail == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "ErrorDetail not found"));

            if (!request.ErrorGuideLineId.HasValue)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "ErrorGuideLineId is required"));

            // 2?? Gán ErrorGuideLine m?i
            errorDetail.ErrorGuideLineId = request.ErrorGuideLineId;

            // 3?? Generate ProgressRecords m?i theo Guideline
            var errorFixSteps = await _unitOfWork.ErrorFixStepRepository.GetByErrorGuidelineIdAsync(request.ErrorGuideLineId.Value);
            if (errorFixSteps == null || !errorFixSteps.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "No ErrorFixSteps found for the given ErrorGuidelineId"));

            var progressRecords = errorFixSteps.Select(step => new ErrorFixProgress
            {
                Id = Guid.NewGuid(),
                ErrorDetailId = id,
                ErrorFixStepId = step.Id,
                IsCompleted = false
            }).ToList();

            // 4?? Thêm các Progress m?i (ch? thêm n?u ch?a có step ?ó)
            if (errorDetail.ProgressRecords == null)
            {
                errorDetail.ProgressRecords = new List<ErrorFixProgress>();
            }

            var newProgress = progressRecords
                .Where(pr => !errorDetail.ProgressRecords.Any(existing => existing.ErrorFixStepId == pr.ErrorFixStepId))
                .ToList();

            // 5?? Thêm progress m?i vào repository
            if (newProgress.Any())
            {
                await _unitOfWork.ErrorFixProgressRepository.AddRangeAsync(newProgress);
            }

            // 6?? X? lý t?o m?i RequestTakeSparePartUsage và SparePartUsage

            // 6.1 Xóa b?n RequestTakeSparePartUsage c? n?u có
            if (errorDetail.RequestTakeSparePartUsageId.HasValue)
            {
                var oldRequest = await _unitOfWork.RequestTakeSparePartUsageRepository.GetByIdAsync(errorDetail.RequestTakeSparePartUsageId.Value);
                if (oldRequest != null)
                {
                    _unitOfWork.RequestTakeSparePartUsageRepository.Remove(oldRequest);
                }
            }

            // 6.2 T?o m?i RequestTakeSparePartUsage
            var requestCode = $"REQ-{TimeHelper.GetHoChiMinhTime().Ticks}";
            var newRequest = new RequestTakeSparePartUsage
            {
                Id = Guid.NewGuid(),
                RequestCode = requestCode,
                RequestDate = TimeHelper.GetHoChiMinhTime(),
                Status = Domain.Enum.SparePartRequestStatus.Unconfirmed,
                SparePartUsages = new List<SparePartUsage>(),

            };

            // 6.3 L?y danh sách sparepart theo guideline
            var errorSpareparts = await _unitOfWork.ErrorSparepartRepository.GetByErrorGuidelineIdAsync(request.ErrorGuideLineId.Value);
            if (errorSpareparts == null || !errorSpareparts.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "No ErrorSpareparts found for the given ErrorGuidelineId"));

            foreach (var sp in errorSpareparts)
            {
                var usage = new SparePartUsage
                {
                    Id = Guid.NewGuid(),
                    RequestTakeSparePartUsageId = newRequest.Id,
                    SparePartId = sp.SparepartId,
                    QuantityUsed = sp.QuantityNeeded ?? 0,
                    IsTakenFromStock = false
                };
                newRequest.SparePartUsages.Add(usage);
            }

            // 6.4 Insert RequestTakeSparePartUsage m?i
            await _unitOfWork.RequestTakeSparePartUsageRepository.CreateAsync(newRequest);

            // 7?? Gán l?i request m?i vào ErrorDetail
            errorDetail.RequestTakeSparePartUsageId = newRequest.Id;

            // 8?? Update ErrorDetail
            await _unitOfWork.ErrorDetailRepository.UpdateAsync(errorDetail);

            // 9?? Cu?i cùng SaveChanges 1 l?n duy nh?t
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "Error guideline updated successfully!" });
        }



        public async Task<Result> UpdateErrorTaskAsync(Guid id, UpdateErrorTaskRequest request)
        {
            var errorDetail = await _unitOfWork.ErrorDetailRepository.GetByIdAsync(id);
            if (errorDetail == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "ErrorDetail not found"));

            if (!request.TaskId.HasValue)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "TaskId is required"));

            errorDetail.TaskId = request.TaskId;

            await _unitOfWork.ErrorDetailRepository.UpdateAsync(errorDetail);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "Error task updated successfully!" });
        }
    }
}