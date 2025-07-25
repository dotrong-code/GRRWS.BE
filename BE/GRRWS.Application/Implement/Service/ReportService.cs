using AutoMapper;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;

using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.ErrorDetail;

using GRRWS.Infrastructure.DTOs.Report;
using GRRWS.Infrastructure.DTOs.Task.ActionTask;
using GRRWS.Infrastructure.DTOs.Task.Repair;
using GRRWS.Infrastructure.DTOs.Task.Warranty;
using GRRWS.Infrastructure.Interfaces;

namespace GRRWS.Application.Implement.Service
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        private readonly IMechanicShiftService _mechanicShiftService;
        
        public ReportService(IUnitOfWork unit, IMapper mapper, ITaskService taskService, IMechanicShiftService mechanicShiftService)
        {
            _unit = unit;
            _mapper = mapper;
            _taskService = taskService;
            _mechanicShiftService = mechanicShiftService;
           
        }
#region Main
        #region Create Report
        public async Task<Result> CreateReportWithIssueErrorAsync(ReportCreateWithIssueErrorDTO dto)
        {
            // Kiểm tra RequestId
            if (dto.RequestId == null)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "RequestId is required.", 0));

            // Kiểm tra Priority
            if (dto.Priority.GetType() != typeof(int))
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be an integer.", 0));
            if (dto.Priority < 0 || dto.Priority > 5)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be between 0 and 5.", 0));

            // Kiểm tra ActionType
            if (!Enum.IsDefined(typeof(ActionType), dto.ActionType))
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Invalid ActionType.", 0));

            // Lấy Request
            var request = await _unit.RequestRepository.GetRequestByIdAsync((Guid)dto.RequestId);
            if (request == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Request not found for the provided RequestId."
                ));
            if (request.ReportId != null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict(
                    "Conflict", "Request already has an associated report."
                ));

            // Kiểm tra ErrorIds
            var allErrorIds = new List<Guid>();
            if (dto.ErrorIds != null && dto.ErrorIds.Any())
            {
                if (dto.ErrorIds.Any(errorId => errorId == Guid.Empty))
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "ErrorIds cannot contain empty GUIDs.", 0));
                allErrorIds.AddRange(dto.ErrorIds);
            }

            // Kiểm tra IssueErrorMappings
            var issueErrorMappings = dto.IssueErrorMappings ?? new Dictionary<Guid, List<Guid>>();
            if (issueErrorMappings.Any())
            {
                foreach (var mapping in issueErrorMappings)
                {
                    if (mapping.Key == Guid.Empty)
                        return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "IssueId in IssueErrorMappings cannot be empty GUID.", 0));
                    if (mapping.Value.Any(errorId => errorId == Guid.Empty))
                        return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "ErrorIds in IssueErrorMappings cannot contain empty GUIDs.", 0));
                    allErrorIds.AddRange(mapping.Value);
                }
            }

            // Kiểm tra xem các Error tồn tại
            var missingErrors = await _unit.ErrorRepository.GetNotFoundErrorDisplayNamesAsync(allErrorIds.Distinct());
            if (missingErrors.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Some errors do not exist: " + string.Join(", ", missingErrors.Select(x => x.Id))
                ));

            // Tạo Location
            var createLocation = "";
            try
            {
                createLocation = TitleHelper.GenerateReportTitle(
                    request.Device.Position.Zone.Area.AreaCode,
                    request.Device.Position.Zone.ZoneCode,
                    request.Device.Position.Index,
                    request.Device.DeviceCode);
            }
            catch (Exception)
            {
                createLocation = "Create title fail";
            }

            // Tạo Report
            var report = new Report
            {
                Id = Guid.NewGuid(),
                RequestId = dto.RequestId,
                Location = createLocation,
                CreatedDate = TimeHelper.GetHoChiMinhTime(),
                
            };

            // Tạo ErrorDetails
            if (allErrorIds.Any())
            {
                report.ErrorDetails = allErrorIds.Select(errorId => new ErrorDetail
                {
                    ReportId = report.Id,
                    ErrorId = errorId
                }).ToList();
            }
            else
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "No ErrorIds provided.", 0));
            }

            // Tạo IssueErrors
            var issueErrors = new List<IssueError>();
            foreach (var mapping in issueErrorMappings)
            {
                var issueId = mapping.Key;
                var errorIds = mapping.Value;
                foreach (var errorId in errorIds)
                {
                    var existingIssueError = await _unit.IssueErrorRepository.GetByIssueAndErrorIdAsync(issueId, errorId);
                    if (existingIssueError == null)
                    {
                        issueErrors.Add(new IssueError
                        {
                            Id = Guid.NewGuid(),
                            IssueId = issueId,
                            ErrorId = errorId
                        });
                    }
                }
            }
            if (issueErrors.Any())
            {
                await _unit.IssueErrorRepository.CreateRangeAsync(issueErrors);
            }

            // Lưu Report
            await _unit.ReportRepository.CreateAsync(report);

            // Cập nhật Request
            request.ReportId = report.Id;
            request.Status = Status.InProgress;
            await _unit.RequestRepository.UpdateAsync(request);

            // Tạo Task Group dựa trên ActionType
            var users = await _unit.UserRepository.GetUsersByRole(2); // Role 2: Mechanic
            var systemUserId = users?.FirstOrDefault()?.Id ?? Guid.Parse("32222222-2222-2222-2222-222222222222");
            Result taskGroupResult;

            if (dto.ActionType == ActionType.Replacement)
            {
                taskGroupResult = await CreateReplacementTaskGroup(report.Id, allErrorIds.Distinct().ToList(), systemUserId);
            }
            else // ActionType.Repair
            {
                taskGroupResult = await CreateRepairTaskGroup(report.Id, allErrorIds.Distinct().ToList(), systemUserId);
            }

            if (taskGroupResult.IsFailure)
            {
                return Result.SuccessWithObject(new
                {
                    Message = $"Report created successfully but failed to create task group: {taskGroupResult.Error.Description}",
                    ReportId = report.Id
                });
            }

            dynamic data = taskGroupResult.Object;
            Guid TaskGroupId = data.TaskGroupId;

            // Lưu tất cả thay đổi
            await _unit.SaveChangesAsync();

            return Result.SuccessWithObject(new
            {
                Message = $"Report created successfully for {dto.ActionType} with IssueErrors!",
                ReportId = report.Id,
                TaskGroupId = TaskGroupId 
            });
        }
        public async Task<Result> CreateReportWithIssueError2Async(ReportCreateWithIssueErrorDTO dto)
        {
            // Kiểm tra RequestId
            if (dto.RequestId == null)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "RequestId is required.", 0));

            // Kiểm tra Priority
            if (dto.Priority.GetType() != typeof(int))
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be an integer.", 0));
            if (dto.Priority < 0 || dto.Priority > 5)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be between 0 and 5.", 0));

            // Kiểm tra ActionType
            if (!Enum.IsDefined(typeof(ActionType), dto.ActionType))
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Invalid ActionType.", 0));

            // Lấy Request
            var request = await _unit.RequestRepository.GetRequestByIdAsync((Guid)dto.RequestId);
            if (request == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Request not found for the provided RequestId."
                ));
            if (request.ReportId != null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict(
                    "Conflict", "Request already has an associated report."
                ));

            // Kiểm tra ErrorIds
            var allErrorIds = new List<Guid>();
            if (dto.ErrorIds != null && dto.ErrorIds.Any())
            {
                if (dto.ErrorIds.Any(errorId => errorId == Guid.Empty))
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "ErrorIds cannot contain empty GUIDs.", 0));
                allErrorIds.AddRange(dto.ErrorIds);
            }

            // Kiểm tra IssueErrorMappings
            var issueErrorMappings = dto.IssueErrorMappings ?? new Dictionary<Guid, List<Guid>>();
            if (issueErrorMappings.Any())
            {
                foreach (var mapping in issueErrorMappings)
                {
                    if (mapping.Key == Guid.Empty)
                        return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "IssueId in IssueErrorMappings cannot be empty GUID.", 0));
                    if (mapping.Value.Any(errorId => errorId == Guid.Empty))
                        return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "ErrorIds in IssueErrorMappings cannot contain empty GUIDs.", 0));
                    allErrorIds.AddRange(mapping.Value);
                }
            }

            // Kiểm tra xem các Error tồn tại
            var missingErrors = await _unit.ErrorRepository.GetNotFoundErrorDisplayNamesAsync(allErrorIds.Distinct());
            if (missingErrors.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Some errors do not exist: " + string.Join(", ", missingErrors.Select(x => x.Id))
                ));

            // Tạo Location
            var createLocation = "";
            try
            {
                createLocation = TitleHelper.GenerateReportTitle(
                    request.Device.Position.Zone.Area.AreaCode,
                    request.Device.Position.Zone.ZoneCode,
                    request.Device.Position.Index,
                    request.Device.DeviceCode);
            }
            catch (Exception)
            {
                createLocation = "Create title fail";
            }

            // Tạo Report
            var report = new Report
            {
                Id = Guid.NewGuid(),
                RequestId = dto.RequestId,
                Location = createLocation,
                CreatedDate = TimeHelper.GetHoChiMinhTime(),

            };

            // Tạo ErrorDetails
            if (allErrorIds.Any())
            {
                report.ErrorDetails = allErrorIds.Select(errorId => new ErrorDetail
                {
                    ReportId = report.Id,
                    ErrorId = errorId
                }).ToList();
            }
            else
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "No ErrorIds provided.", 0));
            }

            // Tạo IssueErrors
            var issueErrors = new List<IssueError>();
            foreach (var mapping in issueErrorMappings)
            {
                var issueId = mapping.Key;
                var errorIds = mapping.Value;
                foreach (var errorId in errorIds)
                {
                    var existingIssueError = await _unit.IssueErrorRepository.GetByIssueAndErrorIdAsync(issueId, errorId);
                    if (existingIssueError == null)
                    {
                        issueErrors.Add(new IssueError
                        {
                            Id = Guid.NewGuid(),
                            IssueId = issueId,
                            ErrorId = errorId
                        });
                    }
                }
            }
            if (issueErrors.Any())
            {
                await _unit.IssueErrorRepository.CreateRangeAsync(issueErrors);
            }

            // Lưu Report
            await _unit.ReportRepository.CreateAsync(report);

            // Cập nhật Request
            request.ReportId = report.Id;
            request.Status = Status.InProgress;
            await _unit.RequestRepository.UpdateAsync(request);

            // Tạo Task Group dựa trên ActionType
            var users = await _unit.UserRepository.GetUsersByRole(2); // Role 2: Mechanic
            var systemUserId = users?.FirstOrDefault()?.Id ?? Guid.Parse("32222222-2222-2222-2222-222222222222");
            Result taskGroupResult;
            if (dto.ActionType == ActionType.Replacement)
            {
                taskGroupResult = await CreateReplacementTaskGroup(report.Id, allErrorIds.Distinct().ToList(), systemUserId);
            }
            else // ActionType.Repair
            {
                taskGroupResult = await CreateRepairTaskGroup2(report.Id, allErrorIds.Distinct().ToList(), systemUserId);
            }

            if (taskGroupResult.IsFailure)
            {
                return Result.SuccessWithObject(new
                {
                    Message = $"Report created successfully but failed to create task group: {taskGroupResult.Error.Description}",
                    ReportId = report.Id
                });
            }


            dynamic data = taskGroupResult.Object;
            Guid TaskGroupId = data.TaskGroupId;

            // Lưu tất cả thay đổi
            await _unit.SaveChangesAsync();

            return Result.SuccessWithObject(new
            {
                Message = $"Report created successfully for {dto.ActionType} with IssueErrors!",
                ReportId = report.Id,
                TaskGroupId = TaskGroupId
            });
        }

        // Tạo Report mang đi bảo hành
        public async Task<Result> CreateReportWithIssueSymtomAsync(ReportCreateWithIssueSymtomDTO dto, Guid userId)
        {
            // Kiểm tra RequestId
            if (dto.RequestId == null)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "RequestId is required.", 0));

            // Kiểm tra Priority
            if (dto.Priority.GetType() != typeof(int))
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be an integer.", 0));
            if (dto.Priority < 0 || dto.Priority > 5)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be between 0 and 5.", 0));

            // Lấy Request
            var request = await _unit.RequestRepository.GetRequestByIdAsync((Guid)dto.RequestId);
            if (request == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Request not found for the provided RequestId."
                ));
            if (request.ReportId != null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict(
                    "Conflict", "Request already has an associated report."
                ));

            // Kiểm tra allSymtomIds
            var allSymtomIds = new List<Guid>();
            if (dto.TechnicalSymtomIds != null && dto.TechnicalSymtomIds.Any())
            {
                if (dto.TechnicalSymtomIds.Any(symtomId => symtomId == Guid.Empty))
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "SymtomIds cannot contain empty GUIDs.", 0));
                allSymtomIds.AddRange(dto.TechnicalSymtomIds);
            }
            else
            {
                dto.TechnicalSymtomIds = new List<Guid> { Guid.Parse("e8e8e8e8-e8e8-e8e8-e8e8-e8e8e8e8e8e8") };
                allSymtomIds.Add(Guid.Parse("e8e8e8e8-e8e8-e8e8-e8e8-e8e8e8e8e8e8"));

            }
            // Kiểm tra IssueSymtomMappings
            var issueSymtomMappings = dto.IssueSymtomMappings ?? new Dictionary<Guid, List<Guid>>();
            if (issueSymtomMappings.Any())
            {
                foreach (var mapping in issueSymtomMappings)
                {
                    if (mapping.Key == Guid.Empty)
                        return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "SymtomId in IssueSymtomMappings cannot be empty GUID.", 0));
                    if (mapping.Value.Any(symtomId => symtomId == Guid.Empty))
                        return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "SymtomIds in IssueSymtomMappings cannot contain empty GUIDs.", 0));
                    allSymtomIds.AddRange(mapping.Value);
                }
            }

            // Kiểm tra xem các Symtom tồn tại
            var missingSymtoms = await _unit.TechnicalSymtomRepository.GetNotFoundTechnicalSymtomDisplayNamesAsync(allSymtomIds.Distinct());
            if (missingSymtoms.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Some symtoms do not exist: " + string.Join(", ", missingSymtoms.Select(x => x.Id))
                ));

            // Tạo Location
            var createLocation = "";
            try
            {
                createLocation = TitleHelper.GenerateReportTitle(request.Device.Position.Zone.Area.AreaCode, request.Device.Position.Zone.ZoneCode, request.Device.Position.Index, request.Device.DeviceCode);
            }
            catch (Exception)
            {
                createLocation = "Create title fail";
            }

            // Tạo Report
            var report = new Report
            {
                Id = Guid.NewGuid(),
                RequestId = dto.RequestId,
                Location = createLocation,
                CreatedDate = TimeHelper.GetHoChiMinhTime()
            };

            // Tạo TechnicalSymptomReports
            if (allSymtomIds.Any())
            {
                report.TechnicalSymptomReports = allSymtomIds.Select(symtomId => new TechnicalSymptomReport
                {
                    ReportId = report.Id,
                    TechnicalSymptomId = symtomId
                }).ToList();
            }
            else
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "No SymptomIds provided.", 0));
            }

            // Tạo issueSymptoms
            var issueSymptoms = new List<IssueTechnicalSymptom>();
            foreach (var mapping in issueSymtomMappings)
            {
                var issueId = mapping.Key;
                var symtomIds = mapping.Value;
                foreach (var symtomId in symtomIds)
                {
                    // Check if the IssueTechnicalSymptom combination already exists
                    var existingIssueSymptom = await _unit.IssueTechnicalSymptomRepository.GetByIssueAndSymptomIdAsync(issueId, symtomId);
                    if (existingIssueSymptom == null) // Only add if it doesn't exist
                    {
                        issueSymptoms.Add(new IssueTechnicalSymptom
                        {
                            Id = Guid.NewGuid(),
                            IssueId = issueId,
                            TechnicalSymptomId = symtomId
                        });
                    }
                }
            }
            if (issueSymptoms.Any())
            {
                await _unit.IssueTechnicalSymptomRepository.CreateRangeAsync(issueSymptoms);
            }

            // Lưu Report
            await _unit.ReportRepository.CreateAsync(report);

            // Cập nhật Request
            request.ReportId = report.Id;
            request.Status = Status.InProgress;
            await _unit.RequestRepository.UpdateAsync(request);

            await _unit.SaveChangesAsync();


            // Gắn người tạo report (dùng để )

            var users = await _unit.UserRepository.GetByIdAsync(userId);
            

            //Tao Task Bao hanh
            var result = await CreateWarrantyTaskGroup(report.Id, allSymtomIds.Distinct().ToList(), userId);
            if (result.IsFailure)
            {
                return Result.SuccessWithObject(new { Message = $"Report created successfully but failed to create task!.{result.Error.Description}", ReportId = report.Id });
            }
            dynamic data = result.Object;

            Guid taskGroupId = data.taskGroupId;

            return Result.SuccessWithObject(new { Message = "Report created successfully with IssueSymtoms!", ReportId = report.Id });
        }
        #endregion

        #region Get Report
        public async Task<Result> GetErrorReportByIdAsync(Guid id)
        {
            var report = await _unit.ReportRepository.GetReportWithErrorDetailsAsync(id);
            if (report == null)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Report not found.", 0));

            // Ánh xạ thủ công sang DTO nếu cần
            var resultDto = new ReportViewErorDTO
            {
                Id = report.Id,
                RequestId = report.RequestId,
                Location = report.Location,
                CreatedDate = report.CreatedDate,
                ModifiedDate = report.ModifiedDate,
                IsDeleted = report.IsDeleted,
                ErrorDetails = report.ErrorDetails?.Select(ed => new ErrorDetailViewDTO
                {
                    Id = ed.Id,
                    ReportId = ed.ReportId,
                    ErrorId = ed.ErrorId,
                    ErrorName = ed.Error?.Name
                }).ToList()
            };

            return Result.SuccessWithObject(resultDto);
        }
        public async Task<Result> GetByIdAsync(Guid id)
        {
            var report = await _unit.ReportRepository.GetReportWithRequestAsync(id);
            if (report == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Report not found.", 0));

            var dto = _mapper.Map<ReportViewDTO>(report);
            return Result.SuccessWithObject(dto);
        }
        public async Task<Result> GetAllAsync()
        {
            var reports = await _unit.ReportRepository.GetReportsWithRequestAsync();
            var dtos = _mapper.Map<List<ReportViewDTO>>(reports).Cast<object>().ToList();
            return Result.SuccessWithObject(dtos);
        }
        #endregion

        #region Delete Report
        public async Task<Result> DeleteAsync(Guid id)
        {
            var report = await _unit.ReportRepository.GetByIdAsync(id);
            if (report == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Report not found.", 0));
            report.IsDeleted = true;
            report.ModifiedDate = TimeHelper.GetHoChiMinhTime();
            await _unit.ReportRepository.UpdateAsync(report);
            return Result.SuccessWithObject(new { Message = "Report canceled successfully!" });
        }
        #endregion

        #endregion
        #region Private method

        // Tạo 2 Task (Thay thế máy và Mang đi bảo hành) + Tạo 3 MachineActionConfirmation (1 StockIn , 1 StockOut, 1 Installation) GỌI TASKSERVICE
        private async Task<Result> CreateWarrantyTaskGroup(Guid reportId, List<Guid> technicalSymptomIds, Guid createdByUserId)
        {
            try
            {
                _unit.ClearChangeTracker(); // Clear change tracker to avoid tracking issues
                var report = await _unit.ReportRepository.GetByIdAsync(reportId);
                if (report == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                        "NotFound", $"Report not found for the provided {reportId}."
                    ));
                }
                var requestId = report.RequestId ?? Guid.Empty;
                if (requestId == Guid.Empty)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                        "NotFound", $"Request not exist in this Report {reportId}."
                    ));
                }
                // Get request to verify it exists and get device information
                var request = await _unit.RequestRepository.GetRequestByIdAsync(requestId);
                if (request == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", $"Request not found for the provided {reportId}."));
                }
                
                //
                var deviceWarrantyId = await _unit.DeviceWarrantyRepository.GetDeviceWarrantyByDeviceIdForDevice(request.DeviceId);
                // Step 2: Tạo task mang máy đi bảo hành (Chưa có tạo request lấy máy bảo hành dưới kho rồi mang đi bảo hành)
                var warrantyRequest = new CreateWarrantyTaskRequest
                {
                    RequestId = requestId,
                    AssigneeId = null,
                    StartDate = null,
                    DeviceWarrantyId = deviceWarrantyId,
                    TechnicalIssueIds = technicalSymptomIds,

                };
                var warrantyResult = await _taskService.CreateWarrantyTask(warrantyRequest, createdByUserId);
                if (warrantyResult.IsFailure)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.Failure(
                        "Failure", $"Warranty task creation failed"
                    ));
                }
                dynamic data = warrantyResult.Object;
                Guid taskGroupId = data.TaskGroupId;
                _unit.ClearChangeTracker();

                // Step 3: Tạo task lấy máy thay thế tạm thời + Request yêu cầu lấy máy dưới kho
                var installRequest = new CreateInstallTaskRequest
                {
                    RequestId = requestId,
                    AssigneeId=null,
                    StartDate =null,
                    TaskGroupId = taskGroupId,
                    NewDeviceId=null
                };
                var installResult = await _taskService.CreateInstallTask(installRequest, createdByUserId);
                if (installResult.IsFailure)
                {
                    {
                        return Result.Failure(Infrastructure.DTOs.Common.Error.Failure(
                            "Failure", $"Installation task creation failed"
                        ));
                    }

                }
                return Result.SuccessWithObject(new { Message = "Create task group sucessfully", taskGroupId = taskGroupId });
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure(
                    "InternalServerError", $"Failed to create warranty task group: {ex.Message}"
                ));
            }
        }
        private async Task<Result> CreateReplacementTaskGroup(Guid reportId, List<Guid> errorIds, Guid createdByUserId)
        {
            try
            {
                _unit.ClearChangeTracker(); // Clear change tracker để tránh lỗi tracking
                var report = await _unit.ReportRepository.GetByIdAsync(reportId);
                if (report == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                        "NotFound", $"Report not found for the provided {reportId}."
                    ));
                }
                var requestId = report.RequestId ?? Guid.Empty;
                if (requestId == Guid.Empty)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                        "NotFound", $"Request not exist in this Report {reportId}."
                    ));
                }

                // Get request to verify it exists and get device information
                var request = await _unit.RequestRepository.GetRequestByIdAsync(requestId);
                if (request == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                        "NotFound", $"Request not found for the provided {reportId}."
                    ));
                }

                // Tạo Task Group, tương tự CreateWarrantyTaskGroup
                
                
                _unit.ClearChangeTracker();

                // Tạo Installation Task
                var installRequest = new CreateInstallTaskRequest
                {
                    RequestId = requestId,
                   
                    
                };
                var installResult = await _taskService.CreateInstallTask(installRequest, createdByUserId);
                if (installResult.IsFailure)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.Failure(
                        "Failure", "Installation task creation failed."
                    ));
                }
                dynamic installData = installResult.Object;
                Guid TaskGroupId = installData.TaskGroupId;

                

                // Lưu tất cả thay đổi
                await _unit.SaveChangesAsync();

                return Result.SuccessWithObject(new
                {
                    Message = "Replacement task group created successfully!",
                    TaskGroupId = TaskGroupId
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure(
                    "InternalServerError", $"Failed to create replacement task group: {ex.Message}"
                ));
            }
        }
        private async Task<Result> CreateRepairTaskGroup(Guid reportId, List<Guid> errorIds, Guid createdByUserId)
        {
            try
            {
                _unit.ClearChangeTracker();
                var report = await _unit.ReportRepository.GetByIdAsync(reportId);
                if (report == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                        "NotFound", $"Report not found for the provided {reportId}."
                    ));
                }
                var requestId = report.RequestId ?? Guid.Empty;
                if (requestId == Guid.Empty)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                        "NotFound", $"Request not exist in this Report {reportId}."
                    ));
                }

                // Get request to verify it exists and get device information
                var request = await _unit.RequestRepository.GetRequestByIdAsync(requestId);
                if (request == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                        "NotFound", $"Request not found for the provided {reportId}."
                    ));
                }

               
                // Chuyển đổi ErrorIds thành ErrorGuidelineIds
                var guidelineIds = await _unit.ErrorGuidelineRepository.GetGuidelineIdsByErrorIdsAsync(errorIds);
                if (!guidelineIds.Any())
                {
                    
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                        "NotFound", $"No error guidelines found for the provided error IDs: {string.Join(", ", errorIds)}."
                    ));
                }
                // Tạo Repair Task
                var repairRequest = new CreateRepairTaskRequest
                {
                    RequestId = requestId,
                    ErrorGuidelineIds = guidelineIds // Gán các ErrorIds để xác định lỗi cần sửa
                };
                var repairResult = await _taskService.CreateRepairTask(repairRequest, createdByUserId);
                if (repairResult.IsFailure)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.Failure(
                        "Failure", "Repair task creation failed."
                    ));
                }
                dynamic repairData = repairResult.Object;
                Guid repairTaskId = repairData.TaskId;
                Guid taskGroupId = repairData.TaskGroupId;
                _unit.ClearChangeTracker();
                return Result.SuccessWithObject(new
                {
                    Message = "Repair task group created successfully!",
                    TaskGroupId = taskGroupId
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure(
                    "InternalServerError", $"Failed to create repair task group: {ex.Message}"
                ));
            }
        }
        private async Task<Result> CreateRepairTaskGroup2(Guid reportId, List<Guid> errorIds, Guid createdByUserId)
        {
            try
            {
                _unit.ClearChangeTracker();
                var report = await _unit.ReportRepository.GetByIdAsync(reportId);
                if (report == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                        "NotFound", $"Report not found for the provided {reportId}."
                    ));
                }
                var requestId = report.RequestId ?? Guid.Empty;
                if (requestId == Guid.Empty)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                        "NotFound", $"Request not exist in this Report {reportId}."
                    ));
                }

                // Get request to verify it exists and get device information
                var request = await _unit.RequestRepository.GetRequestByIdAsync(requestId);
                if (request == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                        "NotFound", $"Request not found for the provided {reportId}."
                    ));
                }


                // Chuyển đổi ErrorIds thành ErrorGuidelineIds
                var guidelineIds = await _unit.ErrorGuidelineRepository.GetGuidelineIdsByErrorIdsAsync(errorIds);
                if (!guidelineIds.Any())
                {

                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                        "NotFound", $"No error guidelines found for the provided error IDs: {string.Join(", ", errorIds)}."
                    ));
                }
                // Tạo Repair Task
                var repairRequest = new Infrastructure.DTOs.Task.CreateCombinedTaskRequest
                {
                    RequestId = requestId,
                    ErrorGuidelineIds = guidelineIds // Gán các ErrorIds để xác định lỗi cần sửa
                };
                var repairResult = await _taskService.CreateCombinedRepairAndReplacementTasks(repairRequest, createdByUserId);
                if (repairResult.IsFailure)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.Failure(
                        "Failure", "Repair task creation failed."
                    ));
                }
                dynamic combinedData = repairResult.Object;
                Guid taskGroupId = combinedData.TaskGroupId;
                Guid? repairTaskId = combinedData.RepairTaskId;
                Guid? installationTaskId = combinedData.InstallationTaskId;
                return Result.SuccessWithObject(new
                {
                    Message = "Repair task group created successfully!",
                    TaskGroupId = taskGroupId,
                    InstallationTaskId = installationTaskId,
                    RepairTaskId = repairTaskId
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure(
                    "InternalServerError", $"Failed to create repair task group: {ex.Message}"
                ));
            }
        }

#endregion
    }
}
