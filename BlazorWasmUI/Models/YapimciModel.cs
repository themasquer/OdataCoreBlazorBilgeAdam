using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorWasmUI.Models
{
    public class YapimciModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Yapımcı Adı zorunludur!")]
        [StringLength(400, ErrorMessage = "Yapımcı Adı en çok 400 karakter girilebilir!")]
        [DisplayName("Yapımcı Adı")]
        public string Adi { get; set; }

        [JsonIgnore] 
        public bool SilindiMi { get; set; } = false;
    }
}
