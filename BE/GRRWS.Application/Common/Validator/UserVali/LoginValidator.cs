using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Validator.Abstract;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.User.Login;


namespace GRRWS.Application.Common.Validator.UserVali
{
    public class LoginValidator : UserValidator<LoginRequest>
    {
        public LoginValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddEmailRules(request => request.Email);
            AddPasswordRules(request => request.Password);
        }
    }
}
