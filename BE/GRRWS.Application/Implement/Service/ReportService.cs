﻿using AutoMapper;
using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;

using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.ErrorDetail;

using GRRWS.Infrastructure.DTOs.Report;
using GRRWS.Infrastructure.DTOs.Task.ActionTask;
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


        public async Task<Result> CreateAsync(ReportCreateDTO dto)
        {
            if (dto.RequestId == null)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "RequestId is required.", 0));

            if (dto.ErrorIds != null && dto.ErrorIds.Any())
                if (dto.ErrorIds.Any(errorId => errorId == Guid.Empty))
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "ErrorIds cannot contain empty GUIDs.", 0));

            if (dto.Priority.GetType() != typeof(int))
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be an integer.", 0));

            if (dto.Priority < 0 || dto.Priority > 5)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be between 0 and 5.", 0));

            var request = await _unit.RequestRepository.GetRequestByIdAsync((Guid)dto.RequestId);
            if (request == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Request not found for the provided RequestId."
                ));
            }
            if (request.ReportId != null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict(
                    "Conflict", "Request already has an associated report."
                ));
            }

            var missingErrors = await _unit.ErrorRepository.GetNotFoundErrorDisplayNamesAsync(dto.ErrorIds);
            if (missingErrors.Any())
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Some errors do not exist: " + string.Join(", ", missingErrors.Select(x => x.Id))
                ));
            }

            var createLocation = "";
            try
            {
                createLocation = TitleHelper.GenerateReportTitle(request.Device.Position.Zone.Area.AreaCode, request.Device.Position.Zone.ZoneCode, request.Device.Position.Index, request.Device.DeviceCode);
            }
            catch (Exception)
            {
                createLocation = "Create title fail";
            }

            var report = _mapper.Map<Report>(dto);
            report.Id = Guid.NewGuid();
            report.CreatedDate = DateTime.Now;
            report.Location = createLocation;

            if (dto.ErrorIds != null && dto.ErrorIds.Any())
            {
                report.ErrorDetails = dto.ErrorIds.Select(errorId => new ErrorDetail
                {
                    ReportId = report.Id,
                    ErrorId = errorId
                }).ToList();
            }
            else
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "ErrorIds is null!.", 0));
            }
            await _unit.ReportRepository.CreateAsync(report);
            var getRequest = await _unit.RequestRepository.GetRequestByIdAsync((Guid)report.RequestId);
            getRequest.ReportId = report.Id;
            getRequest.Status = Status.InProgress;
            await _unit.RequestRepository.UpdateAsync(getRequest);
            return Result.SuccessWithObject(new { Message = "Report created successfully!", ReportId = report.Id });
        }

        public async Task<Result> CreateWarrantyReportAsync(ReportWarrantyCreateDTO dto)
        {
            if (dto.RequestId == null)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "RequestId is required.", 0));

            if (dto.TechnicalSymtomIds != null && dto.TechnicalSymtomIds.Any())
                if (dto.TechnicalSymtomIds.Any(technicalSymtomId => technicalSymtomId == Guid.Empty))
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "TechnicalSymtomIds cannot contain empty GUIDs.", 0));

            if (dto.Priority.GetType() != typeof(int))
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be an integer.", 0));

            if (dto.Priority < 0 || dto.Priority > 5)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be between 0 and 5.", 0));

            var request = await _unit.RequestRepository.GetRequestByIdAsync((Guid)dto.RequestId);
            if (request == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Request not found for the provided RequestId."
                ));
            }
            if (request.ReportId != null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict(
                    "Conflict", "Request already has an associated report."
                ));
            }

            var missingSymtoms = await _unit.TechnicalSymtomRepository.GetNotFoundTechnicalSymtomDisplayNamesAsync(dto.TechnicalSymtomIds);
            if (missingSymtoms.Any())
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Some symtoms do not exist: " + string.Join(", ", missingSymtoms.Select(x => x.Id))
                ));
            }

            var createLocation = "";
            try
            {
                createLocation = TitleHelper.GenerateReportTitle(request.Device.Position.Zone.Area.AreaCode, request.Device.Position.Zone.ZoneCode, request.Device.Position.Index, request.Device.DeviceCode);
            }
            catch (Exception)
            {
                createLocation = "Create title fail";
            }

            var report = _mapper.Map<Report>(dto);
            report.Id = Guid.NewGuid();
            report.CreatedDate = DateTime.Now;
            report.Location = createLocation;

            if (dto.TechnicalSymtomIds != null && dto.TechnicalSymtomIds.Any())
            {
                report.TechnicalSymptomReports = dto.TechnicalSymtomIds.Select(symtomId => new TechnicalSymptomReport
                {
                    ReportId = report.Id,
                    TechnicalSymptomId = symtomId
                }).ToList();
            }
            else
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "TechnicalSymptomIds is null!.", 0));
            }
            await _unit.ReportRepository.CreateAsync(report);
            var getRequest = await _unit.RequestRepository.GetRequestByIdAsync((Guid)report.RequestId);
            getRequest.ReportId = report.Id;
            getRequest.Status = Status.InProgress;
            await _unit.RequestRepository.UpdateAsync(getRequest);
            return Result.SuccessWithObject(new { Message = "Report created successfully!", ReportId = report.Id });
        }

        public async Task<Result> UpdateAsync(ReportUpdateDTO dto)
        {
            var report = await _unit.ReportRepository.GetByIdAsync(dto.Id);
            if (report == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Report not found.", 0));
            _mapper.Map(dto, report);
            if (dto.ErrorIds != null)
            {
                report.ErrorDetails = dto.ErrorIds.Select(errorId => new ErrorDetail
                {
                    ReportId = report.Id,
                    ErrorId = errorId
                }).ToList();
            }
            await _unit.ReportRepository.UpdateReportAsync(report, dto.ErrorIds ?? new List<Guid>());
            return Result.SuccessWithObject(new { Message = "Report updated successfully!" });
        }


        public async Task<Result> DeleteAsync(Guid id)
        {
            var report = await _unit.ReportRepository.GetByIdAsync(id);
            if (report == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Report not found.", 0));
            report.IsDeleted = true;
            report.ModifiedDate = DateTime.Now;
            await _unit.ReportRepository.UpdateAsync(report);
            return Result.SuccessWithObject(new { Message = "Report canceled successfully!" });
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
                createLocation = TitleHelper.GenerateReportTitle(request.Device.Position.Zone.Area.AreaCode, request.Device.Position.Zone.ZoneCode, request.Device.Position.Index, request.Device.DeviceCode);
            }
            catch (Exception)
            {
                createLocation = "Create title fail";
            }

            // Tạo Report (ánh xạ thủ công)
            var report = new Report
            {
                Id = Guid.NewGuid(),
                RequestId = dto.RequestId,
                Location = createLocation,
                CreatedDate = DateTime.Now
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
                    if (existingIssueError == null) // Only add if it doesn't exist
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
            var getRequest = await _unit.RequestRepository.GetRequestByIdAsync((Guid)report.RequestId);
            getRequest.ReportId = report.Id;
            getRequest.Status = Status.InProgress;
            await _unit.RequestRepository.UpdateAsync(getRequest);

            await _unit.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "Report created successfully with IssueErrors!", ReportId = report.Id });
        }
        public async Task<Result> CreateReportWithIssueSymtomAsync(ReportCreateWithIssueSymtomDTO dto)
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
                dto.TechnicalSymtomIds = new List<Guid> { Guid.Parse("A1A1A1A1-1111-1111-1111-111111111111") };
                allSymtomIds.Add(Guid.Parse("A1A1A1A1-1111-1111-1111-111111111111"));

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
                CreatedDate = DateTime.Now
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


            // Fix for CS0019 and CS1001 errors
            var users = await _unit.UserRepository.GetUsersByRole(2);
            var systemUserId = users?.FirstOrDefault()?.Id ?? Guid.Parse("32222222-2222-2222-2222-222222222222");
            var result = await CreateWarrantyTaskGroup(report.Id, allSymtomIds.Distinct().ToList(), systemUserId);
            if (result.IsFailure)
            {
                return Result.SuccessWithObject(new { Message = $"Report created successfully but failed to create task!.{result.Error.Description}", ReportId = report.Id });
            }
            dynamic data = result.Object;

            Guid taskGroupId = data.taskGroupId;
            var createSchedulingResult = await CreateMechanicScheduleForWarranty(taskGroupId);
            if (createSchedulingResult.IsFailure)
            {
                return Result.SuccessWithObject(new { Message = $"Report created successfully but failed to auto-assign tasks!.{createSchedulingResult.Error.Description}", ReportId = report.Id });
            }


            return Result.SuccessWithObject(new { Message = "Report created successfully with IssueSymtoms!", ReportId = report.Id });
        }
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
                var currentTime = DateTime.Now;
                var availableMechanics = await _unit.UserRepository.GetRecommendedMechanicsAsync(currentTime, 1, 10);

                if (!availableMechanics.Any())
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", $"No more mechanic available."));
                }

                var primaryMechanic = availableMechanics.First(); // Best available mechanic
                var secondaryMechanic = availableMechanics.Count > 1 ? availableMechanics[1] : primaryMechanic;

                var newDeviceId = await _unit.DeviceRepository.GetDeviceByStatusAsync(DeviceStatus.Active);
                var deviceWarrantyId = await _unit.DeviceWarrantyRepository.GetDeviceWarrantyByDeviceIdForDevice(request.DeviceId);
                // Step 2: Create Warranty Submission Task using existing TaskService method
                var warrantyRequest = new CreateWarrantyTaskRequest
                {
                    RequestId = requestId,
                    AssigneeId = primaryMechanic.MechanicId, // Will be assigned later through auto-assignment
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

                // Step 3: Create Installation Task for replacement device using existing TaskService method
                var installRequest = new CreateInstallTaskRequest
                {
                    RequestId = requestId,
                    AssigneeId = secondaryMechanic.MechanicId, // Will be assigned later through auto-assignment
                    TaskGroupId = taskGroupId,
                    NewDeviceId = newDeviceId,
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


        private async Task<Result> CreateMechanicScheduleForWarranty(Guid taskGroupId)
        {
            try
            {
                // Get all suggested tasks in the task group
                var suggestedTasks = await _unit.TaskRepository.GetTasksByTaskGroupIdAsync(taskGroupId);
                var tasksToApply = suggestedTasks.Where(t => t.Status == Status.Pending).ToList();

                if (!tasksToApply.Any())
                {
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("NoSuggestedTasks", "No suggested tasks found in this task group.", 0));
                }

                // Get current shift for assignment
                var currentTime = DateTime.Now;
                var currentShift = await _unit.ShiftRepository.GetCurrentShiftAsync(currentTime);
                if (currentShift == null)
                {
                    currentShift = await _unit.ShiftRepository.GetNearestShiftAsync(currentTime);
                }

                if (currentShift == null)
                {
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("NoShiftAvailable", "No shift available for assignment.", 0));
                }

                // Get available mechanics using the existing recommendation system
                var availableMechanics = await _unit.UserRepository.GetRecommendedMechanicsAsync(currentTime, 1, 10);

                if (!availableMechanics.Any())
                {
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("NoAvailableMechanics", "No available mechanics for assignment.", 0));
                }

                // Apply the same assignment logic as AutoAssignedTask
                var uninstallTask = tasksToApply.FirstOrDefault(t => t.TaskType == TaskType.Uninstallation);
                var warrantyTask = tasksToApply.FirstOrDefault(t => t.TaskType == TaskType.WarrantySubmission);
                var installTask = tasksToApply.FirstOrDefault(t => t.TaskType == TaskType.Installation);

                // Select mechanics based on availability and performance
                var primaryMechanic = availableMechanics.First(); // Best available mechanic
                var secondaryMechanic = availableMechanics.Count > 1 ? availableMechanics[1] : primaryMechanic;

                var uninstallWarrantyMechanicId = primaryMechanic.MechanicId;
                var installMechanicId = secondaryMechanic.MechanicId;

                var appliedTasks = new List<object>();
                _unit.ClearChangeTracker();
                // Apply assignments to Uninstall and Warranty tasks (same mechanic)
                if (uninstallTask != null)
                {
                    uninstallTask.AssigneeId = uninstallWarrantyMechanicId;
                    uninstallTask.Status = Status.Pending;
                    uninstallTask.ModifiedDate = DateTime.Now;
                    uninstallTask.ExpectedTime = primaryMechanic.ExpectedTime;
                    await _unit.TaskRepository.UpdateAsync(uninstallTask);

                    // Create mechanic shift for uninstall task
                    var uninstallShiftResult = await _mechanicShiftService.CreateMechanicShiftAsync(uninstallWarrantyMechanicId, uninstallTask.Id);

                    appliedTasks.Add(new { TaskId = uninstallTask.Id, TaskType = "Uninstallation", MechanicId = uninstallWarrantyMechanicId });
                }
                _unit.ClearChangeTracker();
                if (warrantyTask != null)
                {
                    warrantyTask.AssigneeId = uninstallWarrantyMechanicId;
                    warrantyTask.Status = Status.Pending;
                    warrantyTask.ModifiedDate = DateTime.Now;
                    warrantyTask.ExpectedTime = primaryMechanic.ExpectedTime;
                    await _unit.TaskRepository.UpdateAsync(warrantyTask);

                    // Create mechanic shift for warranty task
                    var warrantyShiftResult = await _mechanicShiftService.CreateMechanicShiftAsync(uninstallWarrantyMechanicId, warrantyTask.Id);

                    appliedTasks.Add(new { TaskId = warrantyTask.Id, TaskType = "WarrantySubmission", MechanicId = uninstallWarrantyMechanicId });
                }
                _unit.ClearChangeTracker();
                // Apply assignment to Install task (different mechanic)
                if (installTask != null)
                {
                    installTask.AssigneeId = installMechanicId;
                    installTask.Status = Status.Pending;
                    installTask.ModifiedDate = DateTime.Now;
                    installTask.ExpectedTime = secondaryMechanic.ExpectedTime;
                    await _unit.TaskRepository.UpdateAsync(installTask);

                    // Create mechanic shift for install task
                    var installShiftResult = await _mechanicShiftService.CreateMechanicShiftAsync(installMechanicId, installTask.Id);

                    appliedTasks.Add(new { TaskId = installTask.Id, TaskType = "Installation", MechanicId = installMechanicId });
                }

                await _unit.SaveChangesAsync();

                return Result.SuccessWithObject(new
                {
                    Message = "Suggested task assignments applied successfully!",
                    TaskGroupId = taskGroupId,
                    AppliedTasks = appliedTasks,
                    PrimaryMechanicId = uninstallWarrantyMechanicId,
                    SecondaryMechanicId = installMechanicId
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("AssignmentError", $"Failed to apply suggested assignments: {ex.Message}", 0));
            }
        }



        private async Task AutoAssignedTask(Guid taskGroupId)
        {
            try
            {
                _unit.ClearChangeTracker();
                // Get all tasks in the task group
                var tasks = await _unit.TaskRepository.GetTasksByTaskGroupIdAsync(taskGroupId);

                if (!tasks.Any())
                {
                    throw new InvalidOperationException("No tasks found in the task group");
                }

                // Get available mechanics using the existing recommendation system
                var currentTime = DateTime.Now;
                var availableMechanics = await _unit.UserRepository.GetRecommendedMechanicsAsync(currentTime, 1, 10);

                if (!availableMechanics.Any())
                {
                    throw new InvalidOperationException("No available mechanics for auto-assignment");
                }

                // Find specific task types (each task group should have only 1 of each type)
                var uninstallTask = tasks.FirstOrDefault(t => t.TaskType == TaskType.Uninstallation);
                var warrantyTask = tasks.FirstOrDefault(t => t.TaskType == TaskType.WarrantySubmission);
                var installTask = tasks.FirstOrDefault(t => t.TaskType == TaskType.Installation);

                // Select mechanics based on availability and performance
                var primaryMechanic = availableMechanics.First(); // Best available mechanic
                var secondaryMechanic = availableMechanics.Count > 1 ? availableMechanics[1] : primaryMechanic;

                var uninstallWarrantyMechanicId = primaryMechanic.MechanicId;
                var installMechanicId = secondaryMechanic.MechanicId;

                // Assign same mechanic to Uninstall and Warranty tasks
                if (uninstallTask != null && !uninstallTask.AssigneeId.HasValue)
                {
                    uninstallTask.AssigneeId = uninstallWarrantyMechanicId;
                    uninstallTask.ModifiedDate = DateTime.Now;
                    uninstallTask.ExpectedTime = primaryMechanic.ExpectedTime;
                    await _unit.TaskRepository.UpdateAsync(uninstallTask);

                    // Create mechanic shift for uninstall task
                    await _mechanicShiftService.CreateMechanicShiftAsync(uninstallWarrantyMechanicId, uninstallTask.Id);
                }
                _unit.ClearChangeTracker();
                if (warrantyTask != null && !warrantyTask.AssigneeId.HasValue)
                {
                    warrantyTask.AssigneeId = uninstallWarrantyMechanicId;
                    warrantyTask.ModifiedDate = DateTime.Now;
                    warrantyTask.ExpectedTime = primaryMechanic.ExpectedTime;
                    await _unit.TaskRepository.UpdateAsync(warrantyTask);

                    // Create mechanic shift for warranty task
                    await _mechanicShiftService.CreateMechanicShiftAsync(uninstallWarrantyMechanicId, warrantyTask.Id);
                }
                _unit.ClearChangeTracker();
                // Assign different mechanic to Install task
                if (installTask != null && !installTask.AssigneeId.HasValue)
                {
                    installTask.AssigneeId = installMechanicId;
                    installTask.ModifiedDate = DateTime.Now;
                    installTask.ExpectedTime = secondaryMechanic.ExpectedTime;
                    await _unit.TaskRepository.UpdateAsync(installTask);

                    // Create mechanic shift for install task
                    await _mechanicShiftService.CreateMechanicShiftAsync(installMechanicId, installTask.Id);
                }

                await _unit.SaveChangesAsync();

                // Return the primary mechanic ID (uninstall/warranty mechanic)

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to auto-assign tasks: {ex.Message}", ex);
            }
        }


    }
}
