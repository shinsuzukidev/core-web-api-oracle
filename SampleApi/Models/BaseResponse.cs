using System.Text.Json.Serialization;

namespace SampleApi.Models
{
    public class BaseResponse
    {
        [JsonPropertyOrder(1)]
        public int Status { get; set; }
    }
}
