using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Sparepart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface ISparepartService
    {
        Task<Result> GetAllAsync();
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> CreateAsync(CreateSparepartDTO dto);
        Task<Result> UpdateAsync(Guid id, UpdateSparepartDTO dto);
        Task<Result> DeleteAsync(Guid id);
        Task<Result> GetAvailabilityAsync();
    }
}
