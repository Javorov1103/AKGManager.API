﻿using AutoService.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoService.API.Features.Identity
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret);
        Task<User> FindUserAsync(LoginRequestModel username);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<bool> CreateAsync(User user, string password);
    }
}
