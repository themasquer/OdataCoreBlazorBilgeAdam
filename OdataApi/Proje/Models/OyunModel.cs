using System;
using System.ComponentModel.DataAnnotations;

namespace OdataApi.Proje.Models
{
    public class OyunModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Adi { get; set; }

        public DateTime Tarihi { get; set; }
        public double Puani { get; set; }

        public int YapimciId { get; set; }
        public YapimciModel Yapimci { get; set; }
    }
}
