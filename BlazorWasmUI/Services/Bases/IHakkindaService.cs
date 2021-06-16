using BlazorWasmUI.Models.Api;
using System.Threading.Tasks;

namespace BlazorWasmUI.Services.Bases
{
    public interface IHakkindaService
    {
        Task<HakkindaApiModel> GetHazirlayanAsync();
    }
}
