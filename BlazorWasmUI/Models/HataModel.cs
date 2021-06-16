using System.Text.Json.Serialization;

namespace BlazorWasmUI.Models
{
    public class HataModel
    {
        [JsonPropertyName("code")]
        public string Kod { get; set; }

        [JsonPropertyName("message")]
        public string Mesaj { get; set; }
    }
}
