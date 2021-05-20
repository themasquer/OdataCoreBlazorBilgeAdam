using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdataApi.Proje.Entities
{
    public class Oyun
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Adi { get; set; }

        public DateTime Tarihi { get; set; }

        public double Puani { get; set; }

        public int YapimciId { get; set; }
        public Yapimci Yapimci { get; set; }

        public List<Yorum> Yorumlar { get; set; }
    }
}