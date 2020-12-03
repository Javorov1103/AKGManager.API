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
    
    public class IdentityController : ApiController
    {
        private readonly IIdentityService identityService;
        private readonly AppSettings appSettings;

        public IdentityController(IIdentityService identityService, IOptions<AppSettings> appSettings)
        {
            this.identityService = identityService;
            this.appSettings = appSettings.Value;
        }
       
        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel model)
        {
            User user = await this.identityService.FindUserAsync(model.UserName);

            if (user == null)
            {
                return Unauthorized();
            }

            bool passwordValid = await this.identityService.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                return Unauthorized();
            }

            var token = this.identityService.GenerateJwtToken(
                user.Id,
                user.UserName,
                this.appSettings.Secret);

            return new LoginResponseModel
            {
                Token = token
            };
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };

            bool result = await this.identityService.CreateAsync(user, model.Password);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
        
    }
}
