using GRRWS.Application.Common.Validator.Abstract;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.User.Create;

namespace GRRWS.Application.Common.Validator.UserVali
{
    public class CreateUserValidator : UserValidator<CreateUserRequest>
    {
        public CreateUserValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddEmailRules(request => request.Email, checkExists: true);
            AddUserNameRules(request => request.UserName, checkExists: true);
            // AddFullNameRules(request => request.FullName);
            // AddPhoneNumberRules(request => request.PhoneNumber);
            // AddBirthdayRules(x => x.DateOfBirth);
            AddPasswordRules(x => x.Password);
        }
    }
}