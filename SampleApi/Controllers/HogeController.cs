using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HogeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Content("Hoge Index");
        }
    }
}
