using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.HOTDashboard;
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

    }

}

