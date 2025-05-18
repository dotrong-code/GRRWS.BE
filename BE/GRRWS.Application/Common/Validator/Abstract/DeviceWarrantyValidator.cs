using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common.Message;

namespace GRRWS.Application.Common.Validator.Abstract
{
    public abstract class DeviceWarrantyValidator<T> : AbstractValidator<T>
    {
        private readonly UnitOfWork _unitOfWork;

        protected DeviceWarrantyValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Validation for Status
        protected void AddStatusRules(Expression<Func<T, string>> statusExpression)
        {
            RuleFor(statusExpression)
                .NotEmpty().WithState(_ => DeviceWarrantyErrorMessage.FieldIsEmpty("Status"))
                .Must(status => new[] { "Pending", "InProgress", "Completed", "Rejected" }.Contains(status))
                .WithState(_ => DeviceWarrantyErrorMessage.InvalidStatus());
        }

        // Validation for WarrantyType
        protected void AddWarrantyTypeRules(Expression<Func<T, string>> warrantyTypeExpression)
        {
            RuleFor(warrantyTypeExpression)
                .NotEmpty().WithState(_ => DeviceWarrantyErrorMessage.FieldIsEmpty("Warranty type"))
                .Must(type => new[] { "Manufacturer", "Extended", "ThirdParty" }.Contains(type))
                .WithState(_ => DeviceWarrantyErrorMessage.InvalidWarrantyType());
        }

        // Validation for Warranty Dates
        protected void AddWarrantyDateRules(
            Expression<Func<T, DateTime?>> startDateExpression,
            Expression<Func<T, DateTime?>> endDateExpression)
        {
            RuleFor(startDateExpression)
                .Must((model, startDate) => !startDate.HasValue || !endDateExpression.Compile()(model).HasValue || startDate <= endDateExpression.Compile()(model))
                .WithState(_ => DeviceWarrantyErrorMessage.InvalidDateRange())
                .When(model => startDateExpression.Compile()(model).HasValue);

            RuleFor(endDateExpression)
                .Must((model, endDate) => !endDate.HasValue || !startDateExpression.Compile()(model).HasValue || endDate >= startDateExpression.Compile()(model))
                .WithState(_ => DeviceWarrantyErrorMessage.InvalidDateRange())
                .When(model => endDateExpression.Compile()(model).HasValue);
        }

        // Validation for DeviceId
        protected void AddDeviceIdRules(Expression<Func<T, Guid>> deviceIdExpression)
        {
            RuleFor(deviceIdExpression)
                .NotEmpty().WithState(_ => DeviceWarrantyErrorMessage.FieldIsEmpty("DeviceId"))
                .MustAsync(async (deviceId, cancellation) => await _unitOfWork.DeviceRepository.GetByIdAsync(deviceId) != null)
                .WithState(_ => DeviceWarrantyErrorMessage.DeviceNotExist());
        }

        // Validation for Id (for update requests)
        protected void AddIdRules(Expression<Func<T, Guid>> idExpression)
        {
            RuleFor(idExpression)
                .NotEmpty().WithState(_ => DeviceWarrantyErrorMessage.FieldIsEmpty("Id"));
        }
    }
}
