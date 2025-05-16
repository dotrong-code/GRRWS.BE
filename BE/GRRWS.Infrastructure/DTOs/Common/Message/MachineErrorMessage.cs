using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Common.Message
{
    public static class MachineErrorMessage
    {
        public static Error FieldIsEmpty(string fieldName) =>
             Error.Validation("Machine.FieldEmpty", $"{fieldName} cannot be empty.");

        public static Error MachineCodeExists() =>
            Error.Validation("Machine.CodeExists", "Machine code already exists.");

        public static Error MachineNotExist() =>
            Error.Validation("Machine.NotExist", "Machine does not exist.");

        public static Error InvalidStatus() =>
            Error.Validation("Machine.InvalidStatus", "Status must be 'Active' or 'Discontinued'.");

        public static Error DateCannotBeInFuture(string fieldName) =>
            Error.Validation("Machine.DateInFuture", $"{fieldName} cannot be in the future.");

        public static Error MachineCreateFailed() =>
            Error.Validation("Machine.CreateFailed", "Failed to create machine.");

        public static Error MachineUpdateFailed() =>
            Error.Validation("Machine.UpdateFailed", "Failed to update machine.");

        public static Error MachineDeleteFailed() =>
            Error.Validation("Machine.DeleteFailed", "Failed to delete machine.");
    }
}
