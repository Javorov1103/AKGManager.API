using AutoService.API.Data;
using AutoService.API.Features.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace AutoService.API.Features
{
    
    public class IdentityController : ApiController
    {
       
        private readonly IIdentityService identityService;
        private readonly AppSettings appSettings;

        public IdentityController( IIdentityService identityService, IOptions<AppSettings> appSettings)
        {
            this.identityService = identityService;
            this.appSettings = appSettings.Value;
        }
       
        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel model)
        {

            User user = await this.identityService.FindUserAsync(model);

            Thread.CurrentPrincipal = new GenericPrincipal( user, new string[] { });

            if (user == null)
            {
                return Unauthorized();
            }


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
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", "value3", "value4", "value5", AppContext.User.UserName, AppContext.User.Name };
        }

    }
}
