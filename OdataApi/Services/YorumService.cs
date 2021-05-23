using OdataApi.Contexts;
using OdataApi.Entities;
using OdataApi.Models;
using OdataApi.Services.Bases;
using System.Linq;

namespace OdataApi.Services
{
    public class YorumService : IYorumService
    {
        private readonly DefaultContext _db;

        public YorumService(DefaultContext db)
        {
            _db = db;
        }

        public IQueryable<YorumModel> Query()
        {
            return _db.Yorumlar.Select(y => new YorumModel()
            {
                Id = y.Id,
                Tarihi = y.Tarihi,
                Aciklamasi = y.Aciklamasi,
                YorumcuAdi = y.YorumcuAdi,
                OyunId = y.OyunId,
                OyunAdi = y.Oyun.Adi,
                OyunPuani = y.Oyun.Puani,
                OyunTarihi = y.Oyun.Tarihi,
                OyunYapimciId = y.Oyun.YapimciId,
                OyunYapimciAdi = y.Oyun.Yapimci.Adi
            });
        }

        public void Add(YorumModel model)
        {
            var entity = new Yorum()
            {
                YorumcuAdi = model.YorumcuAdi.Trim(),
                Aciklamasi = model.Aciklamasi.Trim(),
                Tarihi = model.Tarihi,
                OyunId = model.OyunId
            };
            _db.Set<Yorum>().Add(entity);
            _db.SaveChanges();
            model.Id = entity.Id;
        }

        public void Update(YorumModel model)
        {
            var entity = _db.Set<Yorum>().Find(model.Id);
            entity.YorumcuAdi = model.YorumcuAdi.Trim();
            entity.Aciklamasi = model.Aciklamasi.Trim();
            entity.Tarihi = model.Tarihi;
            entity.OyunId = model.OyunId;
            _db.Set<Yorum>().Update(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _db.Set<Yorum>().Find(id);
            _db.Set<Yorum>().Remove(entity);
            _db.SaveChanges();
        }
    }
}
