using System.Collections.Generic;
using BlazorWasmUI.Models;
using BlazorWasmUI.Models.Api;
using System.Threading.Tasks;
using BlazorWasmUI.Models.Filtering;

namespace BlazorWasmUI.Services.Bases
{
    public interface IYapimciService
    {
        Task<YapimciApiModel> GetYapimcilarAsync();
        Task<YapimciApiModel> GetYapimciAsync(int id);
        Task<YapimciModel> AddYapimciAsync(YapimciModel yapimci);
        Task<YapimciModel> UpdateYapimciAsync(YapimciModel yapimci);
        Task<SonucApiModel> DeleteYapimciAsync(int id);
        Task<YapimciRaporApiModel> GetYapimciRapor(YapimciRaporFilterModel filtre);
        byte[] GetYapimciRaporExcel(List<YapimciRaporModel> yapimciRaporList);
    }
}
