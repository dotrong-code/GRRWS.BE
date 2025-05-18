using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Common.Message
{
    public static class PositionErrorMessage
    {
        public static Error PositionNotExist() =>  Error.Validation("Position.NotExist", "Position does not exist.");
        public static Error PositionDeleteFailed() => Error.Validation("Position.DeleteFailed", "Failed to delete position.");
        public static Error InvalidZone() =>  Error.Validation("Position.InvalidZone", "Invalid zone selected.");
        public static Error FieldIsEmpty(string fieldName) =>
            Error.Validation("Position.FieldEmpty", $"{fieldName} cannot be empty.");
        public static Error FieldInvalid(string fieldName) =>
            Error.Validation("FieldInvalid.FieldEmpty", $"{fieldName} cannot be empty.");
        public static Error ZoneNotExist() =>
            Error.Validation("Device.ZoneNotExist", "Specified zone does not exist.");
    }
}
