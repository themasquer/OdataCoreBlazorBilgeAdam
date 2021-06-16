using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BlazorWasmUI.Models
{
    public class YapimciRaporModel
    {
        public int Id { get; set; } // Yapimci.Id

        [DisplayName("Oyun Yapımcısı")]
        public string YapimciAdi { get; set; }

        [DisplayName("Oyun Adı")]
        public string OyunAdi { get; set; }

        public DateTime OyunTarihi { get; set; }

        [DisplayName("Oyun Tarihi")]
        [JsonIgnore]
        public string OyunTarihiText { get; set; }

        public double OyunPuani { get; set; }

        [DisplayName("Oyun Puanı")]
        [JsonIgnore]
        public string OyunPuaniText { get; set; }

        [DisplayName("Yorumcu Adı")]
        public string YorumcuAdi { get; set; }

        [DisplayName("Yorum Açıklaması")]
        public string YorumAciklamasi { get; set; }

        public DateTime YorumTarihi { get; set; }

        [DisplayName("Yorum Tarihi")]
        [JsonIgnore]
        public string YorumTarihiText { get; set; }
    }
}
