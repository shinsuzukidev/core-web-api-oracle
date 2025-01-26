using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SampleApi.Controllers
{
    public class ApiControllerBase: ControllerBase
    {

        protected IActionResult ToJsonResult<T>(T res)
        {
            return new JsonResult(
                res,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
# if DEBUG
                    WriteIndented = true,
# endif
                });
        }
    }
}
