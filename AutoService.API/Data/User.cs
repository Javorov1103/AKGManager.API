using AutoService.API.Features.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AutoService.API.Data
{
    public class User : GenericIdentity, IEntity
    {
        public User() : base("", "Custom")
        {

        }
        public User(RegisterRequestModel model) : base(model.UserName, "Custom")
        {
            this.UserName = model.UserName;
            this.Email = model.Email;
            this.Password = model.Password;
        }

        public int Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public bool IsActive { get; set; }
    }
}
