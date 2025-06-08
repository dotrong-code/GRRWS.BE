using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.ErrorFixProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface IErrorFixProgressService
    {
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> GetAllByErrorDetailIdAsync(Guid errorDetailId);
        Task<Result> UpdateIsCompletedAsync(UpdateIsCompletedRequest request);
        Task<Result> DeleteAsync(Guid id);
    }
}
