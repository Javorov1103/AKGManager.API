using AutoService.API.Data;
using AutoService.API.Infrastructure.Services;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoService.API.Features.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IConnectionFactory connectionFactory;

        public IdentityService(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAsync(User user, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindUserAsync(LoginRequestModel model)
        {
            string companyConnString;
            //User user;

            var query = @"SELECT TOP 1 
		                [ID]
                      ,[CompanyID]
                      ,[ConnectionString]
                  FROM [Company]
                  WHERE CompanyID = @CompanyID";

            using (var connection = this.connectionFactory.GetStandardConnection())
            {
                var obj = await connection.QueryFirstOrDefaultAsync(query, new { CompanyID = model.CompanyID });

                companyConnString = obj.ConnectionString;
            }

            var findUserQuery = @"SELECT [ID]
                                  ,[Username]
                                  ,[Password]
                                  ,[Email]
                              FROM [User]
                              WHERE 
                              Password = @Password and Username = @UserName AND IsActive = 1";

            using (var connection = new SqlConnection(companyConnString))
            {
                return await connection.QueryFirstOrDefaultAsync<User>(findUserQuery, new { Username = model.UserName, Password = model.Password });
            }
        }

        public  User FindUserById(int companyID, int userID)
        {
            string companyConnString;
            //User user;

            var query = @"SELECT TOP 1 
		                [ID]
                      ,[CompanyID]
                      ,[ConnectionString]
                  FROM [Company]
                  WHERE CompanyID = @CompanyID";

            using (var connection = this.connectionFactory.GetStandardConnection())
            {
                var obj = connection.QueryFirstOrDefault(query, new { CompanyID = companyID });

                companyConnString = obj.ConnectionString;
            }

            var findUserQuery = @"SELECT [ID]
                                  ,[Username]
                                  ,[Password]
                                  ,[Email]
                              FROM [User]
                              WHERE 
                              ID = @UserID AND IsActive = 1";

            using (var connection = new SqlConnection(companyConnString))
            {
                return connection.QueryFirstOrDefault<User>(findUserQuery, new { UserID = userID });
            }
        }

        public string GenerateJwtToken(int companyID, string userId, string userName, string secret)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.GivenName, companyID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(365),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

    }
}
