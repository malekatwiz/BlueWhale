using Microsoft.AspNetCore.Identity;

namespace BlueWhale.Security.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
