using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorWasmUI.Models.Api
{
    public class YorumApiModel
    {
        [JsonPropertyName("value")]
        public List<YorumModel> Yorumlar { get; set; }

        public YorumModel Yorum { get; set; }

        public List<OyunModel> Oyunlar { get; set; }
    }
}
