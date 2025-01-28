using System.Runtime.Serialization;

namespace SampleApi.Models.Hoge
{
    public class UploadFile
    {
        [DataMember(Name = "file")]
        public IFormFile? File { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; } = string.Empty;
    }
}
