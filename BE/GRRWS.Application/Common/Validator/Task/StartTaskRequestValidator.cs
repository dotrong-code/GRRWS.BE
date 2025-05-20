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
    public class StartTaskRequestValidator : AbstractValidator<StartTaskRequest>
    {
        public StartTaskRequestValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithState(_ => TaskErrorMessage.InvalidTaskId());
        }
    }
}
