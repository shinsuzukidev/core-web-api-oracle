using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Models.Hoge;
using SampleApi.Util.Config;
using System.Globalization;
using CsvHelper;
using System.Net;
using CsvHelper.Configuration;
using System.Text;

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

        // アプリケーションルートの物理パスを取得
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HogeController(IConfiguration configuration, IMyConfig config, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
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
                        Description = "説明"
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

        [HttpGet("GetFile")]
        public IActionResult GetFile()
        {
            var filePath = Path.GetTempFileName();
            System.IO.File.WriteAllText(filePath, "あいうえお", System.Text.Encoding.UTF8);

            //Read the File into a Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);
            return File(bytes, "application/octet-stream", "aaa.txt");
        }

        [HttpGet("GetCsvFile")]
        public IActionResult GetCsvFile()
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string physicalPath = $"{contentRootPath}\\_Tmp\\";


            List<CsvFileA> csvList = new List<CsvFileA>
            {
                new CsvFileA {ID=1, Title="タイトルa1", IsDone= true },
                new CsvFileA {ID=2, Title="タイトルb1", IsDone= true },
                new CsvFileA {ID=3, Title="タイトルc1", IsDone= true },
            };

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                //ダブルクォーテーション付きにする
                ShouldQuote = (context) => true,
            };


            // utf8,bomなし
            if (false)
            {
                using (var ms = new MemoryStream())
                {
                    using (var sw = new StreamWriter(ms, new UTF8Encoding(false)))
                    {
                        using (var csvwriter = new CsvWriter(sw, config))
                        {
                            csvwriter.WriteRecords(csvList);
                            sw.Flush();
                            byte[] csv = ms.ToArray();
                            var fileName = $"test_{DateTime.Now:yyyyMMdd_HHmmss}_utf8.csv";
                            //System.IO.File.WriteAllBytes(Path.Combine(physicalPath, fileName), csv);
                            return File(csv, "text/csv", fileName);
                        }
                    }
                }
            }

            // utf8,bomあり
            if (false)
            {
                using (var ms = new MemoryStream())
                {
                    using (var sw = new StreamWriter(ms, new UTF8Encoding(true)))
                    {
                        using (var csvwriter = new CsvWriter(sw, config))
                        {
                            csvwriter.WriteRecords(csvList);
                            sw.Flush();
                            byte[] csv = ms.ToArray();
                            var fileName = $"test_{DateTime.Now:yyyyMMdd_HHmmss}_utf8bom.csv";
                            //System.IO.File.WriteAllBytes(Path.Combine(physicalPath, fileName), csv);
                            return File(csv, "text/csv", fileName);
                        }
                    }
                }
            }

            // shift-jis
            if (true)
            {
                // note: Shift-JISを扱う
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (var ms = new MemoryStream())
                {
                    using (var sw = new StreamWriter(ms, System.Text.Encoding.GetEncoding("shift_jis")))
                    {
                        using (var cw = new CsvWriter(sw, config))
                        {
                            cw.WriteRecords(csvList);
                            sw.Flush();
                            byte[] csv = ms.ToArray();
                            var fileName = $"test_{DateTime.Now:yyyyMMdd_HHmmss}_sjis.csv";
                            //System.IO.File.WriteAllBytes(Path.Combine(physicalPath, fileName), csv);
                            //Response.Headers.Append("Content-Type", "text/csv; charset=cp932");
                            //Response.Headers.Append("Content-Disposition", $"attachment;filename={fileName}");
                            return File(csv, "text/csv", fileName);
                        }
                    }
                }
            }
        }

        [HttpGet("GetImageFile")]
        public IActionResult GetImageFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return NotFound("fileName is empty.");
            }

            // アプリケーションルートの物理パスを取得
            string contentRootPath = _webHostEnvironment.ContentRootPath;

            // ダウンロードするファイルの物理パス
            string physicalPath = $"{contentRootPath}\\_File\\{fileName}";

            if (!System.IO.File.Exists(physicalPath))
            {
                return NotFound("file not found.");
            }

            // Content-Disposition ヘッダを設定（RFC 6266 対応してない）
            Response.Headers.Append("Content-Disposition", $"attachment;filename={fileName}");

            return new PhysicalFileResult(physicalPath, "image/jpeg");

        }

        [HttpPost("Upload")]
        public IActionResult Upload([FromForm] UploadFile uploadFile)
        {
            if (uploadFile.File != null && uploadFile.File.Length > 0)
            {
                var reader = new StreamReader(uploadFile.File.OpenReadStream());
                var content = reader.ReadToEnd();
                System.Diagnostics.Debug.Write(content);
                return Ok();
            }

            return BadRequest();
        }
    }
}
