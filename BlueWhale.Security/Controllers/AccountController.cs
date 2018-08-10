using System.Threading.Tasks;
using BlueWhale.Security.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlueWhale.Security.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _identityServerInteractionService;

        public AccountController(IIdentityServerInteractionService identityServerInteractionService)
        {
            _identityServerInteractionService = identityServerInteractionService;
        }

        [HttpGet, Route("login")]
        public async Task<IActionResult> LoginAsync(string returnUrl)
        {
            var context = await _identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            var model = new LoginModel
            {
                ReturnUrl = returnUrl,
                Username = context?.LoginHint
            };

            return View(model);
        }
    }
}