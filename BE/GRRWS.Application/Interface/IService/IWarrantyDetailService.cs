using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.WarrantyDetail;

namespace GRRWS.Application.Interface.IService
{
    public interface IWarrantyDetailService
    {
        Task<Result> CreateAsync(CreateWarrantyDetailDTO dto);
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> GetAllAsync();
        Task<Result> UpdateAsync(Guid id, UpdateWarrantyDetailDTO dto);
        Task<Result> DeleteAsync(Guid id);
    }
}
