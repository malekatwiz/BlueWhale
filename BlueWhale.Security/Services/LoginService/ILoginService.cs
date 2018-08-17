using System.Threading.Tasks;
using BlueWhale.Security.Models;

namespace BlueWhale.Security.Services.LoginService
{
    public interface ILoginService
    {
        Task<ApplicationUser> FindByUsername(string username);
        Task<bool> ValidateCredentials(ApplicationUser user, string password);
        Task SignIn(ApplicationUser user);
    }
}
