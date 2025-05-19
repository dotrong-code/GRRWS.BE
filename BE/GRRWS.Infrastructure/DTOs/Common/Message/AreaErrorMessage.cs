using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Common.Message
{
    public static class AreaErrorMessage
    {
        public static Error AreaNotExist() =>  Error.Validation("Area.NotExist", "Area does not exist.");
        public static Error AreaDeleteFailed() => Error.Validation("Area.DeleteFailed", "Failed to delete area.");
        public static Error FieldIsEmpty(string fieldName) =>
            Error.Validation("Area.FieldEmpty", $"{fieldName} cannot be empty.");
    }
}
