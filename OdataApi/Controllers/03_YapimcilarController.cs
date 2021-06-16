using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using OdataApi.Models;
using OdataApi.Services.Bases;

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
        public IActionResult Get([FromODataUri]int key)
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



        //[HttpPost] // aksiyon adı aynı olduğu için yazılmasına gerek yoktur
        public IActionResult Post([FromBody]YapimciModel model)
        {
            if (ModelState.IsValid)
            {
                _yapimciService.Add(model);
                return Ok(model);
            }
            return BadRequest(ModelState);
        }

        //[HttpPut] // aksiyon adı aynı olduğu için yazılmasına gerek yoktur
        public IActionResult Put([FromODataUri]int key, [FromBody]YapimciModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = key;
                _yapimciService.Update(model);
                return Ok(model);
            }
            return BadRequest(ModelState);
        }

        //[HttpDelete] // aksiyon adı aynı olduğu için yazılmasına gerek yoktur
        public IActionResult Delete([FromODataUri]int key)
        {
            var yapimci = _yapimciService.Query().SingleOrDefault(y => y.Id == key);
            if (yapimci.Oyunlar?.Count > 0)
            {
                return BadRequest("Silinmek istenen yapımcıya ait en az bir oyun bulunduğundan yapımcı silinemez!");
            }
            _yapimciService.Delete(key);
            return Ok();
        }
    }
}
