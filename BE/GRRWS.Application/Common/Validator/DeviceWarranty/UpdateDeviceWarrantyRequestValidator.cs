using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Validator.Abstract;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.DeviceWarranty;

namespace GRRWS.Application.Common.Validator.DeviceWarranty
{
    public class UpdateDeviceWarrantyRequestValidator : DeviceWarrantyValidator<UpdateDeviceWarrantyRequest>
    {
        public UpdateDeviceWarrantyRequestValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddIdRules(request => request.Id);
            AddStatusRules(request => request.Status);
            AddWarrantyTypeRules(request => request.WarrantyType);
            AddWarrantyDateRules(request => request.WarrantyStartDate, request => request.WarrantyEndDate);
            AddDeviceIdRules(request => request.DeviceId);
        }
    }
}
