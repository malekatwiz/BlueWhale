using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlueWhale.Security.Controllers;

namespace BlueWhale.Security.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public IEnumerable<ExternalProvider> ExternalProviders { get; set; }
    }
}
