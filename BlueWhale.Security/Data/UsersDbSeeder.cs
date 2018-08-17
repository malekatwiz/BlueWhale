using System;
using System.Linq;
using System.Threading.Tasks;
using BlueWhale.Security.Models;
using Microsoft.AspNetCore.Identity;

namespace BlueWhale.Security.Data
{
    public class UsersDbSeeder
    {
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

        public async Task SeedAsync(UsersDbContext context)
        {
            if (!context.Users.Any())
            {
                var user = new ApplicationUser
                {
                    UserName = "malek",
                    NormalizedUserName = "malek",
                    Email = "malek@somesite.com",
                    NormalizedEmail = "malek@somesite.com",
                    FirstName = "Malek",
                    LastName = "A",
                    Id = Guid.NewGuid().ToString(),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    EmailConfirmed = true
                };

                user.PasswordHash = _passwordHasher.HashPassword(user, "password");

                context.Users.Add(user);

                await context.SaveChangesAsync();
            }
        }
    }
}
