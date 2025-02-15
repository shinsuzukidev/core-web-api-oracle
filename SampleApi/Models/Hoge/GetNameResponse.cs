﻿using System.Text.Json.Serialization;

namespace SampleApi.Models.Hoge
{
    public class GetNameResponse: BaseResponse
    {
        [JsonPropertyOrder(10)]
        public int Id { get; set; }

        [JsonPropertyOrder(20)]
        public string Name { get; set; } = string.Empty;
    }
}
