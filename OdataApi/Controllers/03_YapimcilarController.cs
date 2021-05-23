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



        //[HttpPost] // aksiyon adı aynı olduğu için yazılmasına gerek yoktur
        //public IActionResult Post([FromBody]YapimciModel model) // [FromBody] attribute'unun yazılmasına gerek yoktur
        public IActionResult Post(YapimciModel model)
        {
            if (ModelState.IsValid)
            {
                _yapimciService.Add(model);
                return Created(model);
            }
            return BadRequest(ModelState);
        }

        //[HttpPut] // aksiyon adı aynı olduğu için yazılmasına gerek yoktur
        //public IActionResult Put([FromODataUri]int key, [FromBody]YapimciModel model) // [FromODataUri] ve [FromBody] attribute'larının yazılmasına gerek yoktur
        public IActionResult Put(int key, YapimciModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = key;
                _yapimciService.Update(model);

                Request.Headers.Add("Prefer", "return=representation"); // aşağıdaki Updated() methodu ile modeli dönebilmek için eklenmesi gerekmektedir

                return Updated(model);
            }
            return BadRequest(ModelState);
        }

        //[HttpDelete] // aksiyon adı aynı olduğu için yazılmasına gerek yoktur
        //public IActionResult Delete([FromODataUri]int key) // [FromODataUri] attribute'unun yazılmasına gerek yoktur
        public IActionResult Delete(int key)
        {
            _yapimciService.Delete(key);
            return NoContent();
        }
    }
}
