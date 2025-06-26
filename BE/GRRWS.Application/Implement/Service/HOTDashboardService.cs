using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Dashboard;
using GRRWS.Infrastructure.DTOs.RequestDTO;

namespace GRRWS.Application.Implement.Service
{
    public class HOTDashboardService : IHOTDashboardService
    {
        private readonly UnitOfWork _unitOfWork;
        public HOTDashboardService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> GetTechnicalHeadDashboardStatsAsync()
        {
            try
            {
                var requestStats = await _unitOfWork.HOTDashboardRepository.GetRequestStatsAsync();
                var taskStats = await _unitOfWork.HOTDashboardRepository.GetTaskStatsAsync();
                var mechanicStats = await _unitOfWork.HOTDashboardRepository.GetMechanicStatsAsync();
                var requestChart = await _unitOfWork.HOTDashboardRepository.GetRequestChartDataAsync();
                var taskChart = await _unitOfWork.HOTDashboardRepository.GetTaskChartDataAsync();
                var mechanicChart = await _unitOfWork.HOTDashboardRepository.GetMechanicChartDataAsync();

                var dashboardStats = new DashboardStatsDTO
                {
                    RequestStats = requestStats,
                    TaskStats = taskStats,
                    MechanicStats = mechanicStats,
                    RequestChart = requestChart,
                    TaskChart = taskChart,
                    MechanicChart = mechanicChart
                };

                return Result.SuccessWithObject(dashboardStats);
            }
            catch (Exception ex)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Failure", ex.Message));
            }
        }
        public async Task<Result> GetRequestsContainReportAsync()
        {
            var requests = await _unitOfWork.HOTDashboardRepository.GetRequestsContainReportAsync();
            return Result.SuccessWithObject(requests);
        }
        public async Task<Result> GetReportStatisticsAsync()
        {
            var reportStats = await _unitOfWork.HOTDashboardRepository.GetReportStatisticsAsync();
            return Result.SuccessWithObject(reportStats);
        }
        public async Task<Result> GetTaskStatisticsAsync()
        {
            var taskStats = await _unitOfWork.HOTDashboardRepository.GetTaskStatisticsAsync();
            return Result.SuccessWithObject(taskStats);
        }
        public async Task<Result> GetDeviceStatisticsAsync()
        {
            var deviceStats = await _unitOfWork.HOTDashboardRepository.GetDeviceStatisticsAsync();
            return Result.SuccessWithObject(deviceStats);
        }
        public async Task<Result> GetTotalUserByRoleAsync()
        {
            var totalUserByRole = await _unitOfWork.HOTDashboardRepository.GetTotalUserByRoleAsync();
            return Result.SuccessWithObject(totalUserByRole);
        }
        public async Task<Result> GetTaskCompletionCountByWeekAndMonthAsync()
        {
            var taskCompletionCount = await _unitOfWork.HOTDashboardRepository.GetTaskCompletionCountByWeekAndMonthAsync();
            return Result.SuccessWithObject(taskCompletionCount);
        }
        public async Task<Result> GetTotalTaskRequestReportAsync()
        {
            var totalTaskRequestReport = await _unitOfWork.HOTDashboardRepository.GetTotalTaskRequestReportAsync();
            return Result.SuccessWithObject(totalTaskRequestReport);
        }
        public async Task<Result> GetTop5MostErrorDevicesAsync()
        {
            var top5MostErrorDevices = await _unitOfWork.HOTDashboardRepository.GetTop5MostErrorDevicesAsync();
            return Result.SuccessWithObject(top5MostErrorDevices);
        }
        public async Task<Result> GetTop3MechanicsAsync()
        {
            var top3Mechanics = await _unitOfWork.HOTDashboardRepository.GetTop3MechanicsAsync();
            return Result.SuccessWithObject(top3Mechanics);
        }
        public async Task<Result> GetMonthlyRequestCountForLast6MonthsAsync()
        {
            var monthlyRequestCount = await _unitOfWork.HOTDashboardRepository.GetMonthlyRequestCountForLast6MonthsAsync();
            return Result.SuccessWithObject(monthlyRequestCount);
        }
    }
}
