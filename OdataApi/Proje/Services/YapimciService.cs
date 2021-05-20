using OdataApi.Proje.Contexts;
using OdataApi.Proje.Entities;
using OdataApi.Proje.Models;
using OdataApi.Proje.Services.Bases;
using System.Linq;

namespace OdataApi.Proje.Services
{
    public class YapimciService : IYapimciService
    {
        private readonly DefaultContext _db;

        public YapimciService(DefaultContext db)
        {
            _db = db;
        }

        public IQueryable<YapimciModel> Query()
        {
            return _db.Set<Yapimci>().Select(y => new YapimciModel()
            {
                Id = y.Id,
                Adi = y.Adi
            });
        }
    }
}
