using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace OdataApi.Controllers
{
    public class HazirlayanController : ODataController
    {



        #region ONBOUND FUNCTION / ACTION
        [HttpGet]
        [ODataRoute("HazirlayaniGetir")]
        public IActionResult HazirlayaniGetir()
        {
            return Ok("Çağıl Alsaç");
        }
        #endregion



    }
}
