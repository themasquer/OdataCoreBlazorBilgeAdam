using System.Net;
using BlazorWasmUI.Models.Api;
using System.Threading.Tasks;
using BlazorWasmUI.Models;

namespace BlazorWasmUI.Services.Bases
{
    public interface IYorumService
    {
        Task<YorumApiModel> GetYorumlarAsync();
        Task<YorumApiModel> GetYorumAsync(int id);
        Task<HttpStatusCode> AddYorumAsync(YorumModel yorum);
        Task<HttpStatusCode> UpdateYorumAsync(YorumModel yorum);
        Task<HttpStatusCode> DeleteYorumAsync(int id);
    }
}
