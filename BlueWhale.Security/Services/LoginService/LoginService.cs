using System.Threading.Tasks;
using BlueWhale.Security.Models;
using Microsoft.AspNetCore.Identity;

namespace BlueWhale.Security.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<ApplicationUser> FindByUsername(string username)
        {
            return _userManager.FindByEmailAsync(username);
        }

        public async Task<bool> ValidateCredentials(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public Task SignIn(ApplicationUser user)
        {
            return _signInManager.SignInAsync(user, true);
        }
    }
}
