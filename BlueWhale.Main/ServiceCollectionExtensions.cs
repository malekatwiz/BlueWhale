using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlueWhale.Main
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
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
                    options.Authority = configuration["Security.Url"];
                    options.RequireHttpsMetadata = false;
                    options.ResponseType = "code id_token";
                    options.ClientSecret = "Secretcode";
                    options.ClientId = "BlueWhale.Main";
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                });
        }
    }
}
