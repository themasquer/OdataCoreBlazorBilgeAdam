using System.Collections.Generic;
using OdataApi.Models;
using OdataApi.Models.Filtering;

namespace OdataApi.Services.Bases
{
    public interface IYapimciOyunYorumService
    {
        List<YapimciOyunYorumModel> GetYapimciOyunYorumlar(YapimciOyunYorumFilterModel filter = null);
        string GetYapimciOyunPuanOrtalamasi(int yapimciId);
    }
}
