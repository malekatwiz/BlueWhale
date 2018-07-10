using Microsoft.AspNetCore.Identity;

namespace BlueWhale.Security.Models
{
    public class UserRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
