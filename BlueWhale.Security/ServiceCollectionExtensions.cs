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
            serviceCollection.AddIdentityServer()
                .AddDeveloperSigningCredential(false)
                .AddInMemoryClients(IdentityServerConfig.GetClients(configuration))
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources());
        }
    }
}
