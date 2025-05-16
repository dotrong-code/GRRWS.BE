using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Common.Message
{
    public static class DeviceErrorMessage
    {
        public static Error FieldIsEmpty(string fieldName) =>
            Error.Validation("Device.FieldEmpty", $"{fieldName} cannot be empty.");

        public static Error DeviceCodeExists() =>
            Error.Validation("Device.CodeExists", "Device code already exists.");

        public static Error SerialNumberExists() =>
            Error.Validation("Device.SerialNumberExists", "Serial number already exists.");

        public static Error DeviceNotExist() =>
            Error.Validation("Device.NotExist", "Device does not exist.");

        public static Error InvalidStatus() =>
            Error.Validation("Device.InvalidStatus", "Status must be 'Active', 'Inactive', 'InRepair', or 'Retired'.");

        public static Error DateCannotBeInFuture(string fieldName) =>
            Error.Validation("Device.DateInFuture", $"{fieldName} cannot be in the future.");

        public static Error InvalidPurchasePrice() =>
            Error.Validation("Device.InvalidPurchasePrice", "Purchase price must be non-negative.");

        public static Error ZoneNotExist() =>
            Error.Validation("Device.ZoneNotExist", "Specified zone does not exist.");

        public static Error MachineNotExist() =>
            Error.Validation("Device.MachineNotExist", "Specified machine does not exist.");

        public static Error PositionNotExist() =>
            Error.Validation("Device.PositionNotExist", "Specified position does not exist.");

        public static Error DeviceCreateFailed() =>
            Error.Validation("Device.CreateFailed", "Failed to create device.");

        public static Error DeviceUpdateFailed() =>
            Error.Validation("Device.UpdateFailed", "Failed to update device.");

        public static Error DeviceDeleteFailed() =>
            Error.Validation("Device.DeleteFailed", "Failed to delete device.");
    }
}
