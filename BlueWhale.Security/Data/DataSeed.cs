using BlueWhale.Security.Models;
using Microsoft.AspNetCore.Identity;

namespace BlueWhale.Security.Data
{
    public static class DataSeed
    {
        public static void SeedTestData(UsersContext context)
        {
            var passwordHasher = new PasswordHasher<User>();
            var user = new User
            {
                UserName = "Malek",
                Email = "malek.atwiz@hotmail.com"
            };

            user.PasswordHash = passwordHasher.HashPassword(user, "MyPassword");

            context.Users.Add(user);

            context.SaveChanges();
        }
    }
}
