﻿using OdataApi.Proje.Contexts;
using OdataApi.Proje.Entities;
using OdataApi.Proje.Models;
using OdataApi.Proje.Models.Filtering;
using OdataApi.Proje.Services.Bases;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OdataApi.Proje.Services
{
    public class YapimciOyunYorumService : IYapimciOyunYorumService
    {
        private readonly DefaultContext _db;

        public YapimciOyunYorumService(DefaultContext db)
        {
            _db = db;
        }

        public List<YapimciOyunYorumModel> GetYapimciOyunYorumlar(YapimciOyunYorumFilterModel filter = null)
        {
            var yapimciQuery = _db.Set<Yapimci>().AsQueryable();
            var oyunQuery = _db.Set<Oyun>().AsQueryable();
            var yorumQuery = _db.Set<Yorum>().AsQueryable();
            var query = from yapimci in yapimciQuery
                        join oyun in oyunQuery
                            on yapimci.Id equals oyun.YapimciId
                        join yorum in yorumQuery
                            on oyun.Id equals yorum.OyunId
                        orderby yapimci.Adi, oyun.Adi, yorum.YorumcuAdi
                        select new YapimciOyunYorumModel()
                        {
                            Id = yapimci.Id,
                            YapimciAdi = yapimci.Adi,
                            OyunAdi = oyun.Adi,
                            OyunPuani = oyun.Puani,
                            OyunTarihi = oyun.Tarihi,
                            YorumcuAdi = yorum.YorumcuAdi,
                            YorumAciklamasi = yorum.Aciklamasi,
                            YorumTarihi = yorum.Tarihi
                        };
            if (filter != null)
            {
                if (filter.YapimciId.HasValue)
                    query = query.Where(q => q.Id == filter.YapimciId);
                if (!string.IsNullOrWhiteSpace(filter.YapimciAdi))
                    query = query.Where(y => y.YapimciAdi.ToLower().Contains(filter.YapimciAdi.ToLower().Trim()));
                if (filter.OyunBaslangicTarihi.HasValue)
                    query = query.Where(q => q.OyunTarihi >= filter.OyunBaslangicTarihi.Value);
                if (filter.OyunBitisTarihi.HasValue)
                    query = query.Where(q => q.OyunTarihi <= filter.OyunBitisTarihi.Value);
                if (filter.OyunBaslangicPuani.HasValue)
                    query = query.Where(q => q.OyunPuani >= filter.OyunBaslangicPuani.Value);
                if (filter.OyunBitisPuani.HasValue)
                    query = query.Where(q => q.OyunPuani <= filter.OyunBitisPuani.Value);
                if (!string.IsNullOrWhiteSpace(filter.YorumcuAdi))
                    query = query.Where(q => q.YorumcuAdi.ToLower().Contains(filter.YorumcuAdi.ToLower().Trim()));
            }
            return query.ToList();
        }

        public string GetYapimciOyunPuanOrtalamasi(int yapimciId)
        {
            var yapimci = _db.Set<Yapimci>().Include(y => y.Oyunlar).SingleOrDefault(y => y.Id == yapimciId);
            var oyunPuanOrtalamasi = yapimci.Oyunlar.Average(o => o.Puani);
            return $"Yapımcı ID: {yapimci.Id}, Yapımcı Adı: {yapimci.Adi}, Oyun Puan Ortalaması: {oyunPuanOrtalamasi}";
        }
    }
}
