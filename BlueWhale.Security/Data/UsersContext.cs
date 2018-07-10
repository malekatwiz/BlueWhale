using BlueWhale.Security.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlueWhale.Security.Data
{
    public class UsersContext : IdentityDbContext<User, UserRole, string>
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {

        }
    }
}
