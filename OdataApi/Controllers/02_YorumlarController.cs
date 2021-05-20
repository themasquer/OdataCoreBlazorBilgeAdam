using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using OdataApi.Proje.Models;
using OdataApi.Proje.Services.Bases;
using System.Linq;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace OdataApi.Controllers
{
    [ODataRoutePrefix("yorumlar")] // *2
    public class YorumlarController : ODataController
    {
        private readonly IYorumService _yorumService;

        public YorumlarController(IYorumService yorumService)
        {
            _yorumService = yorumService;
        }



        #region QUERY OPTIONS
        // EnableQuery attribute'u kullanılmadan ODataQueryOptions tipindeki bir parametre üzerinden sorgu parametreleri alınabilir:
        public IActionResult Get(ODataQueryOptions<YorumModel> options)
        {
            var settings = new ODataValidationSettings()
            {
                // Özelleştirme örnek:
                //AllowedQueryOptions = AllowedQueryOptions.All, // default
                //AllowedFunctions = AllowedFunctions.All, // default
                //AllowedArithmeticOperators = AllowedArithmeticOperators.All, // default
                //AllowedLogicalOperators = AllowedLogicalOperators.All, // default
                //AllowedOrderByProperties = { "Tarihi", "YorumcuAdi" }, // default'u boş kolleksiyon: herhangi bir özelliğe göre sıralamaya izin verir,
                //MaxAnyAllExpressionDepth = 1, // default, sorgudaki iç içe özelliklerde tüm veya herhangi bir özellik için maksimum derinlik
                //MaxNodeCount = 100, // default, $filter ile yapılan sorgulardaki maksimum özellik sayısı
                //MaxOrderByNodeCount = 9, // $orderby ile yapılan sorgulardaki maksimum özellik sayısı
                //MaxSkip = 20, // sorguda gönderilen maksimum $skip değeri
                //MaxTop = 10, // sorguda gönderilen maksimum $top değeri
                //MaxExpansionDepth = 1 // $expand ile yapılan sorgulardaki maksimum derinlik, kapamak için 0 verilir
            };

            options.Validate(settings);

            var query = _yorumService.Query();

            var queryResult = options.ApplyTo(query);

            //var testList = (queryResult as IQueryable<YorumModel>).ToList(); // hata testi için geçici olarak sorgu list'e dönüştürülebilir

            return Ok(queryResult as IQueryable<YorumModel>);
        }
        #endregion



        #region AKSİYONLARDA PARAMETRE KULLANIMI VE CUSTOM ROUTE
        [EnableQuery]
        //[ODataRoute("yorumlar({yorumId})")] // *1 : Her aksiyonda ODataRoute içerisine "yorumlar" yazmak yerine controller class'ının başına ODataRoutePrefix tanımlayarak (*2) bir aşağıdaki satırdaki gibi ODataRoute tanımlayıp (*2) tekrardan kurtulabiliriz
        [ODataRoute("({yorumId})")] // *2
        //public IActionResult Get([FromODataUri] int yorumId) // custom route ile parametre kullanımı: aksiyon ismi olarak mutlaka get kullanılmalıdır
        public IActionResult Get(int yorumId) // [FromODataUri] attribute'u kullanılmadan da yazılabilir
        {
            var query = _yorumService.Query().Where(y => y.Id == yorumId); // Geriye her zaman IQueryable döndüğümüz için where ile filtreliyoruz ve sonuç olarak bir kolleksiyon dönüyor 
            return Ok(query);
        }
        #endregion
    }
}
