using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface IRequestService
    {
        Task<Result> GetAllAsync();
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> CreateAsync(CreateRequestDTO dto, Guid userId);
        Task<Result> UpdateAsync(UpdateRequestDTO dto, Guid id);
        Task<Result> DeleteAsync(Guid id);
        Task<Result> GetRequestByDeviceIdAsync(Guid id);
        Task<Result> GetRequestByUserIdAsync(Guid userId);
        Task<Result> GetIssuesByRequestIdAsync(Guid requestId);
    }
}
