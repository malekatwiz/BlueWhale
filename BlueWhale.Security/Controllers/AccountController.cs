using System.Linq;
using System.Threading.Tasks;
using BlueWhale.Security.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;

namespace BlueWhale.Security.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;
        private readonly IClientStore _clientStore;

        public AccountController(IIdentityServerInteractionService identityServerInteractionService,
            IAuthenticationSchemeProvider authenticationSchemeProvider,
            IClientStore clientStore)
        {
            _identityServerInteractionService = identityServerInteractionService;
            _authenticationSchemeProvider = authenticationSchemeProvider;
            _clientStore = clientStore;
        }

        [HttpGet, Route("login")]
        public async Task<IActionResult> LoginAsync(string returnUrl)
        {
            var model = new LoginModel();

            var context = await _identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            var schemes = await _authenticationSchemeProvider.GetAllSchemesAsync();

            var providers = schemes.Where(x => !string.IsNullOrEmpty(x.DisplayName) ||
                                               string.Equals(x.Name, IISDefaults.AuthenticationScheme))
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName,
                    AuthenticationScheme = x.Name
                });

            if (context?.ClientId != null)
            {
                var client = await _clientStore.FindClientByIdAsync(context.ClientId);
                if (client?.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                {
                    providers = providers.Where(x =>
                        client.IdentityProviderRestrictions.Contains(x.AuthenticationScheme));
                }
            }

            model.ReturnUrl = returnUrl;
            model.Username = context?.LoginHint;
            model.ExternalProviders = providers;

            return View(model);
        }
    }

    public class ExternalProvider
    {
        public string DisplayName { get; set; }
        public string AuthenticationScheme { get; set; }
    }
}