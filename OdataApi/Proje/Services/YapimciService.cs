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
                Adi = y.Adi,
                Oyunlar = y.Oyunlar.Select(o => new OyunModel()
                {
                    Id = o.Id,
                    Adi = o.Adi,
                    Puani = o.Puani,
                    Tarihi = o.Tarihi,
                    YapimciId = o.YapimciId
                }).ToList()
            });
        }

        public void Add(YapimciModel model)
        {
            var entity = new Yapimci()
            {
                Adi = model.Adi.Trim()
            };
            _db.Set<Yapimci>().Add(entity);
            _db.SaveChanges();
            model.Id = entity.Id;
        }

        public void Update(YapimciModel model)
        {
            var entity = _db.Set<Yapimci>().Find(model.Id);
            entity.Adi = model.Adi.Trim();
            _db.Set<Yapimci>().Update(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _db.Set<Yapimci>().Find(id);
            _db.Set<Yapimci>().Remove(entity);
            _db.SaveChanges();
        }
    }
}
