using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Validator.Abstract;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.User.Register;

namespace GRRWS.Application.Common.Validator.UserVali
{
    public class RegisterValidator : UserValidator<RegisterRequest>
    {
        public RegisterValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            // Check if the email and username already exist for registration
            AddEmailRules(request => request.Email, checkExists: true);
            AddUserNameRules(request => request.UserName, checkExists: true);
            AddPasswordRules(request => request.Password);
            AddFullNameRules(request => request.FullName);
            AddPhoneNumberRules(request => request.PhoneNumber);
            AddBirthdayRules(x => x.DateOfBirth);
        }
    }
}
