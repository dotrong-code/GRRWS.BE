using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.DTOs.Task;
using GRRWS.Infrastructure.DTOs.Task.ActionTask;
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
                    TaskType = ed.Task.TaskType.ToString(),
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
                    TaskType = t.TaskType.ToString(),
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
                    TaskType = t.TaskType.ToString(),
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

            //if (!string.IsNullOrWhiteSpace(taskType))
            //    query = query.Where(t => t.TaskType == taskType);

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
                    TaskType = t.TaskType.ToString(),
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
        public async Task<List<Tasks>> GetTasksByGroupIdAsync(Guid taskGroupId)
        {
            return await _context.Tasks
                .Where(t => t.TaskGroupId == taskGroupId && !t.IsDeleted)
                .OrderBy(t => t.OrderIndex)
                .ToListAsync();
        }
        public async Task<RequestInfoDto> GetRequestInfoAsync(Guid requestId)
        {
            return await _context.Requests
                .Include(r => r.Device)
                .Include(r => r.Report)
                .Where(r => r.Id == requestId)
                .Select(r => new RequestInfoDto
                {
                    ReportId = r.ReportId ?? Guid.Empty,
                    DeviceId = r.Device.Id,
                    DeviceName = r.Device.DeviceName,
                    DeviceCode = r.Device.DeviceCode,
                    Location = r.Report.Location ?? "Location not available"
                })
                .FirstOrDefaultAsync();
        }

        public async Task<string> GetDeviceInfoAsync(Guid deviceId)
        {
            var device = await _context.Devices
                .Where(d => d.Id == deviceId)
                .Select(d => new { d.DeviceName, d.DeviceCode })
                .FirstOrDefaultAsync();

            return device != null ? $"{device.DeviceName} ({device.DeviceCode})" : "Unknown Device";
        }
        public async Task<List<GetTaskForMechanic>> GetTasksByMechanicIdAsync2(Guid mechanicId, GetAllSingleTasksRequest request)
        {
            var query = _context.Tasks
                .Where(t => t.AssigneeId == mechanicId && !t.IsDeleted);

            // Apply filters
            if (!string.IsNullOrEmpty(request.TaskType) && Enum.TryParse<TaskType>(request.TaskType, true, out var parsedTaskType))
            {
                query = query.Where(t => t.TaskType == parsedTaskType);
            }

            if (!string.IsNullOrEmpty(request.Status) && Enum.TryParse<Status>(request.Status, true, out var parsedStatus))
            {
                query = query.Where(t => t.Status == parsedStatus);
            }

            if (!string.IsNullOrEmpty(request.Priority) && Enum.TryParse<Domain.Enum.Priority>(request.Priority, true, out var parsedPriority))
            {
                query = query.Where(t => t.Priority == parsedPriority);
            }
            if (!string.IsNullOrEmpty(request.Order) && Enum.TryParse<SearchOrder>(request.Order, true, out var parsedOrder))
            {
                query = parsedOrder switch
                {
                    //SearchOrder.Ascending => query.OrderBy(t => t.),      // A-Z
                    //SearchOrder.Descending => query.OrderByDescending(t => t.SomeTextField), // Z-A
                    SearchOrder.Latest => query.OrderByDescending(t => t.CreatedDate), // newest first
                    SearchOrder.Oldest => query.OrderBy(t => t.CreatedDate),           // oldest first
                    _ => query.OrderByDescending(t => t.CreatedDate)                   // default
                };
            }

            var totalCount = await query.CountAsync();
            var tasks = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(t => new GetTaskForMechanic
                {
                    TaskId = t.Id,
                    TaskName = t.TaskName,
                    TaskDescription = t.TaskDescription,
                    TaskType = t.TaskType.ToString(),
                    Priority = t.Priority.ToString(), // Convert enum to string
                    Status = t.Status.ToString(),
                    CreateDate = t.CreatedDate// Convert enum to string

                })
                .ToListAsync();

            return tasks;
        }
        public async Task<GetDetailWarrantyTaskForMechanic> GetGetDetailWarrantyTaskForMechanicByIdAsync(Guid taskId, TaskType type)
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
                DeviceId = task.WarrantyClaim?.DeviceWarranty?.DeviceId ?? Guid.Empty,
                TaskName = task.TaskName,
                TaskType = task.TaskType.ToString(),
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

                .Where(t => t.Id == taskId && !t.IsDeleted && t.TaskType == TaskType.Repair)
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
                DeviceId = deviceId,
                TaskType = task.TaskType.ToString(),
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
        public async Task<GetDetailUninstallTaskForMechanic> GetDetailUninstallTaskForMechanicByIdAsync(Guid taskId)
        {
            var task = await _context.Tasks
                .Include(t => t.Assignee)
                .Include(t => t.TaskGroup)
                .Where(t => t.Id == taskId && !t.IsDeleted && t.TaskType == TaskType.Uninstallation)
                .FirstOrDefaultAsync();

            if (task == null)
                return null;

            // Get device and location information from the related request
            var deviceInfo = await _context.Requests
                .Include(r => r.Device)
                    .ThenInclude(d => d.Position)
                        .ThenInclude(p => p.Zone)
                            .ThenInclude(z => z.Area)
                .Include(r => r.Report)
                .Where(r => r.ReportId != null && _context.Tasks.Any(t => t.Id == taskId))
                .Select(r => new
                {
                    DeviceId = r.Device.Id,
                    DeviceName = r.Device.DeviceName,
                    DeviceCode = r.Device.DeviceCode,
                    Location = r.Device.Position != null && r.Device.Position.Zone != null && r.Device.Position.Zone.Area != null
                        ? $"{r.Device.Position.Zone.Area.AreaName} - {r.Device.Position.Zone.ZoneName} - {r.Device.Position.Index}"
                        : r.Report.Location ?? "Location not available"
                })
                .FirstOrDefaultAsync();

            return new GetDetailUninstallTaskForMechanic
            {
                TaskId = task.Id,
                DeviceId = deviceInfo?.DeviceId ?? Guid.Empty,
                TaskType = task.TaskType.ToString(),
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                Priority = task.Priority.ToString(),
                Status = task.Status.ToString(),
                StartTime = task.StartTime,
                ExpectedTime = task.ExpectedTime,
                EndTime = task.EndTime,
                AssigneeName = task.Assignee?.FullName,
                DeviceName = deviceInfo?.DeviceName ?? "Unknown Device",
                DeviceCode = deviceInfo?.DeviceCode ?? "N/A",
                Location = deviceInfo?.Location ?? "Location not available",
                TaskGroupName = task.TaskGroup?.GroupName
            };
        }
        public async Task<GetDetailInstallTaskForMechanic> GetDetailInstallTaskForMechanicByIdAsync(Guid taskId)
        {
            var task = await _context.Tasks
                .Include(t => t.Assignee)
                .Include(t => t.TaskGroup)
                .Where(t => t.Id == taskId && !t.IsDeleted && t.TaskType == TaskType.Installation)
                .FirstOrDefaultAsync();

            if (task == null)
                return null;

            // Get device and location information from the related request
            var deviceInfo = await _context.Requests
                .Include(r => r.Device)
                    .ThenInclude(d => d.Position)
                        .ThenInclude(p => p.Zone)
                            .ThenInclude(z => z.Area)
                .Include(r => r.Report)
                .Where(r => r.ReportId != null && _context.Tasks.Any(t => t.Id == taskId))
                .Select(r => new
                {
                    DeviceId = r.Device.Id,
                    DeviceName = r.Device.DeviceName,
                    DeviceCode = r.Device.DeviceCode,
                    Location = r.Device.Position != null && r.Device.Position.Zone != null && r.Device.Position.Zone.Area != null
                        ? $"{r.Device.Position.Zone.Area.AreaName} - {r.Device.Position.Zone.ZoneName} - {r.Device.Position.Index}"
                        : r.Report.Location ?? "Location not available"
                })
                .FirstOrDefaultAsync();

            return new GetDetailInstallTaskForMechanic
            {
                TaskId = task.Id,
                DeviceId = deviceInfo?.DeviceId ?? Guid.Empty,
                TaskType = task.TaskType.ToString(),
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                Priority = task.Priority.ToString(),
                Status = task.Status.ToString(),
                StartTime = task.StartTime,
                ExpectedTime = task.ExpectedTime,
                EndTime = task.EndTime,
                AssigneeName = task.Assignee?.FullName,
                DeviceName = deviceInfo?.DeviceName ?? "Unknown Device",
                DeviceCode = deviceInfo?.DeviceCode ?? "N/A",
                Location = deviceInfo?.Location ?? "Location not available",
                TaskGroupName = task.TaskGroup?.GroupName
            };
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
                        TaskType = TaskType.WarrantySubmission,
                        TaskDescription = $"Mang thiết bị đi bảo hành với cái triệu chứng:{issueDescription}",
                        StartTime = request.StartDate,
                        ExpectedTime = request.StartDate.AddHours(5),
                        Status = Status.Pending,
                        Priority = Domain.Enum.Priority.High,
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
        public async Task<Guid> CreateWarrantyTaskWithGroup(CreateWarrantyTaskRequest request, Guid userId, Guid taskGroupId, int orderIndex)
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
                        TaskType = TaskType.WarrantySubmission,
                        TaskDescription = $"Mang thiết bị đi bảo hành với cái triệu chứng:{issueDescription}",
                        StartTime = request.StartDate,
                        ExpectedTime = request.StartDate.AddHours(5),
                        Status = Status.Pending,
                        Priority = Domain.Enum.Priority.High,
                        AssigneeId = request.AssigneeId,
                        WarrantyClaimId = warrantyClaim.Id,
                        TaskGroupId = taskGroupId,
                        OrderIndex = orderIndex,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = userId,
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
                TaskType = TaskType.WarrantyReturn,
                TaskDescription = $"Collect device from warranty provider for claim: {warrantyClaim.ClaimNumber}. Expected return date: {warrantyClaim.ExpectedReturnDate:dd/MM/yyyy}",
                StartTime = warrantyClaim.ExpectedReturnDate,
                ExpectedTime = warrantyClaim.ExpectedReturnDate?.AddHours(2),
                Status = Status.Pending,
                Priority = Domain.Enum.Priority.Medium,
                OrderIndex = 4,
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
            //_context.Tasks.Update(task);
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
                        TaskType = TaskType.Repair,
                        TaskDescription = $"Repair task for errors: {string.Join(", ", errorGuidelines.Select(eg => eg.Error?.Name ?? "Unknown"))}",
                        StartTime = request.StartDate,
                        ExpectedTime = request.StartDate.Add(totalExpectedTime),
                        Status = Status.Pending,
                        Priority = Domain.Enum.Priority.Medium,
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
        public async Task<Guid> CreateRepairTaskWithGroup(CreateRepairTaskRequest request, Guid userId, Guid? taskGroupId, int orderIndex)
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
                        TaskType = TaskType.Repair,
                        TaskDescription = $"Sửa lỗi: {string.Join(", ", errorGuidelines.Select(eg => eg.Error?.Name ?? "Unknown"))}",
                        StartTime = request.StartDate,
                        ExpectedTime = request.StartDate.Add(totalExpectedTime),
                        Status = Status.Pending,
                        Priority = Domain.Enum.Priority.Medium,
                        AssigneeId = request.AssigneeId,
                        TaskGroupId = taskGroupId,
                        OrderIndex = orderIndex,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = userId,
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
        public async Task<Guid> CreateUninstallTask(CreateUninstallTaskRequest request, Guid userId)
        {
            var executionStrategy = _context.Database.CreateExecutionStrategy();

            return await executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Get request and device information
                    var requestInfo = await _context.Requests
                        .Include(r => r.Device)
                        .Include(r => r.Report)
                        .Where(r => r.Id == request.RequestId)
                        .Select(r => new
                        {
                            r.ReportId,
                            DeviceId = r.Device.Id,
                            DeviceName = r.Device.DeviceName,
                            DeviceCode = r.Device.DeviceCode,
                            r.Report.Location
                        })
                        .FirstOrDefaultAsync();

                    if (requestInfo == null)
                        throw new Exception("No request found.");

                    // Create the uninstall task
                    var task = new Tasks
                    {
                        Id = Guid.NewGuid(),
                        TaskName = $"Tháo máy - {requestInfo.DeviceName}",
                        TaskType = TaskType.Uninstallation,
                        TaskDescription = $"Tháo thiết bị {requestInfo.DeviceName} ({requestInfo.DeviceCode}) tại vị trí: {requestInfo.Location}",
                        StartTime = request.StartDate ?? DateTime.UtcNow,
                        ExpectedTime = (request.StartDate ?? DateTime.UtcNow).AddHours(2), // Default 2 hours for uninstallation
                        Status = Status.Pending,
                        Priority = Domain.Enum.Priority.Medium,
                        AssigneeId = request.AssigneeId,
                        TaskGroupId = request.TaskGroupId,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = userId,
                        IsDeleted = false
                    };

                    await _context.Tasks.AddAsync(task);
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
        public async Task<Guid> CreateUninstallTaskWithGroup(CreateUninstallTaskRequest request, Guid userId, Guid taskGroupId, int orderIndex)
        {
            var executionStrategy = _context.Database.CreateExecutionStrategy();

            return await executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Get request and device information
                    var requestInfo = await _context.Requests
                        .Include(r => r.Device)
                        .Include(r => r.Report)
                        .Where(r => r.Id == request.RequestId)
                        .Select(r => new
                        {
                            r.ReportId,
                            DeviceId = r.Device.Id,
                            DeviceName = r.Device.DeviceName,
                            DeviceCode = r.Device.DeviceCode,
                            r.Report.Location
                        })
                        .FirstOrDefaultAsync();

                    if (requestInfo == null)
                        throw new Exception("No request found.");

                    // Create the uninstall task
                    var task = new Tasks
                    {
                        Id = Guid.NewGuid(),
                        TaskName = $"Tháo máy - {requestInfo.DeviceName}",
                        TaskType = TaskType.Uninstallation,
                        TaskDescription = $"Tháo thiết bị {requestInfo.DeviceName} ({requestInfo.DeviceCode}) tại vị trí: {requestInfo.Location}",
                        StartTime = request.StartDate ?? DateTime.UtcNow,
                        ExpectedTime = (request.StartDate ?? DateTime.UtcNow).AddHours(2), // Default 2 hours for uninstallation
                        Status = Status.Pending,
                        Priority = Domain.Enum.Priority.Medium,
                        AssigneeId = request.AssigneeId,
                        TaskGroupId = taskGroupId,
                        OrderIndex = orderIndex,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = userId,
                        IsDeleted = false
                    };

                    await _context.Tasks.AddAsync(task);
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
        public async Task<Guid> CreateInstallTask(CreateInstallTaskRequest request, Guid userId)
        {
            var executionStrategy = _context.Database.CreateExecutionStrategy();

            return await executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Get request and device information
                    var requestInfo = await _context.Requests
                        .Include(r => r.Device)
                        .Include(r => r.Report)
                        .Where(r => r.Id == request.RequestId)
                        .Select(r => new
                        {
                            r.ReportId,
                            DeviceId = r.Device.Id,
                            DeviceName = r.Device.DeviceName,
                            DeviceCode = r.Device.DeviceCode,
                            r.Report.Location
                        })
                        .FirstOrDefaultAsync();

                    if (requestInfo == null)
                        throw new Exception("No request found.");

                    // Get replacement device information if provided
                    string deviceInfo = "Máy không xác định";
                    if (request.NewDeviceId.HasValue)
                    {
                        var replacementDevice = await _context.Devices
                            .Where(d => d.Id == request.NewDeviceId.Value)
                            .Select(d => new { d.Id, d.DeviceName, d.DeviceCode })
                            .FirstOrDefaultAsync();

                        if (replacementDevice != null)
                        {
                            deviceInfo = $"{replacementDevice.DeviceName} ({replacementDevice.DeviceCode})";
                        }
                    }

                    // Create the install task
                    var task = new Tasks
                    {
                        Id = Guid.NewGuid(),
                        TaskName = $"Lắp đặt máy - {deviceInfo}",
                        TaskType = TaskType.Installation,
                        TaskDescription = $"Lặp đặt máy {deviceInfo} tại vị trí: {requestInfo.Location}",
                        StartTime = request.StartDate ?? DateTime.UtcNow,
                        ExpectedTime = (request.StartDate ?? DateTime.UtcNow).AddHours(3), // Default 3 hours for installation
                        Status = Status.Pending,
                        Priority = Domain.Enum.Priority.Medium,
                        AssigneeId = request.AssigneeId,
                        TaskGroupId = request.TaskGroupId,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = userId,
                        IsDeleted = false
                    };
                    await _context.Tasks.AddAsync(task);
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
        public async Task<Guid> CreateInstallTaskWithGroup(CreateInstallTaskRequest request, Guid userId, Guid taskGroupId, int orderIndex)
        {
            var executionStrategy = _context.Database.CreateExecutionStrategy();

            return await executionStrategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Get request and device information
                    var requestInfo = await _context.Requests
                        .Include(r => r.Device)
                        .Include(r => r.Report)
                        .Where(r => r.Id == request.RequestId)
                        .Select(r => new
                        {
                            r.ReportId,
                            DeviceId = r.Device.Id,
                            DeviceName = r.Device.DeviceName,
                            DeviceCode = r.Device.DeviceCode,
                            r.Report.Location
                        })
                        .FirstOrDefaultAsync();

                    if (requestInfo == null)
                        throw new Exception("No request found.");

                    // Get replacement device information if provided
                    string deviceInfo = "Máy không xác định";
                    if (request.NewDeviceId.HasValue)
                    {
                        var replacementDevice = await _context.Devices
                            .Where(d => d.Id == request.NewDeviceId.Value)
                            .Select(d => new { d.Id, d.DeviceName, d.DeviceCode })
                            .FirstOrDefaultAsync();

                        if (replacementDevice != null)
                        {
                            deviceInfo = $"{replacementDevice.DeviceName} ({replacementDevice.DeviceCode})";
                        }
                    }

                    // Create the install task
                    var task = new Tasks
                    {
                        Id = Guid.NewGuid(),
                        TaskName = $"Lắp đặt máy - {deviceInfo}",
                        TaskType = TaskType.Installation,
                        TaskDescription = $"Lặp đặt máy {deviceInfo} tại vị trí: {requestInfo.Location}",
                        StartTime = request.StartDate ?? DateTime.UtcNow,
                        ExpectedTime = (request.StartDate ?? DateTime.UtcNow).AddHours(3), // Default 3 hours for installation
                        Status = Status.Pending,
                        Priority = Domain.Enum.Priority.Medium,
                        AssigneeId = request.AssigneeId,
                        TaskGroupId = taskGroupId,
                        OrderIndex = orderIndex,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = userId,
                        IsDeleted = false
                    };
                    await _context.Tasks.AddAsync(task);
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
        public async Task<bool> UpdateTaskStatusAsync(Guid taskId, Guid userId)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId && !t.IsDeleted);
            if (task == null)
                return false;
            if (task.Status.Equals(Status.InProgress))
            {
                var getMechanicShift = await _context.MechanicShifts
                    .FirstOrDefaultAsync(ms => ms.MechanicId == userId && ms.TaskId == taskId);
                if (getMechanicShift == null)
                    return false; // Mechanic shift not found or already available
                getMechanicShift.IsAvailable = true;
                _context.MechanicShifts.Update(getMechanicShift);
            }


            // Update status based on current status
            switch (task.Status)
            {
                case Status.Pending:
                    task.Status = Status.InProgress;
                    task.StartTime = DateTime.UtcNow; // Set actual start time
                    break;
                case Status.InProgress:
                    task.Status = Status.Completed;
                    task.EndTime = DateTime.UtcNow;
                    // Set actual end time
                    break;
                default:
                    // Task is already completed or in another state
                    return false;
            }

            // Update modification details
            task.ModifiedDate = DateTime.UtcNow;
            task.ModifiedBy = userId;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> IsTaskCompletedInReqestAsync(Guid requestId, TaskType taskType)
        {
            var report = await _context.Reports
        .Include(r => r.TaskGroups)
            .ThenInclude(tg => tg.Tasks)
        .FirstOrDefaultAsync(r => r.RequestId == requestId);

            if (report == null)
                return false;

            var tasksOfType = report.TaskGroups
                .SelectMany(tg => tg.Tasks)
                .Where(t => t.TaskType == taskType)
                .ToList();

            // Return true if no such task exists OR all are completed
            return !tasksOfType.Any() || tasksOfType.All(t => t.Status == Status.Completed);
        }

        public async Task<(List<GetSingleTaskResponse> Tasks, int TotalCount)> GetAllSingleTasksAsync(string? taskType, string? status, string? priority, string? order, int pageNumber, int pageSize)
        {
            var query = _context.Tasks
                .Include(t => t.Assignee)
                .Where(t => !t.IsDeleted); // Single tasks (not in groups)

            // Apply filters
            if (!string.IsNullOrEmpty(taskType) && Enum.TryParse<TaskType>(taskType, true, out var parsedTaskType))
            {
                query = query.Where(t => t.TaskType == parsedTaskType);
            }

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<Status>(status, true, out var parsedStatus))
            {
                query = query.Where(t => t.Status == parsedStatus);
            }

            if (!string.IsNullOrEmpty(priority) && Enum.TryParse<Domain.Enum.Priority>(priority, true, out var parsedPriority))
            {
                query = query.Where(t => t.Priority == parsedPriority);
            }
            if (!string.IsNullOrEmpty(order) && Enum.TryParse<SearchOrder>(order, true, out var parsedOrder))
            {
                query = parsedOrder switch
                {
                    //SearchOrder.Ascending => query.OrderBy(t => t.),      // A-Z
                    //SearchOrder.Descending => query.OrderByDescending(t => t.SomeTextField), // Z-A
                    SearchOrder.Latest => query.OrderByDescending(t => t.CreatedDate), // newest first
                    SearchOrder.Oldest => query.OrderBy(t => t.CreatedDate),           // oldest first
                    _ => query.OrderByDescending(t => t.CreatedDate)                   // default
                };
            }



            var totalCount = await query.CountAsync();

            var tasks = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new GetSingleTaskResponse
                {
                    TaskId = t.Id,
                    TaskName = t.TaskName,
                    TaskDescription = t.TaskDescription,
                    TaskType = t.TaskType.ToString(),
                    Priority = t.Priority.ToString(),
                    Status = t.Status.ToString(),
                    StartTime = t.StartTime,
                    ExpectedTime = t.ExpectedTime,
                    EndTime = t.EndTime,
                    AssigneeName = t.Assignee.FullName,
                    AssigneeId = t.AssigneeId,
                    CreatedDate = t.CreatedDate,
                    ModifiedDate = t.ModifiedDate,
                    RequestId = _context.Requests
                        .Where(r => r.ReportId != null &&
                                   _context.Reports.Any(rep => rep.Id == r.ReportId &&
                                                              rep.TaskGroups.Any(tg => tg.Tasks.Any(task => task.Id == t.Id))))
                        .Select(r => r.Id)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return (tasks, totalCount);
        }

        public async Task<(List<GetGroupTaskResponse> Groups, int TotalCount)> GetAllGroupTasksAsync(int pageNumber, int pageSize)
        {
            var query = _context.TaskGroups
                .Include(tg => tg.Tasks.Where(t => !t.IsDeleted))
                    .ThenInclude(t => t.Assignee)
                .Include(tg => tg.Report)
                    .ThenInclude(r => r.Request)
                .Where(tg => !tg.IsDeleted)
                .OrderByDescending(tg => tg.CreatedDate);

            var totalCount = await query.CountAsync();

            var groups = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(tg => new GetGroupTaskResponse
                {
                    TaskGroupId = tg.Id,
                    GroupName = tg.GroupName,
                    GroupType = tg.GroupType.ToString(),
                    CreatedDate = tg.CreatedDate,
                    RequestId = tg.Report.Request.Id,
                    Tasks = tg.Tasks
                        .OrderBy(t => t.OrderIndex)
                        .Select(t => new TaskInGroupResponse
                        {
                            TaskId = t.Id,
                            TaskName = t.TaskName,
                            TaskDescription = t.TaskDescription,
                            TaskType = t.TaskType.ToString(),
                            Priority = t.Priority.ToString(),
                            Status = t.Status.ToString(),
                            OrderIndex = t.OrderIndex ?? 0,
                            StartTime = t.StartTime,
                            ExpectedTime = t.ExpectedTime,
                            EndTime = t.EndTime,
                            AssigneeName = t.Assignee.FullName,
                            AssigneeId = t.AssigneeId,
                            CreatedDate = t.CreatedDate
                        })
                        .ToList()
                })
                .ToListAsync();

            return (groups, totalCount);
        }

        public async Task<(List<GetGroupTaskResponse> Groups, int TotalCount)> GetGroupTasksByRequestIdAsync(Guid requestId, int pageNumber, int pageSize)
        {
            var query = _context.TaskGroups
                .Include(tg => tg.Tasks.Where(t => !t.IsDeleted))
                    .ThenInclude(t => t.Assignee)
                .Include(tg => tg.Report)
                    .ThenInclude(r => r.Request)
                .Where(tg => !tg.IsDeleted && tg.Report.RequestId == requestId)
                .OrderByDescending(tg => tg.CreatedDate);

            var totalCount = await query.CountAsync();

            var groups = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(tg => new GetGroupTaskResponse
                {
                    TaskGroupId = tg.Id,
                    GroupName = tg.GroupName,
                    GroupType = tg.GroupType.ToString(),
                    CreatedDate = tg.CreatedDate,
                    RequestId = requestId,
                    Tasks = tg.Tasks
                        .OrderBy(t => t.OrderIndex)
                        .Select(t => new TaskInGroupResponse
                        {
                            TaskId = t.Id,
                            TaskName = t.TaskName,
                            TaskDescription = t.TaskDescription,
                            TaskType = t.TaskType.ToString(),
                            Priority = t.Priority.ToString(),
                            Status = t.Status.ToString(),
                            OrderIndex = t.OrderIndex ?? 0,
                            StartTime = t.StartTime,
                            ExpectedTime = t.ExpectedTime,
                            EndTime = t.EndTime,
                            AssigneeName = t.Assignee.FullName,
                            AssigneeId = t.AssigneeId,
                            CreatedDate = t.CreatedDate
                        })
                        .ToList()
                })
                .ToListAsync();

            return (groups, totalCount);
        }

    }
}
