using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Common.Message
{
    public static class DeviceWarrantyErrorMessage
    {
        public static Error DeviceWarrantyNotExist() =>  Error.Validation("DeviceWarranty.NotExist", "Device warranty does not exist.");
        public static Error DeviceWarrantyDeleteFailed() => Error.Validation("DeviceWarranty.DeleteFailed", "Failed to delete device warranty.");
        public static Error InvalidDevice() => Error.Validation("DeviceWarranty.InvalidDevice", "Invalid device selected.");
        public static Error FieldIsEmpty(string fieldName) => Error.Validation("DeviceWarranty.FieldIsEmpty", $"{fieldName} is required.");
        public static Error InvalidStatus() => Error.Validation("DeviceWarranty.InvalidStatus", "Invalid status. Must be Pending, InProgress, Completed, or Rejected.");
        public static Error InvalidWarrantyType() => Error.Validation("DeviceWarranty.InvalidWarrantyType", "Invalid warranty type. Must be Manufacturer, Extended, or ThirdParty.");
        public static Error InvalidDateRange() => Error.Validation("DeviceWarranty.InvalidDateRange", "Warranty start date must be before or equal to end date.");
        public static Error DeviceNotExist() => Error.Validation("DeviceWarranty.DeviceNotExist", "Device does not exist.");
        
    }
}
