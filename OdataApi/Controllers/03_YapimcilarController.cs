using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OdataApi.Proje.Services.Bases;
using System.Linq;

namespace OdataApi.Controllers
{
    public class YapimcilarController : ODataController
    {
        private readonly IYapimciService _yapimciService;

        public YapimcilarController(IYapimciService yapimciService)
        {
            _yapimciService = yapimciService;
        }



        #region PAGINATION
        [EnableQuery(PageSize = 2)] // sorgu sonucundan 2 kaydı getir
        public IActionResult Get()
        {
            var query = _yapimciService.Query();
            return Ok(query);
        }
        #endregion



        #region ANAHTAR (ID) ÜZERİNDEN KAYIT ÇEKME
        [EnableQuery]
        //public IActionResult Get([FromODataUri] int key)
        public IActionResult Get(int key) // [FromODataUri] attribute'u kullanılmadan da yazılabilir
        {
            var query = _yapimciService.Query().Where(y => y.Id == key);
            return Ok(query);
        }
        #endregion



        #region FUNCTION'LAR
        [HttpGet]
        public IActionResult TumYapimcilar()
        {
            var list = _yapimciService.Query().OrderBy(y => y.Adi).ToList();
            return Ok(list);
        }

        [HttpGet]
        public IActionResult TumYapimciSayisi()
        {
            var count = _yapimciService.Query().Count();
            return Ok(count);
        }

        [HttpGet]
        public IActionResult YapimciAdinaGoreGetir(string yapimciAdi)
        {
            var list = _yapimciService.Query().Where(y => y.Adi.ToLower().Contains(yapimciAdi.ToLower().Trim()));
            return Ok(list);
        }
        #endregion
    }
}
