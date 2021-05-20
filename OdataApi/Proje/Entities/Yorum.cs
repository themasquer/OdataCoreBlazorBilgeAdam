using System;
using System.ComponentModel.DataAnnotations;

namespace OdataApi.Proje.Entities
{
    public class Yorum
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string YorumcuAdi { get; set; }

        [Required]
        [StringLength(500)] 
        public string Aciklamasi { get; set; }

        public DateTime Tarihi { get; set; }

        public int OyunId { get; set; }
        public Oyun Oyun { get; set; }
    }
}