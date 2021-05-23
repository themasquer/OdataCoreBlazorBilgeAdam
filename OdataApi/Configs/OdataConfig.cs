using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using OdataApi.Models;
using OdataApi.Models.Filtering;

namespace OdataApi.Configs
{
    public class OdataConfig
    {
        private readonly IApplicationBuilder _app;

        public OdataConfig(IApplicationBuilder app)
        {
            _app = app;
        }

        public void Configure()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<OyunModel>("Oyunlar");
            builder.EntitySet<YapimciModel>("Yapimcilar");
            builder.EntitySet<YorumModel>("Yorumlar");

            // Aşağıda function veya action oluşturabilmek için entity set tanımlanmalı ve bu entity set adında bir controller oluşturulmalıdır. // *1 
            builder.EntitySet<YapimciOyunYorumModel>("YapimciOyunYorumlar");



            #region FUNCTION VE ACTION'LAR
            // Eğer controller action'ına parametre olarak key gönderiliyorsa function veya action oluşturulurken Collection yazılmaz. // *2
            //builder.EntityType<YapimciModel>().Collection.Function("TumYapimcilar").ReturnsCollection<YapimciModel>(); // ReturnsCollection<Model>() veya ReturnsCollectionFromEntitySet<Model>("EntitySetName") yerine Returns<List<Model>>() kullanılabilir
            builder.EntityType<YapimciModel>().Collection.Function("TumYapimcilar").Returns<List<YapimciModel>>();

            builder.EntityType<YapimciModel>().Collection.Function("TumYapimciSayisi").Returns<int>();

            var yapimciAdinaGoreGetirFunction = builder.EntityType<YapimciModel>().Collection.Function("YapimciAdinaGoreGetir").Returns<List<YapimciModel>>();
            yapimciAdinaGoreGetirFunction.Parameter<string>("yapimciAdi");

            var oyunTarihineGoreGetirFunction = builder.EntityType<YapimciOyunYorumModel>().Collection.Function("OyunTarihineGoreGetir").Returns<List<YapimciOyunYorumModel>>(); // *1
            oyunTarihineGoreGetirFunction.Parameter<DateTime?>("oyunBaslangicTarihi");
            oyunTarihineGoreGetirFunction.Parameter<DateTime?>("oyunBitisTarihi");

            builder.EntityType<YapimciOyunYorumModel>().Action("YapimciOyunAdiPuaniniGetir").Returns<string>(); // *1, *2

            builder.EntityType<YapimciOyunYorumModel>().Collection.Action("KayitSayisiniGetir").Returns<int>(); // *1

            builder.EntityType<YapimciOyunYorumModel>().Collection.Action("YapimciOyunAdlariniGetir").Returns<string>(); // *1

            var yapimciOyunPuanOrtalmasiniGetirAction = builder.EntityType<YapimciOyunYorumModel>().Collection.Action("YapimciOyunPuanOrtalmasiniGetir").Returns<string>(); // *1
            yapimciOyunPuanOrtalmasiniGetirAction.Parameter<int>("yapimciId").Required(); // parametrenin zorunlu olduğunu belirtir

            //var yapimciAdinaGoreGetirAction = builder.EntityType<YapimciOyunYorumModel>().Collection.Action("YorumcuAdinaGoreGetir").ReturnsCollectionFromEntitySet<YapimciOyunYorumModel>("YapimciOyunYorumlar"); // *1
            var yapimciAdinaGoreGetirAction = builder.EntityType<YapimciOyunYorumModel>().Collection.Action("YorumcuAdinaGoreGetir").Returns<List<YapimciOyunYorumModel>>(); // *1
            yapimciAdinaGoreGetirAction.Parameter<string>("yorumcuAdi");

            var oyunTarihineGoreGetirAction = builder.EntityType<YapimciOyunYorumModel>().Collection.Action("YapimciAdiOyunPuaninaGoreGetir").Returns<List<YapimciOyunYorumModel>>(); // *1
            oyunTarihineGoreGetirAction.Parameter<string>("yapimciAdi");
            oyunTarihineGoreGetirAction.Parameter<double?>("oyunBaslangicPuani");
            oyunTarihineGoreGetirAction.Parameter<double?>("oyunBitisPuani");

            var tumYapimciOyunYorumlariGetirAction = builder.EntityType<YapimciOyunYorumModel>().Collection.Action("TumYapimciOyunYorumlariniGetir").Returns<List<YapimciOyunYorumModel>>(); // *1
            tumYapimciOyunYorumlariGetirAction.Parameter<YapimciOyunYorumFilterModel>("filter");

            builder.Function("HazirlayaniGetir").Returns<string>();
            #endregion



            _app.UseEndpoints(endpoints =>
            {
                endpoints.MapODataRoute("odata", "odata", builder.GetEdmModel());
                // 1. "odata": route name, 2. "odata": ~/odata/EntitySetName.
                // Edm: Entity Data Model, modelin property'lerine ve navigasyon property'lerine erişimi sağlar.

                endpoints.Select();
                endpoints.Expand();
                endpoints.OrderBy();

                //endpoints.MaxTop(100);
                endpoints.MaxTop(null); // sınırlama olmadan top kullanımı

                endpoints.Count();
                endpoints.Filter();

                endpoints.MapControllers();
            });
        }
    }
}
