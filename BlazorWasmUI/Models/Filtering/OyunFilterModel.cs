using System.ComponentModel;

namespace BlazorWasmUI.Models.Filtering
{
    public class OyunFilterModel
    {
        [DisplayName("Oyun Adı")]
        public string Adi { get; set; }

        [DisplayName("Oyun Tarihi")]
        public string BaslangicTarihiText { get; set; }

        public string BitisTarihiText { get; set; }

        [DisplayName("Oyun Puanı")]
        public string BaslangicPuaniText { get; set; }

        public string BitisPuaniText { get; set; }

        [DisplayName("Yapımcı")]
        public string YapimciIdText { get; set; }
    }
}
