using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Validator.Abstract;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Device;

namespace GRRWS.Application.Common.Validator.DeviceVali
{
    public class UpdateDeviceValidator : DeviceValidator<UpdateDeviceRequest>
    {
        public UpdateDeviceValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddDeviceNameRules(request => request.DeviceName);
            
            AddSerialNumberRules(request => request.SerialNumber);
            AddStatusRules(request => request.Status);
            AddManufactureDateRules(request => request.ManufactureDate);
            AddInstallationDateRules(request => request.InstallationDate);
            AddPurchasePriceRules(request => request.PurchasePrice);
        }
    }
}
