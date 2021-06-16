using System.ComponentModel;

namespace BlazorWasmUI.Models.Filtering
{
    public class YapimciRaporFilterModel
    {
        [DisplayName("Yapımcı Adı")]
        public string YapimciId { get; set; }

        [DisplayName("Oyun Adı")]
        public string OyunAdi { get; set; }

        [DisplayName("Oyun Tarihi")]
        public string OyunBaslangicTarihi { get; set; }

        public string OyunBitisTarihi { get; set; }

        [DisplayName("Oyun Puanı")]
        public string OyunBaslangicPuani { get; set; }

        public string OyunBitisPuani { get; set; }

        [DisplayName("Yorumcu Adı")]
        public string YorumcuAdi { get; set; }
    }
}
