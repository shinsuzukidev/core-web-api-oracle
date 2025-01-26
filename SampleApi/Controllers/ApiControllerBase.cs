using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

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
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,   // すべての null 値プロパティを無視する
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),          // エンコード対応
#if DEBUG
                    WriteIndented = true,
# endif
                });
        }
    }
}
