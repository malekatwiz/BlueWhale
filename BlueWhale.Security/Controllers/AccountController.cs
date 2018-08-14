using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BlueWhale.Security.Models;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlueWhale.Security.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        private readonly IEventService _eventService;
        private readonly TestUserStore _users;

        public AccountController(IIdentityServerInteractionService identityServerInteractionService,
            IEventService eventService)
        {
            _identityServerInteractionService = identityServerInteractionService;
            _eventService = eventService;
            _users = new TestUserStore(new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "malek",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Name, "Malek"),
                        new Claim(JwtClaimTypes.Address, "Montreal"),
                        new Claim(JwtClaimTypes.Email, "malek@somesite.com")
                    }
                }
            });
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
                if (_users.ValidateCredentials(model.Username, model.Password))
                {
                    var user = _users.FindByUsername(model.Username);
                    await _eventService.RaiseAsync(new UserLoginSuccessEvent(model.Username, user.SubjectId, user.Username));

                    await HttpContext.SignInAsync(user.SubjectId, user.Username);

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