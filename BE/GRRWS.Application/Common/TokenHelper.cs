﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Infrastructure.DTOs.Common;
using Microsoft.AspNetCore.Http;

namespace GRRWS.Application.Common
{
    public class TokenHelper
    {
        private static TokenHelper instance;
        public static TokenHelper Instance
        {
            get { if (instance == null) instance = new TokenHelper(); return Common.TokenHelper.instance; }
            private set { Common.TokenHelper.instance = value; }
        }
        public async Task<CurrentUserObject> GetThisUserInfo(HttpContext httpContext)
        {

            // Check if the user has an "email" claim
            var checkUser = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (checkUser == null)
            {
                return null;  // Return early if the user is not authenticated
            }

            // Create a new CurrentUserObject and populate its properties
            var currentUser = new CurrentUserObject
            {
                Email = checkUser.Value,
                Fullname = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                PhoneNumber = httpContext.User.Claims.FirstOrDefault(c => c.Type == "phone")?.Value,
                RoleName = httpContext.User.Claims.FirstOrDefault(c => c.Type == "role")?.Value
            };

            // Convert UserId claim (string) to Guid
            var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (Guid.TryParse(userIdClaim, out Guid userId))
            {
                currentUser.UserId = userId;
            }
            else
            {
                // Handle invalid or missing UserId claim
                currentUser.UserId = Guid.Empty;  // Or any default handling you prefer
            }



            return currentUser;
        }
    }
}
