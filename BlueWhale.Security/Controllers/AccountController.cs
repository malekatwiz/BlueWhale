using System.Threading.Tasks;
using BlueWhale.Security.Models;
using BlueWhale.Security.Services.LoginService;
using IdentityServer4.Events;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlueWhale.Security.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        private readonly IEventService _eventService;
        private readonly ILoginService _loginService;

        public AccountController(IIdentityServerInteractionService identityServerInteractionService,
            IEventService eventService,
            ILoginService loginService)
        {
            _identityServerInteractionService = identityServerInteractionService;
            _eventService = eventService;
            _loginService = loginService;
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

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _loginService.FindByUsername(model.Username);
                if (user != null && await _loginService.ValidateCredentials(user, model.Password))
                {
                    
                    //await _eventService.RaiseAsync(new UserLoginSuccessEvent(model.Username, user.SubjectId, user.Username));

                    //await HttpContext.SignInAsync(user.SubjectId, user.Username);
                    await _loginService.SignIn(user);

                    if (_identityServerInteractionService.IsValidReturnUrl(model.ReturnUrl))
                    {
                        Redirect(model.ReturnUrl);
                    }
                    return Redirect(_identityServerInteractionService.IsValidReturnUrl(model.ReturnUrl) ? 
                        model.ReturnUrl : "~/");
                }

                await _eventService.RaiseAsync(new UserLoginFailureEvent(model.Username, "Invalid credentials"));
            }
            return View(string.Empty);
        }
    }
}