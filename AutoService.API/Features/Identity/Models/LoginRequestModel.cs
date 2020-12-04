namespace AutoService.API.Features.Identity
{
    using System.ComponentModel.DataAnnotations;

    public class LoginRequestModel
    {
        [Required]
        public int CompanyID { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
