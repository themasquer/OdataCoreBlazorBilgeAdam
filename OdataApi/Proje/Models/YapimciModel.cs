using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdataApi.Proje.Models
{
    public class YapimciModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(400)]
        public string Adi { get; set; }

        public List<OyunModel> Oyunlar { get; set; }
    }
}
