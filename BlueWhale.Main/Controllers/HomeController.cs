using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlueWhale.Main.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace BlueWhale.Main.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GetValues()
        {
            var tokenClient = new TokenClient("http://10.0.75.1:5100/connect/token", "BlueWhale.Main", "Secretcode");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("BlueWhale.Exchange");

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var content = await client.GetStringAsync($"{_configuration["Exchange.Url"]}/api/values");

            return Ok(content);
        }
    }
}
