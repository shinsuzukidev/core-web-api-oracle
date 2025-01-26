using System.Text.Json.Serialization;

namespace SampleApi.Models.Hoge
{
    public class IndexResponse: BaseResponse
    {
        [JsonPropertyOrder(10)]
        public string Configuration { get; set; } = string.Empty;

        [JsonPropertyOrder(20)]
        public string Settings{ get; set;  } = string.Empty;
    }
}
