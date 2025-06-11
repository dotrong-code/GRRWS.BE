using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Task;
using GRRWS.Infrastructure.DTOs.Task.Get;
using GRRWS.Infrastructure.DTOs.Task.Get.SubObject;
using GRRWS.Infrastructure.DTOs.Task.Repair;
using GRRWS.Infrastructure.DTOs.Task.Warranty;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class TaskRepository : GenericRepository<Tasks>, ITaskRepository
    {
        public TaskRepository(GRRWSContext context) : base(context) { }

        public async Task<List<TaskByReportResponse>> GetTasksByReportIdAsync(Guid reportId)
        {
            return await _context.ErrorDetails
                .Where(ed => ed.ReportId == reportId && ed.TaskId.HasValue && !ed.Task.IsDeleted)
                .OrderByDescending(ed => ed.Task.Priority)
                .ThenBy(ed => ed.Task.StartTime ?? DateTime.MaxValue)
                .Select(ed => new TaskByReportResponse
                {
                    TaskId = ed.TaskId.Value,
                    TaskType = ed.Task.TaskType,
                    Priority = (int)ed.Task.Priority, // Convert enum to int
                    Status = ed.Task.Status.ToString(), // Convert enum to string
                    AssigneeName = ed.Task.Assignee.FullName,
                    StartTime = ed.Task.StartTime
                })
                .ToListAsync();
        }
        public async Task<List<GetTaskResponse>> GetTasksByMechanicIdAsync(Guid mechanicId, int pageNumber, int pageSize)
        {
            var query = _context.Tasks
                .Where(t => t.AssigneeId == mechanicId && !t.IsDeleted)
                .OrderByDescending(t => t.CreatedDate);

            var totalCount = await query.CountAsync();
            var tasks = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new GetTaskResponse
                {
                    Id = t.Id,
                    TaskName = t.TaskName,
                    TaskDescription = t.TaskDescription,
                    TaskType = t.TaskType,
                    StartTime = t.StartTime,
                    ExpectedTime = t.ExpectedTime,
                    EndTime = t.EndTime,
                    AssigneeId = t.AssigneeId,
                    AssigneeName = t.Assignee.FullName,
                    DeviceReturnTime = t.DeviceReturnTime,
                    DeviceCondition = t.DeviceCondition,
                    ReportNotes = t.ReportNotes,
                    RepairSpareparts = t.RepairSpareparts.Select(rs => new RepairSparepartDto
                    {
                        SpareId = rs.SpareId,
                        SparepartName = rs.Sparepart.SparepartName
                    }).ToList(),
                    ErrorDetails = t.ErrorDetails.Select(ed => new ErrorDetailDto
                    {
                        ErrorId = ed.ErrorId,
                        ErrorCode = ed.Error.ErrorCode
                    }).ToList()
                })
                .ToListAsync();

            return tasks;
        }
        public async Task<GetTaskResponse> GetTaskDetailsAsync(Guid taskId)
        {
            return await _context.Tasks
                .Where(t => t.Id == taskId && !t.IsDeleted)
                .Select(t => new GetTaskResponse
                {
                    Id = t.Id,
                    TaskName = t.TaskName,
                    TaskDescription = t.TaskDescription,
                    TaskType = t.TaskType,
                    StartTime = t.StartTime,
                    ExpectedTime = t.ExpectedTime,
                    EndTime = t.EndTime,
                    AssigneeId = t.AssigneeId,
                    AssigneeName = t.Assignee.FullName,
                    DeviceReturnTime = t.DeviceReturnTime,
                    DeviceCondition = t.DeviceCondition,
                    ReportNotes = t.ReportNotes,
                    RepairSpareparts = t.RepairSpareparts.Select(rs => new RepairSparepartDto
                    {
                        SpareId = rs.SpareId,
                        SparepartName = rs.Sparepart.SparepartName
                    }).ToList(),
                    ErrorDetails = t.ErrorDetails.Select(ed => new ErrorDetailDto
                    {
                        ErrorId = ed.ErrorId,
                        ErrorCode = ed.Error.ErrorCode
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }
        public async Task<List<Tasks>> GetAllTasksAsync()
        {
            return await _context.Tasks
                .Include(t => t.Assignee)
                .Include(t => t.ErrorDetails).ThenInclude(ed => ed.Error)
                .Include(t => t.RepairSpareparts).ThenInclude(rs => rs.Sparepart)
                .ToListAsync();
        }
        public async Task<Tasks> GetTaskByIdAsync(Guid id)
        {
            return await _context.Tasks
                .Include(t => t.Assignee)
                .Include(t => t.ErrorDetails).ThenInclude(ed => ed.Error)
                .Include(t => t.RepairSpareparts).ThenInclude(rs => rs.Sparepart)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task CreateTaskAsync(Tasks task, Guid reportId, List<Guid> errorIds, List<Guid> sparepartIds)
        {
            // Add task
            await _context.Tasks.AddAsync(task);

            // Link Errors via ErrorDetails
            if (errorIds != null && errorIds.Any())
            {
                // Fetch the report
                var report = await _context.Reports
                    .FirstOrDefaultAsync(r => r.Id == reportId);
                if (report == null)
                    throw new Exception("No report found to link errors.");

                // Deduplicate errorIds to prevent duplicate ErrorDetail entries
                var uniqueErrorIds = errorIds.Distinct().ToList();

                // Check for existing ErrorDetails to avoid duplicates
                var existingErrorDetails = await _context.ErrorDetails
                    .Where(ed => ed.ReportId == reportId && uniqueErrorIds.Contains(ed.ErrorId))
                    .Select(ed => ed.ErrorId)
                    .ToListAsync();

                // Filter out errorIds that already exist
                var newErrorIds = uniqueErrorIds.Except(existingErrorDetails).ToList();

                // Create new ErrorDetail records only for non-existing pairs
                var errorDetails = newErrorIds.Select(errorId => new ErrorDetail
                {
                    ReportId = report.Id,
                    ErrorId = errorId,
                    TaskId = task.Id
                }).ToList();

                if (errorDetails.Any())
                {
                    await _context.ErrorDetails.AddRangeAsync(errorDetails);
                }
            }

            // Link Spareparts via RepairSpareparts
            if (sparepartIds != null && sparepartIds.Any())
            {
                // Deduplicate sparepartIds to prevent duplicate RepairSparepart entries
                var uniqueSparepartIds = sparepartIds.Distinct().ToList();

                var repairSpareparts = uniqueSparepartIds.Select(sparepartId => new RepairSparepart
                {
                    SpareId = sparepartId,
                    TaskId = task.Id
                }).ToList();
                await _context.RepairSpareparts.AddRangeAsync(repairSpareparts);
            }

            // Save all changes
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTaskAsync(Tasks task, List<Guid> errorIds, List<Guid> sparepartIds)
        {
            var existingTask = await _context.Tasks
                .Include(t => t.ErrorDetails)
                .Include(t => t.RepairSpareparts)
                .FirstOrDefaultAsync(t => t.Id == task.Id);

            if (existingTask == null)
                throw new Exception("Task not found.");

            // Update task properties
            _context.Entry(existingTask).CurrentValues.SetValues(task);

            // Update ErrorDetails
            var existingErrorDetails = existingTask.ErrorDetails.ToList();
            _context.ErrorDetails.RemoveRange(existingErrorDetails);

            if (errorIds != null && errorIds.Any())
            {
                var report = await _context.Reports.FirstOrDefaultAsync(); // Placeholder; adjust as needed
                if (report == null)
                    throw new Exception("No report found to link errors.");

                var newErrorDetails = errorIds.Select(errorId => new ErrorDetail
                {
                    ReportId = report.Id,
                    ErrorId = errorId,
                    TaskId = task.Id
                }).ToList();
                await _context.ErrorDetails.AddRangeAsync(newErrorDetails);
            }

            // Update RepairSpareparts
            var existingRepairSpareparts = existingTask.RepairSpareparts.ToList();
            _context.RepairSpareparts.RemoveRange(existingRepairSpareparts);

            if (sparepartIds != null && sparepartIds.Any())
            {
                var newRepairSpareparts = sparepartIds.Select(sparepartId => new RepairSparepart
                {
                    SpareId = sparepartId,
                    TaskId = task.Id
                }).ToList();
                await _context.RepairSpareparts.AddRangeAsync(newRepairSpareparts);
            }

            await _context.SaveChangesAsync();
        }
        public async Task UpdateErrorDetailsAsync(List<Guid> errorDetailIds, Guid taskId)
        {
            if (errorDetailIds == null || !errorDetailIds.Any())
                return;

            var errorDetails = await _context.ErrorDetails
                .Where(ed => errorDetailIds.Contains(ed.ErrorId) && !ed.TaskId.HasValue)
                .ToListAsync();

            foreach (var errorDetail in errorDetails)
            {
                errorDetail.TaskId = taskId;
            }

            _context.ErrorDetails.UpdateRange(errorDetails);
            await _context.SaveChangesAsync();
        }
        public async Task<(List<GetTaskResponse> Tasks, int TotalCount)> GetAllTasksAsync(string? taskType, string? status, int? priority, int pageNumber, int pageSize)
        {
            var query = _context.Tasks
                .Where(t => !t.IsDeleted);

            if (!string.IsNullOrWhiteSpace(taskType))
                query = query.Where(t => t.TaskType == taskType);

            if (!string.IsNullOrWhiteSpace(status))

                if (priority.HasValue)

                    query = query.OrderByDescending(t => t.CreatedDate);

            var totalCount = await query.CountAsync();
            var tasks = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new GetTaskResponse
                {
                    Id = t.Id,
                    TaskName = t.TaskName,
                    TaskDescription = t.TaskDescription,
                    TaskType = t.TaskType,
                    StartTime = t.StartTime,
                    ExpectedTime = t.ExpectedTime,
                    EndTime = t.EndTime,
                    AssigneeId = t.AssigneeId,
                    AssigneeName = t.Assignee.FullName,
                    DeviceReturnTime = t.DeviceReturnTime,
                    DeviceCondition = t.DeviceCondition,
                    ReportNotes = t.ReportNotes,
                    RepairSpareparts = t.RepairSpareparts.Select(rs => new RepairSparepartDto
                    {
                        SpareId = rs.SpareId,
                        SparepartName = rs.Sparepart.SparepartName
                    }).ToList(),
                    ErrorDetails = t.ErrorDetails.Select(ed => new ErrorDetailDto
                    {
                        ErrorId = ed.ErrorId,
                        ErrorCode = ed.Error.ErrorCode
                    }).ToList()
                })
                .ToListAsync();

            return (tasks, totalCount);
        }
        public async Task<Guid> CreateTaskWebAsync(CreateTaskWeb dto)
        {
            var reportId = await _context.Requests
                .Where(r => r.Id == dto.RequestId)
                .Select(r => r.ReportId)
                .FirstOrDefaultAsync();

            var task = new Tasks
            {
                Id = Guid.NewGuid(),
                TaskType = dto.TaskType,
                TaskName = "Task for " + dto.TaskType,
                StartTime = dto.StartDate,
                Priority = Priority.Low, // Use enum value
                Status = Status.Pending, // Use enum value
                EndTime = DateTime.UtcNow.AddDays(7),
                ExpectedTime = DateTime.UtcNow.AddDays(1),
                TaskDescription = "This is description",
                AssigneeId = dto.AssigneeId,
                CreatedDate = DateTime.UtcNow,
                DeviceReturnTime = DateTime.UtcNow.AddDays(1),
                ReportNotes = "Report notes here",
                DeviceCondition = "New",
                IsDeleted = false,
            };

            await _context.Tasks.AddAsync(task);

            // Handle ErrorDetails (existing code remains the same)
            if (dto.ErrorIds != null && dto.ErrorIds.Count > 0)
            {
                var existingErrorDetails = await _context.ErrorDetails
                    .Where(ed => ed.ReportId == reportId && dto.ErrorIds.Contains(ed.ErrorId))
                    .ToListAsync();

                var existingErrorIds = existingErrorDetails.Select(ed => ed.ErrorId).ToHashSet();

                foreach (var ed in existingErrorDetails)
                {
                    ed.TaskId = task.Id;
                }
                if (existingErrorDetails.Count > 0)
                    _context.ErrorDetails.UpdateRange(existingErrorDetails);

                var newErrorDetails = dto.ErrorIds
                    .Where(eid => !existingErrorIds.Contains(eid))
                    .Select(eid => new ErrorDetail
                    {
                        ReportId = (Guid)reportId,
                        ErrorId = eid,
                        TaskId = task.Id
                    }).ToList();

                if (newErrorDetails.Count > 0)
                    await _context.ErrorDetails.AddRangeAsync(newErrorDetails);
            }

            // Handle RepairSpareparts (existing code remains the same)
            if (dto.SparepartIds != null && dto.SparepartIds.Count > 0)
            {
                var repairSpareparts = dto.SparepartIds
                    .Select(spid => new RepairSparepart
                    {
                        TaskId = task.Id,
                        SpareId = spid
                    }).ToList();

                await _context.RepairSpareparts.AddRangeAsync(repairSpareparts);
            }

            await _context.SaveChangesAsync();
            return task.Id;
        }
        public async Task<Guid> CreateSimpleTaskWebAsync(CreateSimpleTaskWeb dto)
        {
            var reportId = await _context.Requests
                .Where(r => r.Id == dto.RequestId)
                .Select(r => r.ReportId)
                .FirstOrDefaultAsync();

            var task = new Tasks
            {
                Id = Guid.NewGuid(),
                TaskType = dto.TaskType,
                StartTime = dto.StartDate,
                Status = Status.Pending, // Use enum value
                Priority = Priority.Medium, // Use enum value
                TaskDescription = "This is description",
                AssigneeId = dto.AssigneeId,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            await _context.Tasks.AddAsync(task);

            if (dto.SparepartIds != null && dto.SparepartIds.Count > 0)
            {
                var repairSpareparts = dto.SparepartIds.Select(sparepartId => new RepairSparepart
                {
                    TaskId = task.Id,
                    SpareId = sparepartId
                }).ToList();

                await _context.RepairSpareparts.AddRangeAsync(repairSpareparts);
            }

            await _context.SaveChangesAsync();
            return task.Id;
        }
        public async Task<Guid> CreateTaskFromErrorsAsync(CreateTaskFromErrorsRequest request)
        {
            var reportId = await _context.Requests
                .Where(r => r.Id == request.RequestId)
                .Select(r => r.ReportId)
                .FirstOrDefaultAsync();

            var task = new Tasks
            {
                Id = Guid.NewGuid(),
                TaskType = request.TaskType,
                StartTime = request.StartDate,
                Status = Status.Pending, // Use enum value
                Priority = Priority.Medium, // Use enum value
                TaskDescription = "Task created from error analysis",
                AssigneeId = request.AssigneeId,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            await _context.Tasks.AddAsync(task);

            // Handle ErrorDetails
            if (request.ErrorIds != null && request.ErrorIds.Count > 0)
            {
                var existingErrorDetails = await _context.ErrorDetails
                    .Where(ed => ed.ReportId == reportId && request.ErrorIds.Contains(ed.ErrorId))
                    .ToListAsync();

                var existingErrorIds = existingErrorDetails.Select(ed => ed.ErrorId).ToHashSet();

                foreach (var ed in existingErrorDetails)
                {
                    ed.TaskId = task.Id;
                }
                if (existingErrorDetails.Count > 0)
                    _context.ErrorDetails.UpdateRange(existingErrorDetails);

                var newErrorDetails = request.ErrorIds
                    .Where(eid => !existingErrorIds.Contains(eid))
                    .Select(eid => new ErrorDetail
                    {
                        ReportId = (Guid)reportId,
                        ErrorId = eid,
                        TaskId = task.Id
                    }).ToList();

                if (newErrorDetails.Count > 0)
                    await _context.ErrorDetails.AddRangeAsync(newErrorDetails);
            }

            // Handle RepairSpareparts
            if (request.SparepartIds != null && request.SparepartIds.Count > 0)
            {
                var repairSpareparts = request.SparepartIds
                    .Select(spid => new RepairSparepart
                    {
                        TaskId = task.Id,
                        SpareId = spid
                    }).ToList();

                await _context.RepairSpareparts.AddRangeAsync(repairSpareparts);
            }

            await _context.SaveChangesAsync();
            return task.Id;
        }
        public async Task<Guid> CreateTaskFromTechnicalIssueAsync(CreateTaskFromTechnicalIssueRequest request)
        {
            var reportId = await _context.Requests
                .Where(r => r.Id == request.RequestId)
                .Select(r => r.ReportId)
                .FirstOrDefaultAsync();

            var task = new Tasks
            {
                Id = Guid.NewGuid(),
                TaskType = request.TaskType,
                StartTime = request.StartDate,
                Status = Status.Pending, // Use enum value
                Priority = Priority.High, // Use enum value for warranty tasks
                TaskDescription = "Warranty task created from technical issue",
                AssigneeId = request.AssigneeId,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            await _context.Tasks.AddAsync(task);

            if (request.TechnicalIssueIds != null && request.TechnicalIssueIds.Count > 0)
            {
                var existingReports = await _context.TechnicalSymptomReports
                    .Where(tsr => tsr.ReportId == reportId &&
                           request.TechnicalIssueIds.Contains(tsr.TechnicalSymptomId))
                    .ToListAsync();

                var existingTechnicalSymptomIds = existingReports.Select(tsr => tsr.TechnicalSymptomId).ToHashSet();

                foreach (var report in existingReports)
                {
                    report.TaskId = task.Id;
                }

                if (existingReports.Any())
                    _context.TechnicalSymptomReports.UpdateRange(existingReports);

                var newTechnicalSymptomReports = request.TechnicalIssueIds
                    .Where(id => !existingTechnicalSymptomIds.Contains(id))
                    .Select(issueId => new TechnicalSymptomReport
                    {
                        TaskId = task.Id,
                        TechnicalSymptomId = issueId,
                        ReportId = (Guid)reportId
                    })
                    .ToList();

                if (newTechnicalSymptomReports.Any())
                    await _context.TechnicalSymptomReports.AddRangeAsync(newTechnicalSymptomReports);
            }

            await _context.SaveChangesAsync();
            return task.Id;
        }
        public async Task<Guid> CreateSimpleTaskAsync(CreateSimpleTaskRequest request)
        {
            var reportId = await _context.Requests
                .Where(r => r.Id == request.RequestId)
                .Select(r => r.ReportId)
                .FirstOrDefaultAsync();

            var actions = new List<string>();
            if (request.BringDeviceToRepairPlace)
                actions.Add("Remove faulty device and bring to repair facility");
            if (request.SetupReplacementDevice)
                actions.Add("Install and configure replacement device");

            var taskDescription = request.TaskDescription ??
                $"Device replacement task: {string.Join("; ", actions)}";

            var task = new Tasks
            {
                Id = Guid.NewGuid(),
                TaskType = request.TaskType,
                StartTime = request.StartDate,
                Status = Status.Pending, // Use enum value
                Priority = Priority.Medium, // Use enum value
                TaskDescription = taskDescription,
                AssigneeId = request.AssigneeId,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task.Id;
        }

        public async Task<Guid> CreateWarrantyTask(CreateWarrantyTaskRequest request, Guid userId)
        {
            var executionStrategy = _context.Database.CreateExecutionStrategy();

            return await executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Get report ID from request
                    var reportId = await _context.Requests
                        .Where(r => r.Id == request.RequestId)
                        .Select(r => r.ReportId)
                        .FirstOrDefaultAsync();

                    if (reportId == Guid.Empty)
                        throw new Exception("No report found for this request.");

                    // Validate device warranty exists
                    var deviceWarranty = await _context.DeviceWarranties
                        .FirstOrDefaultAsync(dw => dw.Id == request.DeviceWarrantyId);
                    if (deviceWarranty == null)
                        throw new Exception("Device warranty not found.");

                    // Get technical symptom names to create issue description
                    string issueDescription = "";
                    if (request.TechnicalIssueIds != null && request.TechnicalIssueIds.Any())
                    {
                        var technicalSymptomNames = await _context.TechnicalSymptoms
                            .Where(ts => request.TechnicalIssueIds.Contains(ts.Id))
                            .Select(ts => ts.Name)
                            .ToListAsync();

                        if (technicalSymptomNames.Any())
                        {
                            issueDescription = string.Join(", ", technicalSymptomNames.Where(name => !string.IsNullOrEmpty(name)));
                        }
                    }

                    // Generate claim number with concurrency handling
                    string claimNumber;
                    int claimCount;
                    // Lock the WarrantyClaims table to prevent concurrent claim number generation
                    await using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "SELECT COUNT(*) FROM WarrantyClaims WITH (UPDLOCK, HOLDLOCK)";
                        command.Transaction = transaction.GetDbTransaction();
                        await _context.Database.OpenConnectionAsync();
                        claimCount = (int)await command.ExecuteScalarAsync();
                    }
                    claimNumber = $"WC-{DateTime.UtcNow:yyyyMM}-{(claimCount + 1):D3}";

                    // Create warranty claim (without SubmittedByTaskId)
                    var warrantyClaim = new WarrantyClaim
                    {
                        Id = Guid.NewGuid(),
                        ClaimNumber = claimNumber,
                        ClaimStatus = Status.Pending,
                        IssueDescription = issueDescription,
                        DeviceWarrantyId = request.DeviceWarrantyId,
                        CreatedByUserId = request.AssigneeId,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        SubmittedByTaskId = null // Explicitly null to avoid circular dependency
                    };

                    await _context.WarrantyClaims.AddAsync(warrantyClaim);
                    await _context.SaveChangesAsync(); // Save WarrantyClaim first

                    // Create the warranty submission task
                    var task = new Tasks
                    {
                        Id = Guid.NewGuid(),
                        TaskName = $"Đưa thiết bị đi bảo hành - {claimNumber}",
                        TaskType = "WarrantySubmission",
                        TaskDescription = $"Mang thiết bị đi bảo hành với cái triệu chứng:{issueDescription}",
                        StartTime = request.StartDate,
                        ExpectedTime = request.StartDate.AddHours(5),
                        Status = Status.Pending,
                        Priority = Priority.High,
                        AssigneeId = request.AssigneeId,
                        WarrantyClaimId = warrantyClaim.Id,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false
                    };
                    await _context.Tasks.AddAsync(task);

                    // Update warranty claim with submission task ID
                    warrantyClaim.SubmittedByTaskId = task.Id;
                    _context.WarrantyClaims.Update(warrantyClaim);

                    // Link technical symptoms to the task through TechnicalSymptomReports
                    if (request.TechnicalIssueIds != null && request.TechnicalIssueIds.Any())
                    {
                        var existingSymptomReports = await _context.TechnicalSymptomReports
                            .Where(tsr => tsr.ReportId == reportId &&
                                          request.TechnicalIssueIds.Contains(tsr.TechnicalSymptomId))
                            .ToListAsync();

                        foreach (var report in existingSymptomReports)
                        {
                            report.TaskId = task.Id;
                        }

                        if (existingSymptomReports.Any())
                            _context.TechnicalSymptomReports.UpdateRange(existingSymptomReports);
                    }

                    await _context.SaveChangesAsync(); // Save Tasks and updated WarrantyClaim
                    await transaction.CommitAsync();

                    return task.Id;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
                finally
                {
                    await _context.Database.CloseConnectionAsync();
                }
            });
        }

        public async Task<List<GetTaskForMechanic>> GetTasksByMechanicIdAsync2(Guid mechanicId, int pageNumber, int pageSize)
        {
            var query = _context.Tasks
                .Where(t => t.AssigneeId == mechanicId && !t.IsDeleted)
                .OrderByDescending(t => t.CreatedDate);

            var totalCount = await query.CountAsync();
            var tasks = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new GetTaskForMechanic
                {
                    TaskId = t.Id,
                    TaskName = t.TaskName,
                    TaskDescription = t.TaskDescription,
                    TaskType = t.TaskType,
                    Priority = t.Priority.ToString(), // Convert enum to string
                    Status = t.Status.ToString(),
                    CreateDate = t.CreatedDate// Convert enum to string

                })
                .ToListAsync();

            return tasks;
        }

        public async Task<GetDetailWarrantyTaskForMechanic> GetGetDetailWarrantyTaskForMechanicByIdAsync(Guid taskId, string type)
        {
            var task = await _context.Tasks
                .Include(t => t.Assignee)

                .Include(t => t.WarrantyClaim)
                    .ThenInclude(wc => wc.DeviceWarranty)
                .Include(t => t.WarrantyClaim)
                    .ThenInclude(u => u.CreatedByUser)
                .Where(t => t.Id == taskId && !t.IsDeleted && t.TaskType == type)
                .FirstOrDefaultAsync();

            if (task == null)
                return null;

            return new GetDetailWarrantyTaskForMechanic
            {
                TaskId = task.Id,
                TaskType = task.TaskType,
                DeviceId = task.WarrantyClaim?.DeviceWarranty?.Id ?? Guid.Empty,
                TaskName = task.TaskName,
                WarrantyProvider = task.WarrantyClaim?.DeviceWarranty?.Provider,
                WarrantyCode = task.WarrantyClaim?.DeviceWarranty?.WarrantyCode,
                TaskDescription = task.TaskDescription,
                Priority = task.Priority.ToString(),
                Status = task.Status.ToString(),
                StartTime = task.StartTime,
                ExpectedTime = task.ExpectedTime,
                EndTime = task.EndTime,
                AssigneeName = task.Assignee?.FullName,
                ClaimNumber = task.WarrantyClaim?.ClaimNumber,
                ClaimStatus = task.WarrantyClaim?.ClaimStatus.ToString(),
                StartDate = task.WarrantyClaim?.CreatedDate,
                ExpectedReturnDate = task.WarrantyClaim?.ExpectedReturnDate,
                ActualReturnDate = task.WarrantyClaim?.ActualReturnDate,
                // Location = task.WarrantyClaim?.DeviceWarranty?.Location,
                Location = "28 Bui Thi Xuan,Thanh Pho Ho Chi Minh",
                Resolution = task.WarrantyClaim?.Resolution,
                IssueDescription = task.WarrantyClaim?.IssueDescription,
                WarrantyNotes = task.WarrantyClaim?.WarrantyNotes,
                ClaimAmount = task.WarrantyClaim?.ClaimAmount,
                ContractNumber = task.WarrantyClaim?.ContractNumber,
                HotNumber = task.WarrantyClaim?.CreatedByUser?.PhoneNumber // Assuming CreatedByUser has PhoneNumber property
            };
        }

        public async Task<GetDetailtRepairTaskForMechanic> GetDetailtRepairTaskForMechanicByIdAsync(Guid taskId, string type)
        {
            var task = await _context.Tasks
                .Include(t => t.Assignee)
                .Include(t => t.ErrorDetails)
                    .ThenInclude(ed => ed.Error)
                        .ThenInclude(e => e.ErrorGuidelines)
                .Include(t => t.ErrorDetails)
                    .ThenInclude(ed => ed.ProgressRecords)
                        .ThenInclude(pr => pr.ErrorFixStep)
                .Include(t => t.ErrorDetails)
                    .ThenInclude(ed => ed.RequestTakeSparePartUsage)
                        .ThenInclude(rtspu => rtspu.SparePartUsages) // Fixed: Access the collection
                            .ThenInclude(spu => spu.SparePart) // Then access SparePart from SparePartUsage

                .Where(t => t.Id == taskId && !t.IsDeleted && t.TaskType == type)
                .FirstOrDefaultAsync();
            var reportId = task.ErrorDetails.FirstOrDefault()?.ReportId;

            if (reportId == null)
            {
                throw new Exception("ReportId not found in ErrorDetails.");
            }

            var deviceId = await _context.Requests
                .Where(r => r.ReportId == reportId)
                .Select(r => r.DeviceId)
                .FirstOrDefaultAsync();

            if (task == null)
                return null;

            var errorDetails = task.ErrorDetails.Select(ed => new ErrorDetailOfTask
            {
                ErrorId = ed.ErrorId,
                ErrorDetailId = ed.Id,
                ErrorName = ed.Error?.Name,
                ErrorGuildelineTitle = ed.Error?.ErrorGuidelines?.FirstOrDefault()?.Title,
                ErrorFixProgress = ed.ProgressRecords?.Select(pr => new ErrorFixProgressDTO
                {
                    ErrorFixProgressId = pr.Id,
                    StepDescription = pr.ErrorFixStep?.StepDescription,
                    StepOrder = pr.ErrorFixStep?.StepOrder ?? 0,
                    IsCompleted = pr.IsCompleted,
                    CompletedAt = pr.CompletedAt,
                }).OrderBy(dto => dto.StepOrder).ToList() ?? new List<ErrorFixProgressDTO>(),
                SparePartUsages = ed.RequestTakeSparePartUsage?.SparePartUsages?.Select(spu => new SparePartUsageDTO
                {
                    SparePartUsageId = spu.Id,
                    SparePartId = spu.SparePartId,
                    SparePartName = spu.SparePart?.SparepartName,
                    QuantityUsed = spu.QuantityUsed,
                    IsTakenFromStock = spu.IsTakenFromStock,
                }).ToList() ?? new List<SparePartUsageDTO>() // Fixed: Map the collection properly
            }).ToList();

            return new GetDetailtRepairTaskForMechanic
            {
                TaskId = task.Id,
                TaskType = task.TaskType,
                DeviceId = deviceId,
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                Priority = task.Priority.ToString(),
                Status = task.Status.ToString(),
                StartTime = task.StartTime,
                ExpectedTime = task.ExpectedTime,
                EndTime = task.EndTime,
                AssigneeName = task.Assignee?.FullName,
                ErrorDetails = errorDetails
            };
        }

        public Task<GetDetailReplaceTaskForMechanic> GetDetailReplaceTaskForMechanicByIdAsync(Guid taskId, string type)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> FillInWarrantyTask(FillInWarrantyTask request)
        {
            // Get the task with its warranty claim
            var task = await _context.Tasks
                .Include(t => t.WarrantyClaim)
                    .ThenInclude(wc => wc.DeviceWarranty)
                .FirstOrDefaultAsync(t => t.Id == request.TaskId && !t.IsDeleted);

            if (task == null)
                throw new Exception("Task not found.");

            if (task.WarrantyClaim == null)
                throw new Exception("Warranty claim not found for this task.");

            // Update task completion details
            task.EndTime = DateTime.UtcNow;
            task.Status = Status.Completed;
            task.ModifiedDate = DateTime.UtcNow;

            // Update warranty claim details
            var warrantyClaim = task.WarrantyClaim;
            warrantyClaim.ExpectedReturnDate = DateTime.UtcNow.Add(request.WarrantyTime);
            warrantyClaim.Resolution = request.Resolution;
            warrantyClaim.ContractNumber = request.ContractNumber;
            warrantyClaim.ClaimStatus = Status.InProgress;
            warrantyClaim.ModifiedDate = DateTime.UtcNow;

            // Create return task for collecting the device from warranty
            var returnTask = new Tasks
            {
                Id = Guid.NewGuid(),
                TaskName = $"Nhận thiết bị từ bảo hành - {warrantyClaim.ClaimNumber}",
                TaskType = "WarrantyReturn",
                TaskDescription = $"Collect device from warranty provider for claim: {warrantyClaim.ClaimNumber}. Expected return date: {warrantyClaim.ExpectedReturnDate:dd/MM/yyyy}",
                StartTime = warrantyClaim.ExpectedReturnDate,
                ExpectedTime = warrantyClaim.ExpectedReturnDate?.AddHours(2),
                Status = Status.Pending,
                Priority = Priority.Medium,
                AssigneeId = task.AssigneeId, // Assign to same technician
                WarrantyClaimId = warrantyClaim.Id,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            // Add return task to context
            await _context.Tasks.AddAsync(returnTask);

            // Update warranty claim with return task ID
            warrantyClaim.ReturnTaskId = returnTask.Id;

            // Update entities
            _context.Tasks.Update(task);
            _context.WarrantyClaims.Update(warrantyClaim);

            await _context.SaveChangesAsync();

            return task.Id;
        }

        public async Task<Guid> CreateRepairTask(CreateRepairTaskRequest request, Guid userId)
        {
            var executionStrategy = _context.Database.CreateExecutionStrategy();

            return await executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Get report and device information from request
                    var requestInfo = await _context.Requests
                        .Include(r => r.Device)
                        .Where(r => r.Id == request.RequestId)
                        .Select(r => new { r.ReportId, DeviceName = r.Device.DeviceName })
                        .FirstOrDefaultAsync();

                    if (requestInfo == null)
                        throw new Exception("No request found.");

                    // Validate error guidelines exist
                    var errorGuidelines = await _context.ErrorGuidelines
                        .Include(eg => eg.ErrorFixSteps)
                        .Include(eg => eg.Error)
                        .Where(eg => request.ErrorGuidelineIds.Contains(eg.Id))
                        .ToListAsync();

                    if (!errorGuidelines.Any())
                        throw new Exception("No error guidelines found.");

                    // Calculate total expected time from all error guidelines
                    var totalExpectedTime = TimeSpan.Zero;
                    foreach (var guideline in errorGuidelines)
                    {
                        if (guideline.EstimatedRepairTime.HasValue)
                        {
                            totalExpectedTime = totalExpectedTime.Add(guideline.EstimatedRepairTime.Value);
                        }
                    }

                    // If no estimated time found, default to 8 hours
                    if (totalExpectedTime == TimeSpan.Zero)
                    {
                        totalExpectedTime = TimeSpan.FromHours(8);
                    }

                    // Create the repair task
                    var task = new Tasks
                    {
                        Id = Guid.NewGuid(),
                        TaskName = $"Sửa máy - {requestInfo.DeviceName}",
                        TaskType = "Repair",
                        TaskDescription = $"Repair task for errors: {string.Join(", ", errorGuidelines.Select(eg => eg.Error?.Name ?? "Unknown"))}",
                        StartTime = request.StartDate,
                        ExpectedTime = request.StartDate.Add(totalExpectedTime),
                        Status = Status.Pending,
                        Priority = Priority.Medium,
                        AssigneeId = request.AssigneeId,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false
                    };

                    await _context.Tasks.AddAsync(task);

                    // Update ErrorDetails and create RequestTakeSparePartUsage for each error guideline
                    foreach (var errorGuideline in errorGuidelines)
                    {
                        // Find existing ErrorDetail for this error and report
                        var errorDetail = await _context.ErrorDetails
                            .FirstOrDefaultAsync(ed => ed.ReportId == requestInfo.ReportId && ed.ErrorId == errorGuideline.ErrorId);

                        if (errorDetail != null)
                        {
                            // Update ErrorDetail with task and guideline
                            errorDetail.TaskId = task.Id;
                            errorDetail.ErrorGuideLineId = errorGuideline.Id;

                            // Create RequestTakeSparePartUsage if guideline has spareparts
                            var errorGuidelineSpareparts = await _context.ErrorSpareparts
                                .Include(egsp => egsp.Sparepart)
                                .Where(egsp => egsp.ErrorGuidelineId == errorGuideline.Id)
                                .ToListAsync();

                            if (errorGuidelineSpareparts.Any())
                            {
                                // Generate request code
                                var requestCount = await _context.RequestTakeSparePartUsages.CountAsync();
                                var requestCode = $"REQ-{DateTime.UtcNow:yyyyMM}-{(requestCount + 1):D3}";

                                // Create RequestTakeSparePartUsage
                                var requestTakeSparePartUsage = new RequestTakeSparePartUsage
                                {
                                    Id = Guid.NewGuid(),
                                    RequestCode = requestCode,
                                    RequestDate = DateTime.UtcNow,
                                    RequestedById = userId,
                                    AssigneeId = request.AssigneeId,
                                    Status = SparePartRequestStatus.Unconfirmed,
                                    Notes = $"Auto-generated for repair task: {task.TaskName}",
                                    CreatedDate = DateTime.UtcNow,
                                    IsDeleted = false
                                };

                                await _context.RequestTakeSparePartUsages.AddAsync(requestTakeSparePartUsage);

                                // Create SparePartUsages
                                var sparePartUsages = errorGuidelineSpareparts.Select(egsp => new SparePartUsage
                                {
                                    Id = Guid.NewGuid(),
                                    SparePartId = egsp.SparepartId,
                                    QuantityUsed = egsp.QuantityNeeded ?? 1, // Default to 1 if not specified
                                    IsTakenFromStock = false,
                                    RequestTakeSparePartUsageId = requestTakeSparePartUsage.Id,
                                    CreatedDate = DateTime.UtcNow,
                                    IsDeleted = false
                                }).ToList();

                                await _context.SparePartUsages.AddRangeAsync(sparePartUsages);

                                // Link ErrorDetail to RequestTakeSparePartUsage
                                errorDetail.RequestTakeSparePartUsageId = requestTakeSparePartUsage.Id;
                            }

                            _context.ErrorDetails.Update(errorDetail);

                            // Create ErrorFixProgress for each step in the guideline
                            if (errorGuideline.ErrorFixSteps != null && errorGuideline.ErrorFixSteps.Any())
                            {
                                var errorFixProgresses = errorGuideline.ErrorFixSteps.Select(step => new ErrorFixProgress
                                {
                                    Id = Guid.NewGuid(),
                                    ErrorDetailId = errorDetail.Id,
                                    ErrorFixStepId = step.Id,
                                    IsCompleted = false,
                                    CreatedDate = DateTime.UtcNow,
                                    IsDeleted = false
                                }).ToList();

                                await _context.ErrorFixProgresses.AddRangeAsync(errorFixProgresses);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return task.Id;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<bool> UpdateTaskStatusToCompleted(Guid taskId, Guid userId)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId && !t.IsDeleted);

            if (task == null)
                return false;

            // Update task status
            task.Status = Status.Completed;
            task.EndTime = DateTime.UtcNow;
            task.ModifiedDate = DateTime.UtcNow;
            task.ModifiedBy = userId;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
