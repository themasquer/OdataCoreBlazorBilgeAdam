using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorWasmUI.Models
{
    public class YorumModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Yorumcu Adı zorunludur!")]
        [StringLength(200, ErrorMessage = "Yorumcu Adı en çok 200 karakter girilebilir!")]
        [DisplayName("Yorumcu Adı")]
        public string YorumcuAdi { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur!")]
        [StringLength(500, ErrorMessage = "Açıklama en çok 500 karakter girilebilir!")]
        [DisplayName("Yorum Açıklaması")]
        public string Aciklamasi { get; set; }

        public DateTime Tarihi { get; set; }

        //[Required(ErrorMessage = "Yorum Tarihi zorunludur!")] // Bootstrap-Datepicker üzerinden set ettiğimiz için Blazor tarih girilmemiş olarak görüyor
        [DisplayName("Yorum Tarihi")]
        [JsonIgnore] // OData servisine JSON olarak bu özellik gönderilmesin
        public string TarihiText { get; set; }

        public int OyunId { get; set; }

        [Required(ErrorMessage = "Oyun zorunludur!")]
        [DisplayName("Oyun Adı")]
        [JsonIgnore]
        public string OyunIdText { get; set; }

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

        public int OyunYapimciId { get; set; }

        [DisplayName("Oyun Yapımcısı")]
        public string OyunYapimciAdi { get; set; }
    }
}
