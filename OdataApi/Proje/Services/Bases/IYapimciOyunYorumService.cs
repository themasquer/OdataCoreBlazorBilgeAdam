using OdataApi.Proje.Models;
using OdataApi.Proje.Models.Filtering;
using System.Collections.Generic;

namespace OdataApi.Proje.Services.Bases
{
    public interface IYapimciOyunYorumService
    {
        List<YapimciOyunYorumModel> GetYapimciOyunYorumlar(YapimciOyunYorumFilterModel filter = null);
        string GetYapimciOyunPuanOrtalamasi(int yapimciId);
    }
}
