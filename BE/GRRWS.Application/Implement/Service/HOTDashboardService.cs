using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.Common;

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
    }
}
