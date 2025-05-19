using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common.Message;

namespace GRRWS.Application.Common.Validator.Abstract
{
    public abstract class DeviceValidator<T> : AbstractValidator<T>
    {
        private readonly UnitOfWork _unitOfWork;

        public DeviceValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Validation for DeviceName
        protected void AddDeviceNameRules(Expression<Func<T, string>> deviceNameExpression)
        {
            RuleFor(deviceNameExpression)
                .NotEmpty().WithState(_ => DeviceErrorMessage.FieldIsEmpty("Device name"))
                .MinimumLength(3).WithState(_ => DeviceErrorMessage.InValidLength());
        }

        // Validation for DeviceCode (with option to check uniqueness)
        

        // Validation for SerialNumber (optional, but must be valid if provided)
        protected void AddSerialNumberRules(Expression<Func<T, string>> serialNumberExpression)
        {
            RuleFor(serialNumberExpression)
                .MaximumLength(50).WithState(_ => DeviceErrorMessage.SerialNumberInvalidLength())
                .When(x => !string.IsNullOrEmpty(serialNumberExpression.Compile()(x)));
        }

        // Validation for Status
        protected void AddStatusRules(Expression<Func<T, string>> statusExpression)
        {
            var validStatuses = new[] { "Active", "Inactive", "InRepair", "Retired" };
            RuleFor(statusExpression)
                .NotEmpty().WithState(_ => DeviceErrorMessage.FieldIsEmpty("Status"))
                .Must(status => validStatuses.Contains(status))
                .WithState(_ => DeviceErrorMessage.InvalidStatus());
        }

        // Validation for ManufactureDate
        protected void AddManufactureDateRules(Expression<Func<T, DateTime?>> manufactureDateExpression)
        {
            RuleFor(manufactureDateExpression)
                .Must(NotBeInFuture).WithState(_ => DeviceErrorMessage.DateCannotBeInFuture("Manufacture date"))
                .When(x => manufactureDateExpression.Compile()(x).HasValue);
        }

        // Validation for InstallationDate
        protected void AddInstallationDateRules(Expression<Func<T, DateTime?>> installationDateExpression)
        {
            RuleFor(installationDateExpression)
                .Must(NotBeInFuture).WithState(_ => DeviceErrorMessage.DateCannotBeInFuture("Installation date"))
                .When(x => installationDateExpression.Compile()(x).HasValue);
        }

        // Validation for PurchasePrice
        protected void AddPurchasePriceRules(Expression<Func<T, decimal?>> purchasePriceExpression)
        {
            RuleFor(purchasePriceExpression)
                .GreaterThanOrEqualTo(0).WithState(_ => DeviceErrorMessage.PurchasePriceInvalid())
                .When(x => purchasePriceExpression.Compile()(x).HasValue);
        }

        // Helper method to validate that the date is not in the future
        private bool NotBeInFuture(DateTime? date)
        {
            return !date.HasValue || date.Value.Date <= DateTime.Today;
        }
    }
}
