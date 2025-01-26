using System.Text.Json.Serialization;

namespace SampleApi.Models.Hoge
{
    public class GetAgeResponse: BaseResponse
    {
        [JsonPropertyOrder(10)]
        public int Age { get; set; }    
    }
}
