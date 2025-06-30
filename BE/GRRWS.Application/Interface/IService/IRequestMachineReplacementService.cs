using GRRWS.Application.Common.Result;

namespace GRRWS.Application.Interface.IService
{
    public interface IRequestMachineReplacementService
    {
        public Task<Result> GetAllAsync(
    int pageNumber,
    int pageSize,
    string? status = null,
    string? sortBy = null,
    bool isAscending = true);



    }
}
