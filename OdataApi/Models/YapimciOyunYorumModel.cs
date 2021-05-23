using System;

namespace OdataApi.Models
{
    public class YapimciOyunYorumModel
    {
        public int Id { get; set; } // Yapimci.Id
        public string YapimciAdi { get; set; }
        public string OyunAdi { get; set; }
        public DateTime OyunTarihi { get; set; }
        public double OyunPuani { get; set; }
        public string YorumcuAdi { get; set; }
        public string YorumAciklamasi { get; set; }
        public DateTime YorumTarihi { get; set; }
    }
}
