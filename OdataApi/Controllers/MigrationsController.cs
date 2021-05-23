using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdataApi.Contexts;
using OdataApi.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OdataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MigrationsController : ControllerBase
    {
        private readonly DefaultContext _db;

        public MigrationsController(DefaultContext db)
        {
            _db = db;
        }

        // /api/Migrations
        [HttpGet]
        public IActionResult Seed()
        {
            List<Yapimci> yapimcilar = new List<Yapimci>()
            {
                new Yapimci()
                {
                    Adi = "Rockstar Games"
                },
                new Yapimci()
                {
                    Adi = "Valve"
                },
                new Yapimci()
                {
                    Adi = "Electronic Arts"
                }
            };

            List<Oyun> oyunlar = new List<Oyun>()
            {
                new Oyun()
                {
                    Adi = "Red Dead Redemption 2",
                    Yapimci = yapimcilar[0],
                    Tarihi = DateTime.Parse("26.10.2018", new CultureInfo("tr")),
                    Puani = 93
                },
                new Oyun()
                {
                    Adi = "Grand Theft Auto 5",
                    Yapimci = yapimcilar[0],
                    Tarihi = DateTime.Parse("17.09.2013", new CultureInfo("tr")),
                    Puani = 96
                },
                new Oyun()
                {
                    Adi = "Half-Life 2",
                    Yapimci = yapimcilar[1],
                    Tarihi = DateTime.Parse("22.06.2004", new CultureInfo("tr")),
                    Puani = 96
                },
                new Oyun()
                {
                    Adi = "Left 4 Dead 2",
                    Yapimci = yapimcilar[1],
                    Tarihi = DateTime.Parse("17.11.2009", new CultureInfo("tr")),
                    Puani = 89
                },
                new Oyun()
                {
                    Adi = "Counter-Strike Global Offensive",
                    Yapimci = yapimcilar[1],
                    Tarihi = DateTime.Parse("21.08.2012", new CultureInfo("tr")),
                    Puani = 83
                },
                new Oyun()
                {
                    Adi = "Battlefield 1942",
                    Yapimci = yapimcilar[2],
                    Tarihi = DateTime.Parse("10.09.2002", new CultureInfo("tr")),
                    Puani = 89
                }
            };

            List<Yorum> yorumlar = new List<Yorum>()
            {
                new Yorum()
                {
                    YorumcuAdi = "Oyungezer",
                    Tarihi = DateTime.Parse("26.11.2018", new CultureInfo("tr")),
                    Aciklamasi = "Tam bir şaheser.",
                    Oyun = oyunlar[0]
                },
                new Yorum()
                {
                    YorumcuAdi = "Oyungezer",
                    Tarihi = DateTime.Parse("18.10.2013", new CultureInfo("tr")),
                    Aciklamasi = "Efsane devam ediyor.",
                    Oyun = oyunlar[1]
                },
                new Yorum()
                {
                    YorumcuAdi = "Oyungezer",
                    Tarihi = DateTime.Parse("28.06.2004", new CultureInfo("tr")),
                    Aciklamasi = "Valve bu işi çok iyi biliyor.",
                    Oyun = oyunlar[2]
                },
                new Yorum()
                {
                    YorumcuAdi = "Oyungezer",
                    Tarihi = DateTime.Parse("20.11.2009", new CultureInfo("tr")),
                    Aciklamasi = "En iyi zombi oyunu.",
                    Oyun = oyunlar[3]
                },
                new Yorum()
                {
                    YorumcuAdi = "Oyungezer",
                    Tarihi = DateTime.Parse("23.08.2012", new CultureInfo("tr")),
                    Aciklamasi = "Counter-Strike adına yakışan bir oyun.",
                    Oyun = oyunlar[4]
                },
                new Yorum()
                {
                    YorumcuAdi = "Oyungezer",
                    Tarihi = DateTime.Parse("10.10.2002", new CultureInfo("tr")),
                    Aciklamasi = "Muhteşem bir II. Dünya Savaşı oyunu.",
                    Oyun = oyunlar[5]
                },
                new Yorum()
                {
                    YorumcuAdi = "Level",
                    Tarihi = DateTime.Parse("28.10.2018", new CultureInfo("tr")),
                    Aciklamasi = "Bugüne kadar çıkmış en iyi oyun.",
                    Oyun = oyunlar[0]
                },
                new Yorum()
                {
                    YorumcuAdi = "Level",
                    Tarihi = DateTime.Parse("25.06.2004", new CultureInfo("tr")),
                    Aciklamasi = "Half-Life serisi başarıyla devam ediyor.",
                    Oyun = oyunlar[2]
                },
                new Yorum()
                {
                    YorumcuAdi = "Level",
                    Tarihi = DateTime.Parse("19.09.2002", new CultureInfo("tr")),
                    Aciklamasi = "Çok yenilikçi bir açık dünya savaş oyunu.",
                    Oyun = oyunlar[5]
                },
            };

            int idSeedValue = 0;
            int oyunlarCount = _db.Oyunlar.Count();
            if (oyunlarCount == 0)
                idSeedValue = 1;

            _db.Database.ExecuteSqlRaw("dbcc CHECKIDENT('Oyunlar', RESEED, " + idSeedValue + ")");
            _db.Database.ExecuteSqlRaw("dbcc CHECKIDENT('Yapimcilar', RESEED, " + idSeedValue + ")");
            _db.Database.ExecuteSqlRaw("dbcc CHECKIDENT('Yorumlar', RESEED, " + idSeedValue + ")");

            List<Yapimci> dbYapimcilar = _db.Yapimcilar.ToList();
            _db.Yapimcilar.RemoveRange(dbYapimcilar);
            _db.SaveChanges();

            List<Oyun> dbOyunlar = _db.Oyunlar.ToList();
            _db.Oyunlar.RemoveRange(dbOyunlar);
            _db.SaveChanges();

            List<Yorum> dbYorumlar = _db.Yorumlar.ToList();
            _db.Yorumlar.RemoveRange(dbYorumlar);
            _db.SaveChanges();

            _db.Yapimcilar.AddRange(yapimcilar);
            _db.SaveChanges();
            _db.Oyunlar.AddRange(oyunlar);
            _db.SaveChanges();
            _db.Yorumlar.AddRange(yorumlar);
            _db.SaveChanges();
            return Ok("İlk veriler başarıyla olşuturuldu!");
        }
    }
}
