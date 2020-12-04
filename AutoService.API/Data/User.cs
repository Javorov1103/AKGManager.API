using AutoService.API.Features.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AutoService.API.Data
{
    public class User : IdentityUser<int>, IEntity
    {
        public User()
        {

        }

        public User(RegisterRequestModel model)
        {
            this.UserName = model.UserName;
            this.Email = model.Email;
            this.PasswordHash = model.Password;
        }

        public DateTime CreatedOn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CreatedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? ModifiedOn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ModifiedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
