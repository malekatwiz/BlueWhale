using BlueWhale.Security.Data;
using BlueWhale.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlueWhale.Security
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection serviceCollection)
        {
        }

        public static void ConfigureIdentityServer(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddIdentityServer(x => x.IssuerUri = "null")
                .AddDeveloperSigningCredential(false)
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients(configuration))
                .AddAspNetIdentity<ApplicationUser>();
        }

        public static void ConfigureUsersContext(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddDbContext<UsersDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("UsersDb"));
            });
        }

        public static void ConfigureAspIdentity(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
