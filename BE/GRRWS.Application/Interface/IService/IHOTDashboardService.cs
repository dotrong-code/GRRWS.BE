using GRRWS.Application.Common.Result;

namespace GRRWS.Application.Interface.IService
{
    public interface IHOTDashboardService
    {
        Task<Result> GetTechnicalHeadDashboardStatsAsync();
    }
}
