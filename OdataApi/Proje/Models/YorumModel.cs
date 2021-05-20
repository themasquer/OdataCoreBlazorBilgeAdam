using System;
using System.ComponentModel.DataAnnotations;

namespace OdataApi.Proje.Models
{
    public class YorumModel
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
        public string OyunAdi { get; set; }
        public DateTime OyunTarihi { get; set; }
        public double OyunPuani { get; set; }

        public int OyunYapimciId { get; set; }
        public string OyunYapimciAdi { get; set; }
    }
}
