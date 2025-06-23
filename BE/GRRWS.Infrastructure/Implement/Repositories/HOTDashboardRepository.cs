using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Dashboard;
using GRRWS.Infrastructure.DTOs.HOTDashboard;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class HOTDashboardRepository : GenericRepository<DashboardStatsDTO>, IHOTDashboardRepository
    {
        public HOTDashboardRepository(GRRWSContext context) : base(context)
        {
        }
        public async Task<RequestStatsDTO> GetRequestStatsAsync()
        {
            var stats = await _context.Requests
                .Where(r => !r.IsDeleted)
                .GroupBy(r => r.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return new RequestStatsDTO
            {
                Pending = stats.FirstOrDefault(s => s.Status == Status.Pending)?.Count ?? 0,
                InProgress = stats.FirstOrDefault(s => s.Status == Status.InProgress)?.Count ?? 0,
                Completed = stats.FirstOrDefault(s => s.Status == Status.Completed)?.Count ?? 0
            };
        }

        public async Task<TaskStatsDTO> GetTaskStatsAsync()
        {
            var stats = await _context.Tasks
                .Where(t => !t.IsDeleted)
                .GroupBy(t => t.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return new TaskStatsDTO
            {
                Pending = stats.FirstOrDefault(s => s.Status == Status.Pending)?.Count ?? 0,
                InProgress = stats.FirstOrDefault(s => s.Status == Status.InProgress)?.Count ?? 0,
                Completed = stats.FirstOrDefault(s => s.Status == Status.Completed)?.Count ?? 0
            };
        }

        public async Task<MechanicStatsDTO> GetMechanicStatsAsync()
        {
            // Get mechanics who have tasks in progress (InTask)
            var mechanicsInTask = await _context.Tasks
                .Where(t => !t.IsDeleted && t.Status != Status.Completed)
                .Select(t => t.AssigneeId)
                .Distinct()
                .CountAsync();

            // Get total mechanics (assuming mechanics are users with specific role)
            var totalMechanics = await _context.Users
                .Where(u => !u.IsDeleted && u.Role == 3)
                .CountAsync();

            return new MechanicStatsDTO
            {
                InTask = mechanicsInTask,
                Available = totalMechanics - mechanicsInTask
            };
        }

        public async Task<List<ChartDataDTO>> GetRequestChartDataAsync()
        {
            var stats = await GetRequestStatsAsync();
            var total = stats.Total;

            return new List<ChartDataDTO>
            {
                new ChartDataDTO
                {
                    Label = "Pending",
                    Value = stats.Pending,
                    Color = "#FFA726",
                    Percentage = total > 0 ? Math.Round((double)stats.Pending / total * 100, 2) : 0
                },
                new ChartDataDTO
                {
                    Label = "InProgress",
                    Value = stats.InProgress,
                    Color = "#42A5F5",
                    Percentage = total > 0 ? Math.Round((double)stats.InProgress / total * 100, 2) : 0
                },
                new ChartDataDTO
                {
                    Label = "Completed",
                    Value = stats.Completed,
                    Color = "#66BB6A",
                    Percentage = total > 0 ? Math.Round((double)stats.Completed / total * 100, 2) : 0
                }
            };
        }

        public async Task<List<ChartDataDTO>> GetTaskChartDataAsync()
        {
            var stats = await GetTaskStatsAsync();
            var total = stats.Total;

            return new List<ChartDataDTO>
            {
                new ChartDataDTO
                {
                    Label = "Pending",
                    Value = stats.Pending,
                    Color = "#FF7043",
                    Percentage = total > 0 ? Math.Round((double)stats.Pending / total * 100, 2) : 0
                },
                new ChartDataDTO
                {
                    Label = "InProgress",
                    Value = stats.InProgress,
                    Color = "#29B6F6",
                    Percentage = total > 0 ? Math.Round((double)stats.InProgress / total * 100, 2) : 0
                },
                new ChartDataDTO
                {
                    Label = "Completed",
                    Value = stats.Completed,
                    Color = "#26A69A",
                    Percentage = total > 0 ? Math.Round((double)stats.Completed / total * 100, 2) : 0
                }
            };
        }

        public async Task<List<ChartDataDTO>> GetMechanicChartDataAsync()
        {
            var stats = await GetMechanicStatsAsync();
            var total = stats.Total;

            return new List<ChartDataDTO>
            {
                new ChartDataDTO
                {
                    Label = "Available",
                    Value = stats.Available,
                    Color = "#4CAF50",
                    Percentage = total > 0 ? Math.Round((double)stats.Available / total * 100, 2) : 0
                },
                new ChartDataDTO
                {
                    Label = "InTask",
                    Value = stats.InTask,
                    Color = "#F44336",
                    Percentage = total > 0 ? Math.Round((double)stats.InTask / total * 100, 2) : 0
                }
            };
        }
        public async Task<List<RequestDTO>> GetRequestsContainReportAsync()
        {
            return await _context.Requests
                .Where(r => !r.IsDeleted && r.Report != null && r.ReportId != null)
                .Include(r => r.Report)
                .Include(r => r.Device)
                .ThenInclude(d => d.Position)
                .ThenInclude(p => p.Zone)
                .ThenInclude(z => z.Area)
                .Include(r => r.RequestIssues)
                .ThenInclude(i => i.Images)
                .Include(r => r.RequestIssues)
                .ThenInclude(i => i.Issue)
                .Select(r => new RequestDTO
                {
                    Id = r.Id,
                    ReportId = r.Report.Id,
                    DeviceId = r.Device.Id,
                    DeviceName = r.Device.DeviceName,
                    DeviceCode = r.Device.DeviceCode,
                    PositionIndex = r.Device.Position.Index,
                    ZoneName = r.Device.Position.Zone.ZoneName,
                    AreaName = r.Device.Position.Zone.Area.AreaName,
                    RequestDate = r.CreatedDate,
                    RequestTitle = r.RequestTitle,
                    Description = r.Description,
                    Status = r.Status.ToString(),
                    Priority = r.Priority.ToString(),
                    CreatedDate = r.CreatedDate,
                    CreatedBy = r.RequestedById,
                    ModifiedBy = r.ModifiedBy,
                    ModifiedDate = r.ModifiedDate,
                    Issues = r.RequestIssues.Select(ri => new IssueDTO
                    {
                        Id = ri.Id,
                        DisplayName = ri.Issue.DisplayName,
                        ImageUrls = ri.Images.Select(i => i.ImageUrl).ToList(),
                    }).ToList()
                })
                .OrderBy(r => r.CreatedDate)
                .ToListAsync();
        }

        public async Task<ReportStatisticsDTO> GetReportStatisticsAsync()
        {
            var totalReports = await _context.Reports.Where(r => !r.IsDeleted).CountAsync();
            var totalWarrantyReports = await _context.Reports.Where(r => !r.IsDeleted && r.TechnicalSymptomReports != null && r.ErrorDetails == null).CountAsync();
            var totalRepairReports = await _context.Reports.Where(r => !r.IsDeleted && r.TechnicalSymptomReports == null && r.ErrorDetails != null).CountAsync();
            var totalCompletedReports = await _context.Reports.Where(r => !r.IsDeleted && r.Request.Status == Status.Completed).CountAsync();
            var totalPendingReports = await _context.Reports.Where(r => !r.IsDeleted && r.Request.Status == Status.Pending).CountAsync();
            return new ReportStatisticsDTO
            {
                TotalReports = totalReports,
                TotalRepairReports = totalRepairReports,
                TotalWarrantyReports = totalWarrantyReports,
                TotalCompletedReports = totalCompletedReports,
                TotalPendingReports = totalPendingReports,
                WarrantyReportsPercentage = totalReports > 0 ? Math.Round((double)totalWarrantyReports / totalReports * 100, 2) : 0,
                RepairReportsPercentage = totalReports > 0 ? Math.Round((double)totalRepairReports / totalReports * 100, 2) : 0
            };
        }
        public async Task<TaskStatisticsDTO> GetTaskStatisticsAsync()
        {
            var totalTasks = await _context.Tasks.Where(t => !t.IsDeleted && (t.TaskType == TaskType.WarrantySubmission || t.TaskType == TaskType.Repair || t.TaskType == TaskType.Replacement)).CountAsync();

            var totalWarrantyTasks = await _context.Tasks.Where(t => !t.IsDeleted && t.TaskType == TaskType.WarrantySubmission).CountAsync();

            var totalRepairTasks = await _context.Tasks.Where(t => !t.IsDeleted && t.TaskType == TaskType.Repair).CountAsync();

            var totalReplaceTasks = await _context.Tasks.Where(t => !t.IsDeleted && t.TaskType == TaskType.Replacement).CountAsync();

            var totalPendingTasks = await _context.Tasks.Where(t => !t.IsDeleted && t.Status == Status.Pending).CountAsync();

            var totalCompletedTasks = await _context.Tasks.Where(t => !t.IsDeleted && t.Status == Status.Completed).CountAsync();
            return new TaskStatisticsDTO
            {
                TotalTasks = totalTasks,
                TotalPendingTasks = totalPendingTasks,
                TotalCompletedTasks = totalCompletedTasks,
                TotalWarrantyTasks = totalWarrantyTasks,
                TotalRepairTasks = totalRepairTasks,
                TotalReplaceTasks = totalReplaceTasks,
                WarrantyTasksPercentage = totalTasks > 0 ? Math.Round((double)totalWarrantyTasks / totalTasks * 100, 2) : 0,
                RepairTasksPercentage = totalTasks > 0 ? Math.Round((double)totalRepairTasks / totalTasks * 100, 2) : 0,
                ReplaceTasksPercentage = totalTasks > 0 ? Math.Round((double)totalReplaceTasks / totalTasks * 100, 2) : 0
            };
        }
        public async Task<DeviceStatisticsDTO> GetDeviceStatisticsAsync()
        {
            var totalDevices = await _context.Devices.Where(d => d.Status != DeviceStatus.Inactive).CountAsync();
            var totalActiveDevices = await _context.Devices.Where(d => d.Status == DeviceStatus.Active).CountAsync();
            var totalInUseDevices = await _context.Devices.Where(d => d.Status == DeviceStatus.InUse).CountAsync();
            var totalInRepairDevices = await _context.Devices.Where(d => d.Status == DeviceStatus.InRepair).CountAsync();
            var totalInWarrantyDevices = await _context.Devices.Where(d => d.Status == DeviceStatus.InWarranty).CountAsync();
            var totalDecommissionedDevices = await _context.Devices.Where(d => d.Status == DeviceStatus.Decommissioned).CountAsync();
            return new DeviceStatisticsDTO
            {
                TotalDevices = totalDevices,
                TotalActiveDevices = totalActiveDevices,
                TotalInUseDevices = totalInUseDevices,
                TotalInRepairDevices = totalInRepairDevices,
                TotalInWarrantyDevices = totalInWarrantyDevices,
                TotalDecommissionedDevices = totalDecommissionedDevices
            };
        }
        public async Task<TotalTaskRequestReportDTO> GetTotalTaskRequestReportAsync()
        {
            var totalTasks = await _context.Tasks.CountAsync(t => !t.IsDeleted);
            var totalRequests = await _context.Requests.CountAsync(r => !r.IsDeleted);
            var totalReports = await _context.Reports.CountAsync(r => !r.IsDeleted);
            return new TotalTaskRequestReportDTO
            {
                TotalTasks = totalTasks,
                TotalRequests = totalRequests,
                TotalReports = totalReports
            };
        }
        public async Task<TotalUserByRoleDTO> GetTotalUserByRoleAsync()
        {
            var totalUsers = await _context.Users.CountAsync(u => !u.IsDeleted);
            var totalAdmins = await _context.Users.CountAsync(u => !u.IsDeleted && u.Role == 5);
            var totalHeadDepartments = await _context.Users.CountAsync(u => !u.IsDeleted && u.Role == 1);
            var totalHeadTechnicals = await _context.Users.CountAsync(u => !u.IsDeleted && u.Role == 2);
            var totalMechanics = await _context.Users.CountAsync(u => !u.IsDeleted && u.Role == 3);
            var totalStockKeepers = await _context.Users.CountAsync(u => !u.IsDeleted && u.Role == 4);
            return new TotalUserByRoleDTO
            {
                TotalUsers = totalUsers,
                TotalAdmins = totalAdmins,
                TotalHeadsOfDepartment = totalHeadDepartments,
                TotalHeadsOfTechnical = totalHeadTechnicals,
                TotalMechanics = totalMechanics,
                TotalStockKeepers = totalStockKeepers
            };
        }
        public async Task<TaskByWeekAndMonthDTO> GetTaskCompletionCountByWeekAndMonthAsync()
        {
            var currentDate = DateTime.UtcNow;
            var startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + 1).Date;
            if (currentDate.DayOfWeek == DayOfWeek.Sunday) startOfWeek = currentDate.AddDays(-6).Date;
            var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1).Date;

            var result = new TaskByWeekAndMonthDTO
            {
                TotalTasksThisWeek = await _context.Tasks
                    .CountAsync(t => t.Status == Status.Completed &&
                                  t.EndTime.HasValue &&
                                  t.EndTime.Value.Date >= startOfWeek &&
                                  t.EndTime.Value.Date <= currentDate.Date),
                TotalTasksThisMonth = await _context.Tasks
                    .CountAsync(t => t.Status == Status.Completed &&
                                  t.EndTime.HasValue &&
                                  t.EndTime.Value.Date >= startOfMonth &&
                                  t.EndTime.Value.Date <= currentDate.Date)
            };

            return result;
        }
    }
}

