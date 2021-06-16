using System.Collections.Generic;
using System.Text.Json.Serialization;
using BlazorWasmUI.Models.Filtering;

namespace BlazorWasmUI.Models.Api
{
    public class YapimciRaporApiModel
    {
        [JsonPropertyName("value")]
        public List<YapimciRaporModel> YapimciRaporList { get; set; }

        public List<YapimciModel> Yapimcilar { get; set; }

        public YapimciRaporFilterModel Filtre { get; set; }
    }
}
