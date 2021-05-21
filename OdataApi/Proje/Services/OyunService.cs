﻿using OdataApi.Proje.Contexts;
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

        public void Add(OyunModel model)
        {
            var entity = new Oyun()
            {
                Adi = model.Adi.Trim(),
                Puani = model.Puani,
                Tarihi = model.Tarihi,
                YapimciId = model.YapimciId
            };
            _db.Set<Oyun>().Add(entity);
            _db.SaveChanges();
            model.Id = entity.Id;
        }

        public void Update(OyunModel model)
        {
            var entity = _db.Set<Oyun>().Find(model.Id);
            entity.Adi = model.Adi.Trim();
            entity.Puani = model.Puani;
            entity.Tarihi = model.Tarihi;
            entity.YapimciId = model.YapimciId;
            _db.Set<Oyun>().Update(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _db.Set<Oyun>().Find(id);
            _db.Set<Oyun>().Remove(entity);
            _db.SaveChanges();
        }
    }
}
