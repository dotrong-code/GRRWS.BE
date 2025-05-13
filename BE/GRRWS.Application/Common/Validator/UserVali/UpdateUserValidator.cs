using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Validator.Abstract;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.User.Update;

namespace GRRWS.Application.Common.Validator.UserVali
{
    public class UpdateUserValidator : UserValidator<UpdateUserRequest>
    {
        public UpdateUserValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddEmailRules(request => request.Email, checkExists: true);
            AddUserNameRules(request => request.UserName, checkExists: true);
            AddFullNameRules(request => request.FullName);
            AddPhoneNumberRules(request => request.PhoneNumber);
            AddBirthdayRules(x => x.DateOfBirth);
        }
    }
}
