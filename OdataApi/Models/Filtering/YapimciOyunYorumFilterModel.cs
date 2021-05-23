using System;

namespace OdataApi.Models.Filtering
{
    public class YapimciOyunYorumFilterModel
    {
        public int? YapimciId { get; set; }
        public string YapimciAdi { get; set; }
        public string OyunAdi { get; set; }
        public DateTime? OyunBaslangicTarihi { get; set; }
        public DateTime? OyunBitisTarihi { get; set; }
        public double? OyunBaslangicPuani { get; set; }
        public double? OyunBitisPuani { get; set; }
        public string YorumcuAdi { get; set; }
    }
}
