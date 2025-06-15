using FluentValidation;
using GRRWS.Infrastructure.DTOs.Task.Get;
using GRRWS.Domain.Enum;

namespace GRRWS.Application.Validators.Task
{
    public class GetAllSingleTasksValidator : AbstractValidator<GetAllSingleTasksRequest>
    {
        public GetAllSingleTasksValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0")
                .LessThanOrEqualTo(100)
                .WithMessage("Page size cannot exceed 100");

            RuleFor(x => x.TaskType)
                .Must(BeValidTaskType)
                .WithMessage("Invalid task type")
                .When(x => !string.IsNullOrEmpty(x.TaskType));

            RuleFor(x => x.Status)
                .Must(BeValidStatus)
                .WithMessage("Invalid status")
                .When(x => !string.IsNullOrEmpty(x.Status));

            RuleFor(x => x.Priority)
                .Must(BeValidPriority)
                .WithMessage("Invalid priority")
                .When(x => !string.IsNullOrEmpty(x.Priority));
        }

        private bool BeValidTaskType(string taskType)
        {
            return Enum.TryParse<TaskType>(taskType, true, out _);
        }

        private bool BeValidStatus(string status)
        {
            return Enum.TryParse<Status>(status, true, out _);
        }

        private bool BeValidPriority(string priority)
        {
            return Enum.TryParse<Priority>(priority, true, out _);
        }
    }
}