using System.Text.Json.Serialization;

namespace BlazorWasmUI.Models.Api
{
    public class HakkindaApiModel
    {
        [JsonPropertyName("value")]
        public string Hazirlayan { get; set; }
    }
}
