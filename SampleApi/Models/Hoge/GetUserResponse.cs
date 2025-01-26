using System.Text.Json.Serialization;

namespace SampleApi.Models.Hoge
{
    public class GetUserResponse: BaseResponse
    {
        [JsonPropertyOrder(10)]
        public int id { get; set; }
        [JsonPropertyOrder(20)]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyOrder(30)]
        public int Age { get; set; }
    }
}
