using Microsoft.AspNetCore.Mvc;

namespace Projekt.Controllers
{
    [Route("/")]
    [ApiController]
    public class Test : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Ok();
        }
        
    }
}
