using OdataApi.Proje.Contexts;
using OdataApi.Proje.Models;
using OdataApi.Proje.Services.Bases;
using System.Linq;

namespace OdataApi.Proje.Services
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
    }
}
