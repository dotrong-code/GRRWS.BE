using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.RequestMachineReplacement;

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
        public Task<Result> ConfirmTakenDevice(Guid requestMachineId, Guid userId);
        public Task<Result> CreateRequestMachineReplacementAsync(Guid requestId, Guid requestUserId);
        public Task<Result> ConfirmHadDevice(Guid requestMachineId, Guid userId);
        public Task<Result> UpdateRequestMachineReplacement(UpdateRMR updateRMR);


    }
}
