using AutoService.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoService.API.Features.Identity
{
    public interface ILoginService
    {
        string GenerateJSONWebToken(int userId, string userName, string secret);
        Task<User> FindUserAsync(string username);
    }
}
