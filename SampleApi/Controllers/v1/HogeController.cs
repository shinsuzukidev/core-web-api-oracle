using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using SampleApi.Models.Hoge;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SampleApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HogeController : ControllerBase
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            //return Content("Hoge Index");
            return new JsonResult("index");
        }

        [HttpGet("GetName")]
        public IActionResult GetName(int id)
        {
            return new JsonResult(
                new GetNameResponse() { Id = id, Name = "sato" },
                new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
# if DEBUG
                    WriteIndented = true
#endif
                });
        }
    }
}
