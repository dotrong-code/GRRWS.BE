using GRRWS.Application.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface IMechanicShiftService
    {
        Task<Result> CreateMechanicShiftAsync(Guid mechanicId, Guid taskId);
        Task<Result> GetAllMechanicShiftAsync();
        Task<Result> UpdateMechanicShiftStatusToAvailableAsync(Guid mechanicShiftId);
    }
}
