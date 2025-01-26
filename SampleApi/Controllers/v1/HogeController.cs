using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using SampleApi.Models.Hoge;
using System.Text.Json;
using System.Text.Json.Serialization;
using SampleApi.Util.Config;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Runtime;

namespace SampleApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HogeController : ApiControllerBase
    {
        // appsetting.jsonを読み込みます
        private IConfiguration _configuration;
        private IMyConfig _config;

        public HogeController(IConfiguration configuration, IMyConfig config)
        {
            _configuration = configuration;
            _config = config;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            //var settings = _config.GetConfigurationRoot().GetValue<string>("title");

            //return this.ToJsonResult<IndexResponse>(
            //    new IndexResponse()
            //    {
            //        Configuration = _configuration!["MyInfo:Name"],  // appsetting.json
            //        Settings = _config.GetConfigurationRoot()["properties:type:description"]    // custom, mysettings.json
            //    });


            MySettings mySettings = new MySettings()
            {
                Title = "myHome",
                Type = "myType",
                Properties = new Properties
                {
                    Name = new Name
                    {
                        Type = "type-a",
                        Description = "説明!"
                    }
                }
            };

            return this.ToJsonResult<MySettings>(mySettings);
        }

        [HttpGet("GetName")]
        public IActionResult GetName(int id)
        {
            //            return new JsonResult(
            //                new GetNameResponse() { Status = (int)HttpStatusCode.OK  ,Id = id, Name = "sato" },
            //                new JsonSerializerOptions()
            //                {
            //                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            //#if DEBUG
            //                    WriteIndented = true
            //#endif
            //                });

            throw new Exception("myException");

            return this.ToJsonResult<GetNameResponse>(
                new GetNameResponse()
                {
                    Status = (int)HttpStatusCode.OK,
                    Id = id,
                    Name = "sato"
                });
        }

        [HttpGet("GetAge")]
        public IActionResult GetAge(int id)
        {
            // req.idにより検索

            // baseクラスを使用したレスポンス
            return this.ToJsonResult(new GetAgeResponse()
            { 
                Status = (int)HttpStatusCode.OK ,
                Age = 20
            });
        }

        [HttpPost("GetUser")]
        public IActionResult GetUser(GetUserRequest req)
        {
            // req.idにより検索

            // レスポンス
            //            return new JsonResult(
            //                new GetUserResponse() { id = req.id, Name="kato", Age=20},
            //                new JsonSerializerOptions()
            //                {
            //                    DefaultIgnoreCondition= JsonIgnoreCondition.WhenWritingNull,
            //#if DEBUG
            //                    WriteIndented = true
            //#endif
            //                });

            return this.ToJsonResult(new GetUserResponse()
            {
                Status = (int)HttpStatusCode.OK,
                id = req.id,
                Name = "kato",
                Age = 20
            });

        }

    }
}
