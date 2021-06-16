using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorWasmUI.Models.Api
{
    public class YapimciApiModel
    {
        [JsonPropertyName("value")] 
        public List<YapimciModel> Yapimcilar { get; set; }

        public YapimciModel Yapimci { get; set; }
    }
}
