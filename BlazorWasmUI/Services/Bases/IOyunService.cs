using System.Net;
using BlazorWasmUI.Models.Api;
using System.Threading.Tasks;
using BlazorWasmUI.Models;
using BlazorWasmUI.Models.Filtering;
using BlazorWasmUI.Models.Ordering;
using BlazorWasmUI.Models.Paging;

namespace BlazorWasmUI.Services.Bases
{
    public interface IOyunService
    {
        Task<OyunApiModel> GetOyunlarAsync(OyunFilterModel filtre, PageModel sayfa, OrderModel sira);
        Task<OyunApiModel> GetOyunlarAsync();
        Task<OyunApiModel> GetOyunAsync(int id);
        Task<HttpStatusCode> AddOyunAsync(OyunModel oyun);
        Task<HttpStatusCode> UpdateOyunAsync(OyunModel oyun);
        Task<SonucApiModel> DeleteOyunAsync(int id);
    }
}
