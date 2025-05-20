using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GRRWS.Infrastructure.DTOs.Common.Message;
using GRRWS.Infrastructure.DTOs.Task;

namespace GRRWS.Application.Common.Validator.Task
{
    public class CreateTaskReportRequestValidator : AbstractValidator<CreateTaskReportRequest>
    {
        public CreateTaskReportRequestValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithState(_ => TaskErrorMessage.InvalidTaskId());

            RuleFor(x => x.DeviceCondition)
                .NotEmpty().WithState(_ => TaskErrorMessage.InvalidDeviceCondition())
                .Must(x => new[] { "Repaired", "Partially Repaired", "Unrepaired" }.Contains(x))
                .WithState(_ => TaskErrorMessage.InvalidDeviceCondition());

            RuleFor(x => x.DeviceReturnTime)
                .NotEmpty().WithState(_ => TaskErrorMessage.InvalidDeviceReturnTime())
                .LessThanOrEqualTo(DateTime.UtcNow).WithState(_ => TaskErrorMessage.InvalidDeviceReturnTime());

            RuleFor(x => x.ReportNotes)
                .MaximumLength(1000).WithMessage("Report notes must not exceed 1000 characters.");
        }
    }
}
