using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdataApi.Entities
{
    public class Yapimci
    {
        public int Id { get; set; }

        [Required]
        [StringLength(400)]
        public string Adi { get; set; }

        public List<Oyun> Oyunlar { get; set; }
    }
}