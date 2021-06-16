using System.Net.Http;
using BlazorWasmUI.Models.Api;
using BlazorWasmUI.Services.Bases;
using System.Threading.Tasks;
using BlazorWasmUI.Configs;
using System.Text.Json;

namespace BlazorWasmUI.Services
{
    public class HakkindaService : IHakkindaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _hazirlayanApiUrl = AppConfig.ApiUrl + "/hazirlayanigetir";

        public HakkindaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HakkindaApiModel> GetHazirlayanAsync()
        {
            HakkindaApiModel hakkindaApiModel = null;
            var hazirlayanResult = await _httpClient.GetAsync(_hazirlayanApiUrl);
            if (hazirlayanResult.IsSuccessStatusCode)
            {
                hakkindaApiModel = JsonSerializer.Deserialize<HakkindaApiModel>(await hazirlayanResult.Content.ReadAsStringAsync());
            }
            return hakkindaApiModel;
        }
    }
}
