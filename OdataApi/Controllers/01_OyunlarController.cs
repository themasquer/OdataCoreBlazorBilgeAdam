using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OdataApi.Models;
using OdataApi.Services.Bases;

namespace OdataApi.Controllers
{
    // OData (Open Data Protocol) veri kaynaklarını REST url üzerinden sorgulama protokolüdür.
    // Microsoft tarafından 2007 yılından itibaren geliştirilmeye başlanmıştır.
    // Api servislerinde kullanılır.

    // Avantajı servis üzerindeki sorguların client tarafına bırakılması, böylece endpoint tarafında daha az iş yüküdür.

    // Kullanım için öncelikle NuGet'ten Microsoft.AspNetCore.OData paketi yüklenir.

    // Daha sonra /Proje/Configs/OdataConfig.cs'te olduğu gibi konfigürasyon entity set'ler ile (tip olarak projede kullanacağımız modeller) controller adları aynı olacak şekilde yapılır
    // ve Startup.cs'te Configure() methodunda çağrılır.

    // Startup.cs'te ConfigureServices() methoduna da services.AddOData(); eklenir.

    // OData projede kullanacağımız modellere [EntitySetName]Controller olarak erişim sağlamaktadır.

    // Daha sonrasında ODataController'dan controller class'ları türetilir.

    // OData ile Swagger'ın birlikte kullanılması mümkün değildir.

    // OData get servislerini hızlıca test edebilmek için ~/Properties/launchSettings.json dosyasında "IIS Express" ve "OdataApi" profillerine "launchUrl": "index.html" ekliyoruz,
    // kullanabilmek için de Startup.cs Configure() methoduna app.UseStaticFiles(); satırını, projemize de wwwroot klasörünü ekleyip içindeki index.html altında test link'lerini yazıyoruz.

    // Bir OData controller'ının get aksiyonlarında dönüş tipinin IQueryable olması daha performanslı sorgulamalar yapılmasını sağlar.

    // OData controller'larında action isimlendirmeleri get işlemleri için Get ile başlamalıdır. Post, put ve delete gibi action'lar WebApi'de olduğu gibi Post, Put ve Delete ile başlayacak şekilde isimlendirilebilir.

    // Bir get aksiyonuna sorgulanabilirlik eklenmesi için aksiyon üzerine EnableQuery attribute'u tanımlanmalıdır.

    // OData'nın kullandığı entity data model'ler hakkında bilgi alabilmek için /odata/$metadata yazılabilir.

    // OData ile sorgulama:
    // OData Query Options üzerinden sunucudan dönecek veri üzerinde filtreleme, sayfalama ve sıralama işlemleri yapılabilir.
    // Controller adından sonra kullanılır, query string üzerinden sorgulama yapılacağından ? ve & sonrasında mutlaka $ işareti kullanılmalıdır.
    // Dönen sorgularda iki sonuç döner, 1.'si kaynak metadata bilgileri, 2.'si ise veri. Tek bir değer istense de mutlaka sonuç array olarak döner.

    // Sorgu örnekleri ~/wwwroot/index.html sayfasında bulunabilir.

    // OData sorguları üzerinden büyük küçük harf duyarsız sorgulama yapılabilir.

    // Endpoint'lerde izin kontrol işlemleri için EnableQuery attribute'u özelleştirilebilir. Entity Set düzeyinde de $select, $filter vb. izin kontrol işlemleri yapılabilir.

    // OData'nın hem öğrenmesi hem de uygulaması piyasadaki rakibi GraphQL'e göre çok daha kolaydır.

    public class OyunlarController : ODataController
    {
        private readonly IOyunService _oyunService;

        public OyunlarController(IOyunService oyunService)
        {
            _oyunService = oyunService;
        }

        //[HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _oyunService.Query();
            return Ok(query);
        }

        //[HttpPost] // aksiyon adı aynı olduğu için yazılmasına gerek yoktur
        //public IActionResult Post([FromBody]OyunModel model) // [FromBody] attribute'unun yazılmasına gerek yoktur
        public IActionResult Post(OyunModel model)
        {
            if (ModelState.IsValid)
            {
                _oyunService.Add(model);
                return Created(model);
            }
            return BadRequest(ModelState);
        }

        //[HttpPut] // aksiyon adı aynı olduğu için yazılmasına gerek yoktur
        //public IActionResult Put([FromODataUri]int key, [FromBody]OyunModel model) // [FromODataUri] ve [FromBody] attribute'larının yazılmasına gerek yoktur
        public IActionResult Put(int key, OyunModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = key;
                _oyunService.Update(model);

                Request.Headers.Add("Prefer", "return=representation"); // aşağıdaki Updated() methodu ile modeli dönebilmek için eklenmesi gerekmektedir

                return Updated(model);
            }
            return BadRequest(ModelState);
        }

        //[HttpDelete] // aksiyon adı aynı olduğu için yazılmasına gerek yoktur
        //public IActionResult Delete([FromODataUri]int key) // [FromODataUri] attribute'unun yazılmasına gerek yoktur
        public IActionResult Delete(int key)
        {
            _oyunService.Delete(key);
            return NoContent();
        }
    }
}
