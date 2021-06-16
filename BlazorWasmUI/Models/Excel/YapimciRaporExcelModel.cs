using System.ComponentModel;

namespace BlazorWasmUI.Models.Excel
{
    public class YapimciRaporExcelModel
    {
        [DisplayName("Oyun Yapımcısı")]
        public string YapimciAdi { get; set; }

        [DisplayName("Oyun Adı")]
        public string OyunAdi { get; set; }

        [DisplayName("Oyun Tarihi")]
        public string OyunTarihi { get; set; }

        [DisplayName("Oyun Puanı")]
        public string OyunPuani { get; set; }

        [DisplayName("Yorumcu Adı")]
        public string YorumcuAdi { get; set; }

        [DisplayName("Yorum Açıklaması")]
        public string YorumAciklamasi { get; set; }

        [DisplayName("Yorum Tarihi")]
        public string YorumTarihi { get; set; }
    }
}
