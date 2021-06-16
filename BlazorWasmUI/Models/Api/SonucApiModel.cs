using System.Text.Json.Serialization;

namespace BlazorWasmUI.Models.Api
{
    public class SonucApiModel
    {
        [JsonPropertyName("error")]
        public HataModel Hata { get; set; }

        public bool BasariliMi { get; set; }

        public SonucApiModel()
        {
            
        }

        public SonucApiModel(bool basariliMi)
        {
            BasariliMi = basariliMi;
        }
    }
}
