using System.Threading.Tasks;
using BlueWhale.Security.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlueWhale.Security.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interactionService;

        public HomeController(IIdentityServerInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        public IActionResult Index()
        {
            return View(string.Empty);
        }

        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();
            
           var message = await _interactionService.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);

        }
    }
}