using OdataApi.Proje.Contexts;
using OdataApi.Proje.Entities;
using OdataApi.Proje.Models;
using OdataApi.Proje.Services.Bases;
using System.Linq;

namespace OdataApi.Proje.Services
{
    public class OyunService : IOyunService
    {
        private readonly DefaultContext _db;

        public OyunService(DefaultContext db)
        {
            _db = db;
        }

        public IQueryable<OyunModel> Query()
        {
            return _db.Set<Oyun>().Select(o => new OyunModel()
            {
                Id = o.Id,
                Adi = o.Adi,
                Puani = o.Puani,
                Tarihi = o.Tarihi,
                YapimciId = o.YapimciId,
                Yapimci = new YapimciModel()
                {
                    Id = o.Yapimci.Id,
                    Adi = o.Yapimci.Adi
                }
            });
        }
    }
}
