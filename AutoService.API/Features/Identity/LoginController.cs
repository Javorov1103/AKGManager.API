using AutoService.API.Data;
using AutoService.API.Features.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.API.Features
{
    
    public class LoginController : ApiController
    {
        private readonly ILoginService _loginService;
        private readonly AppSettings _appSettings;

        public LoginController(ILoginService loginService, IOptions<AppSettings> appSettings)
        {
            _loginService = loginService;
            _appSettings = appSettings.Value;
        }
       
        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Login))]
        public async Task<ActionResult> Login([FromBody] LoginRequestModel login)
        {
            ActionResult response = Unauthorized();
            //var user = await _loginService.FindUserAsync(login.Username);
            var user = new User() { Id = 1, Username = "Kalin", Email = "k.javorov@gmail.com" };

            if (user != null)
            {
                var tokenString = _loginService.GenerateJSONWebToken(user.Id, user.Username,this._appSettings.Secret);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        //private string GenerateJSONWebToken(UserModel userInfo)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //      _config["Jwt:Issuer"],
        //      null,
        //      expires: DateTime.Now.AddMinutes(120),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //private string AuthenticateUser(LoginRequestModel login)
        //{
        //    User user = null;

        //    //Validate the User Credentials    
        //    //Demo Purpose, I have Passed HardCoded User Information    
        //    if (user.Username == "Jignesh")
        //    {
        //        user = new UserModel { Username = "Jignesh Trivedi", EmailAddress = "test.btest@gmail.com" };
        //    }
        //    return user;
        //}
    }
}
