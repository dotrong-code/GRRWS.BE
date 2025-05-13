using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Infrastructure.DTOs.Common;
using Microsoft.IdentityModel.Tokens;

namespace GRRWS.Application.Interface.IService
{
    public interface ITokenService
    {
        Task<SecurityToken> GenerateTokenAsync(CurrentUserObject currentUserObject);
        Task<dynamic> RenewTokenAsync(RenewTokenDTO tokenDTO);
        Task<string> GenerateAccessTokenAsync(SecurityToken token);
    }
}
