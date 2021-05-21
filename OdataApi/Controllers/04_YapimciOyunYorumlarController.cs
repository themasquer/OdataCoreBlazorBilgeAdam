using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OdataApi.Proje.Models.Filtering;
using OdataApi.Proje.Services.Bases;
using System;
using System.Linq;

namespace OdataApi.Controllers
{
    public class YapimciOyunYorumlarController : ODataController
    {
        private readonly IYapimciOyunYorumService _yapimciOyunYorumService;

        public YapimciOyunYorumlarController(IYapimciOyunYorumService yapimciOyunYorumService)
        {
            _yapimciOyunYorumService = yapimciOyunYorumService;
        }



        #region FUNCTION'LAR
        [HttpGet]
        public IActionResult OyunTarihineGoreGetir(DateTime? oyunBaslangicTarihi, DateTime? oyunBitisTarihi)
        {
            var filter = new YapimciOyunYorumFilterModel()
            {
                OyunBaslangicTarihi = oyunBaslangicTarihi,
                OyunBitisTarihi = oyunBitisTarihi
            };
            var list = _yapimciOyunYorumService.GetYapimciOyunYorumlar(filter);
            return Ok(list);
        }
        #endregion



        #region ACTION'LAR
        [HttpPost]
        //public IActionResult YapimciOyunAdiPuaniniGetir([FromODataUri] int key)
        public IActionResult YapimciOyunAdiPuaniniGetir(int key) // [FromODataUri] attribute'u kullanılmadan da yazılabilir
        {
            var filter = new YapimciOyunYorumFilterModel()
            {
                YapimciId = key
            };
            var result = _yapimciOyunYorumService.GetYapimciOyunYorumlar(filter).FirstOrDefault();
            if (result == null)
                return NotFound();
            return Ok($"Yapımcı: {result.YapimciAdi}, Oyun: {result.OyunAdi}, Puan: {result.OyunPuani}");
        }

        [HttpPost]
        public IActionResult KayitSayisiniGetir()
        {
            var list = _yapimciOyunYorumService.GetYapimciOyunYorumlar();
            return Ok(list.Count);
        }

        [HttpPost]
        public IActionResult YapimciOyunAdlariniGetir()
        {
            var list = _yapimciOyunYorumService.GetYapimciOyunYorumlar();
            var result = string.Join(", ", list.Select(l => l.YapimciAdi + " - " + l.OyunAdi).Distinct());
            return Ok(result);
        }

        [HttpPost]
        public IActionResult YorumcuAdinaGoreGetir(string yorumcuAdi)
        {
            var filter = new YapimciOyunYorumFilterModel()
            {
                YorumcuAdi = yorumcuAdi
            };
            var list = _yapimciOyunYorumService.GetYapimciOyunYorumlar(filter);
            return Ok(list);
        }

        [HttpPost]
        public IActionResult YapimciAdiOyunPuaninaGoreGetir(string yapimciAdi, double? oyunBaslangicPuani, double? oyunBitisPuani)
        {
            var filter = new YapimciOyunYorumFilterModel()
            {
                YapimciAdi = yapimciAdi,
                OyunBaslangicPuani = oyunBaslangicPuani,
                OyunBitisPuani = oyunBitisPuani
            };
            var list = _yapimciOyunYorumService.GetYapimciOyunYorumlar(filter);
            return Ok(list);
        }

        [HttpPost]
        public IActionResult YapimciOyunPuanOrtalmasiniGetir(int yapimciId)
        {
            var result = _yapimciOyunYorumService.GetYapimciOyunPuanOrtalamasi(yapimciId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult TumYapimciOyunYorumlariniGetir(YapimciOyunYorumFilterModel filter)
        {
            var list = _yapimciOyunYorumService.GetYapimciOyunYorumlar(filter);
            return Ok(list);
        }
        #endregion
    }
}
