using BlazorWasmUI.Models.Filtering;
using BlazorWasmUI.Models.Ordering;
using BlazorWasmUI.Models.Paging;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlazorWasmUI.Models.Api
{
    public class OyunApiModel
    {
        [JsonPropertyName("@odata.count")] 
        public int Count { get; set; }

        [JsonPropertyName("value")] 
        public List<OyunModel> Oyunlar { get; set; }

        public OyunModel Oyun { get; set; }

        public List<YapimciModel> Yapimcilar { get; set; }

        public OyunFilterModel Filtre { get; set; }

        public PageModel Sayfa { get; set; }

        public OrderModel Sira { get; set; }
    }
}
