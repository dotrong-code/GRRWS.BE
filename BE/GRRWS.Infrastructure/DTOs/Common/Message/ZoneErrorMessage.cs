using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Common.Message
{
    public static class ZoneErrorMessage
    {
        public static Error ZoneNotExist() =>  Error.Validation("Zone.NotExist", "Zone does not exist.");
        public static Error ZoneDeleteFailed() => Error.Validation("Zone.DeleteFailed", "Failed to delete zone.");
        public static Error InvalidArea() => Error.Validation("Zone.InvalidArea", "Invalid area selected.");

        public static Error FieldIsEmpty(string fieldName) => Error.Validation("Zone.FieldIsEmpty", $"{fieldName} is required.");
        public static Error FieldTooLong(string fieldName, int maxLength) => Error.Validation("Zone.FieldTooLong", $"{fieldName} must not exceed {maxLength} characters.");
        public static Error AreaNotExist() => Error.Validation("Zone.AreaNotExist", "Area does not exist.");
    }
}
