using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
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

            // Validate Error exists
            // var errorExists = await _unitOfWork.ErrorRepository.GetByIdAsync(request.ErrorId) != null;
            // if (!errorExists)
            //     return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Error not found"));

            // Get ReportId from RequestId
            var reportId = await _unitOfWork.ErrorDetailRepository.GetReportIdByRequestIdAsync(request.RequestId);
            if (reportId == Guid.Empty)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Report not found for this request"));

            // Check if ErrorDetail already exists for this Error and Report
            // var exists = await _unitOfWork.ErrorDetailRepository.ErrorDetailExistsAsync(request.ErrorId, reportId);
            // if (exists)
            //     return Result.Failure(Infrastructure.DTOs.Common.Error.BadRequest("Bad Request", "ErrorDetail already exists for this Error and Report"));

            var errorDetail = new ErrorDetail
            {
                // Id = Guid.NewGuid(),
                ErrorId = request.ErrorId,
                ReportId = reportId,
                // DetectedDate = DateTime.UtcNow,
                // IsResolved = false,
                // CreatedDate = DateTime.UtcNow,
                // IsDeleted = false
            };

            await _unitOfWork.ErrorDetailRepository.CreateAsync(errorDetail);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "ErrorDetail created successfully!"/*, ErrorDetailId = errorDetail.Id*/ });
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
            // errorDetail.ModifiedDate = DateTime.UtcNow;

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
            var requestCode = $"REQ-{DateTime.UtcNow.Ticks}";
            var newRequest = new RequestTakeSparePartUsage
            {
                Id = Guid.NewGuid(),
                RequestCode = requestCode,
                RequestDate = DateTime.UtcNow,
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