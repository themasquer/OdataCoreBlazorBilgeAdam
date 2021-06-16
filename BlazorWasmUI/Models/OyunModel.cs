using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorWasmUI.Models
{
    public class OyunModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Oyun Adı zorunludur!")]
        [StringLength(300, ErrorMessage = "Oyun Adı en çok 300 karakter girilebilir!")]
        [DisplayName("Oyun Adı")]
        public string Adi { get; set; }

        public DateTime Tarihi { get; set; }

        //[Required(ErrorMessage = "Oyun Tarihi zorunludur!")] // Bootstrap-Datepicker üzerinden set ettiğimiz için Blazor tarih girilmemiş olarak görüyor
        [DisplayName("Oyun Tarihi")]
        [JsonIgnore] // OData servisine JSON olarak bu özellik gönderilmesin
        public string TarihiText { get; set; }

        public double Puani { get; set; }

        [Required(ErrorMessage = "Oyun Puanı zorunludur!")]
        [DisplayName("Oyun Puanı")]
        [JsonIgnore]
        public string PuaniText { get; set; }

        public int YapimciId { get; set; }

        [Required(ErrorMessage = "Yapımcı zorunludur!")]
        [DisplayName("Yapımcı")]
        [JsonIgnore]
        public string YapimciIdText { get; set; }

        [DisplayName("Yapımcı")]
        public YapimciModel Yapimci { get; set; }
    }
}
