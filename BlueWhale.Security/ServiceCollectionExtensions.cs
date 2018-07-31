using System.Text;
using BlueWhale.Security.Data;
using BlueWhale.Security.Models;
using BlueWhale.Security.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BlueWhale.Security
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection serviceCollection, string issuer, string audience, string key)
        {
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
        }

        public static void ConfigureDbContext(this IServiceCollection serviceCollection, string connectionString)
        {
            //serviceCollection.AddDbContext<UsersContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            serviceCollection.AddDbContext<UsersContext>(options => options.UseSqlServer(connectionString));
        }

        public static void ConfigureIdentity(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddIdentity<User, UserRole>().AddEntityFrameworkStores<UsersContext>()
                .AddDefaultTokenProviders();
        }

        public static void RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITokenService, TokenService>();
        }
    }
}
