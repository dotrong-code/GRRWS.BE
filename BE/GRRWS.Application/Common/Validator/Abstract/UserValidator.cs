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
    public abstract class UserValidator<T> : AbstractValidator<T>
    {
        private readonly UnitOfWork _unitOfWork;

        public UserValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Validation for UserName (with option to check existence)
        protected void AddUserNameRules(Expression<Func<T, string>> userNameExpression, bool checkExists = false)
        {
            RuleFor(userNameExpression)
                .NotEmpty().WithState(_ => UserErrorMessage.FieldIsEmpty("User name"))
                .MinimumLength(3).WithState(_ => UserErrorMessage.UserNameInValidLength());

            if (checkExists)
            {
                RuleFor(userNameExpression)
                    .MustAsync(async (userName, cancellation) =>
                        !(await _unitOfWork.UserRepository.UserNameExistsAsync(userName)))
                    .WithState(_ => UserErrorMessage.UserNameIsExist());
            }
        }

        // Validation for Full Name
        protected void AddFullNameRules(Expression<Func<T, string>> fullNameExpression)
        {
            RuleFor(fullNameExpression)
                .NotEmpty().WithState(_ => UserErrorMessage.FieldIsEmpty("Full name"));
        }

        // Validation for Email (with option to check existence)
        protected void AddEmailRules(Expression<Func<T, string>> emailExpression, bool checkExists = false)
        {
            RuleFor(emailExpression)
                .NotEmpty().WithState(_ => UserErrorMessage.FieldIsEmpty("Email"))
                .EmailAddress().WithState(_ => UserErrorMessage.EmailInValidFormat());

            if (checkExists)
            {
                RuleFor(emailExpression)
                    .MustAsync(async (email, cancellation) =>
                        !(await _unitOfWork.UserRepository.EmailExistsAsync(email)))
                    .WithState(_ => UserErrorMessage.EmailIsExist());
            }
        }

        // Shared validation for Password
        protected void AddPasswordRules(Expression<Func<T, string>> passwordFunc)
        {
            RuleFor(passwordFunc)
                .NotEmpty().WithState(_ => UserErrorMessage.FieldIsEmpty("Password"))
                .MinimumLength(8).WithState(_ => UserErrorMessage.PasswordInValidLength())
                .Matches(@"[A-Z]").WithState(_ => UserErrorMessage.PasswordInValidUppercase())
                .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithState(_ => UserErrorMessage.PasswordInValidSpecialChar());
        }

        // Validation for Phone Number
        protected void AddPhoneNumberRules(Expression<Func<T, string>> phoneNumberSelector)
        {
            RuleFor(phoneNumberSelector)
                .Matches(@"^\d{10,11}$").WithState(_ => UserErrorMessage.PhoneInvalidFormat());
        }

        // Validation for Address

        // Validation for Date of Birth
        protected void AddBirthdayRules(Expression<Func<T, DateTime>> birthdaySelector)
        {
            RuleFor(birthdaySelector)
                .NotEmpty().WithState(_ => UserErrorMessage.FieldIsEmpty("Date of Birth"))
                .Must(BeAtLeast18YearsOld).WithState(_ => UserErrorMessage.UserMustBeAtLeast18())
                .Must(NotBeInFuture).WithState(_ => UserErrorMessage.BirthdayCannotBeInFuture());
        }

        // Helper method to validate if the user is at least 18 years old
        private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age >= 18;
        }

        // Helper method to validate that the date is not in the future
        private bool NotBeInFuture(DateTime dateOfBirth)
        {
            return dateOfBirth.Date <= DateTime.Today;
        }
    }
}
