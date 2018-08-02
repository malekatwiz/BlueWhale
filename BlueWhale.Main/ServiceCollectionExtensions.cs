using Microsoft.Extensions.DependencyInjection;

namespace BlueWhale.Main
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection serviceCollection, string securityUrl)
        {
            serviceCollection.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";
                    options.Authority = securityUrl;
                    options.RequireHttpsMetadata = false;
                    options.ClientId = "BlueWhale.Main";
                    options.SaveTokens = true;
                });
        }
    }
}
