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
    public class CreateDeviceWarrantyRequestValidator : DeviceWarrantyValidator<CreateDeviceWarrantyRequest>
    {
        public CreateDeviceWarrantyRequestValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddStatusRules(request => request.Status);
            AddWarrantyTypeRules(request => request.WarrantyType);
            AddWarrantyDateRules(request => request.WarrantyStartDate, request => request.WarrantyEndDate);
            AddDeviceIdRules(request => request.DeviceId);
        }
    }
}
