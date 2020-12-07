using AutoService.API.Data;
using AutoService.API.Features.Identity;
using AutoService.API.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoService.API.Features
{
    
    public class IdentityController : ApiController
    {
       
        private readonly IIdentityService identityService;
        private readonly AppSettings appSettings;
        private readonly ICurrentUserService currentUserService;

        public IdentityController( IIdentityService identityService, IOptions<AppSettings> appSettings, ICurrentUserService currentUserService)
        {
            this.identityService = identityService;
            this.appSettings = appSettings.Value;
            this.currentUserService = currentUserService;
        }
       
        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel model)
        {

            User user = await this.identityService.FindUserAsync(model);

            
           

            if (user == null)
            {
                return Unauthorized();
            }

            //Thread.CurrentPrincipal = new GenericPrincipal( user, new string[] { });
            //this.httpContentAccessor.HttpContext.Items["User"] = user;

            this.currentUserService.SetUser(user);


            var token = this.identityService.GenerateJwtToken(
                model.CompanyID,
                user.Id.ToString(),
                user.UserName,
                this.appSettings.Secret); ;

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
            var user = new User(model);

            bool result = await this.identityService.CreateAsync(user, model.Password);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<object>> Get()
        {
            //return new string[] { "value1", "value2", "value3", "value4", "value5", AppContext.User.UserName, AppContext.User.Name };
            return new object[] { "value1", "value2", "value3", "value4", "value5", this.currentUserService.GetUserName() };
        }

    }
}
