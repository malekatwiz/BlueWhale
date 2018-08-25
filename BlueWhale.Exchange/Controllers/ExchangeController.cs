using Microsoft.AspNetCore.Mvc;

namespace BlueWhale.Exchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        [HttpPost, Route("send")]
        public JsonResult Send(string message)
        {
            return new JsonResult("OK");
        }
    }
}