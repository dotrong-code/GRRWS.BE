using FluentValidation;
using GRRWS.Infrastructure.DTOs.Task.Get;

namespace GRRWS.Application.Common.Validator.Task
{
    public class GetTasksByRequestIdValidator : AbstractValidator<GetTasksByRequestIdRequest>
    {
        public GetTasksByRequestIdValidator()
        {
            RuleFor(x => x.RequestId)
                .NotEmpty()
                .WithMessage("Request ID is required")
                .NotEqual(Guid.Empty)
                .WithMessage("Request ID cannot be empty");

            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0")
                .LessThanOrEqualTo(100)
                .WithMessage("Page size cannot exceed 100");
        }
    }
}