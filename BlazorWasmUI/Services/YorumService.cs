using System;
using BlazorWasmUI.Configs;
using BlazorWasmUI.Models;
using BlazorWasmUI.Models.Api;
using BlazorWasmUI.Services.Bases;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorWasmUI.Converters;

namespace BlazorWasmUI.Services
{
    public class YorumService : IYorumService
    {
        private readonly HttpClient _httpClient;
        private readonly string _yorumApiUrl = AppConfig.ApiUrl + "/yorumlar";
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public YorumService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions();
            _jsonSerializerOptions.Converters.Add(new JsonDateTimeConverter()); // OData'da tarih formatı "yyyy-MM-ddTHH:mm:ssZ" olduğu için kendi JsonConverter'ımızı yazmak zorunda kaldık
        }

        public async Task<YorumApiModel> GetYorumlarAsync()
        {
            YorumApiModel yorumApiModel = null;
            var yorumResult = await _httpClient.GetAsync(_yorumApiUrl);
            if (yorumResult.IsSuccessStatusCode)
            {
                string yorumJson = await yorumResult.Content.ReadAsStringAsync();
                yorumApiModel = JsonSerializer.Deserialize<YorumApiModel>(yorumJson, _jsonSerializerOptions);
                if (yorumApiModel != null)
                {
                    yorumApiModel.Yorumlar = yorumApiModel.Yorumlar.Select(y => new YorumModel()
                    {
                        Id = y.Id,
                        YorumcuAdi = y.YorumcuAdi,
                        Aciklamasi = y.Aciklamasi,
                        Tarihi = y.Tarihi,
                        TarihiText = y.Tarihi.ToString("dd.MM.yyyy", new CultureInfo("tr")),
                        OyunId = y.OyunId,
                        OyunIdText = y.OyunId.ToString(),
                        OyunAdi = y.OyunAdi,
                        OyunPuani = y.OyunPuani,
                        OyunPuaniText = y.OyunPuani.ToString(new CultureInfo("tr")),
                        OyunTarihi = y.OyunTarihi,
                        OyunTarihiText = y.OyunTarihi.ToString("dd.MM.yyyy", new CultureInfo("tr")),
                        OyunYapimciId = y.OyunYapimciId,
                        OyunYapimciAdi = y.OyunYapimciAdi
                    }).ToList();
                }
            }
            return yorumApiModel;
        }

        public async Task<YorumApiModel> GetYorumAsync(int id)
        {
            YorumApiModel yorumApiModel = null;
            var yorumResult = await _httpClient.GetAsync(_yorumApiUrl + "(" + id + ")");
            if (yorumResult.IsSuccessStatusCode)
            {
                string yorumJson = await yorumResult.Content.ReadAsStringAsync();
                yorumApiModel = JsonSerializer.Deserialize<YorumApiModel>(yorumJson, _jsonSerializerOptions);
                if (yorumApiModel != null)
                {
                    yorumApiModel.Yorumlar = yorumApiModel.Yorumlar.Select(y => new YorumModel()
                    {
                        Id = y.Id,
                        YorumcuAdi = y.YorumcuAdi,
                        Aciklamasi = y.Aciklamasi,
                        Tarihi = y.Tarihi,
                        TarihiText = y.Tarihi.ToString("dd.MM.yyyy", new CultureInfo("tr")),
                        OyunId = y.OyunId,
                        OyunIdText = y.OyunId.ToString(),
                        OyunAdi = y.OyunAdi,
                        OyunPuani = y.OyunPuani,
                        OyunPuaniText = y.OyunPuani.ToString(new CultureInfo("tr")),
                        OyunTarihi = y.OyunTarihi,
                        OyunTarihiText = y.OyunTarihi.ToString("dd.MM.yyyy", new CultureInfo("tr")),
                        OyunYapimciId = y.OyunYapimciId,
                        OyunYapimciAdi = y.OyunYapimciAdi
                    }).ToList();
                    yorumApiModel.Yorum = yorumApiModel.Yorumlar.FirstOrDefault();
                }
            }
            return yorumApiModel;
        }

        public async Task<HttpStatusCode> AddYorumAsync(YorumModel yorum)
        {
            yorum.OyunId = Convert.ToInt32(yorum.OyunIdText);
            yorum.Tarihi = DateTime.Now;
            string yorumJson = JsonSerializer.Serialize(yorum, _jsonSerializerOptions);
            HttpContent yorumHttpContent = new StringContent(yorumJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(_yorumApiUrl, yorumHttpContent);
            return result.StatusCode;
        }

        public async Task<HttpStatusCode> UpdateYorumAsync(YorumModel yorum)
        {
            yorum.OyunId = Convert.ToInt32(yorum.OyunIdText);
            yorum.Tarihi = DateTime.Now;
            string yorumJson = JsonSerializer.Serialize(yorum, _jsonSerializerOptions);
            HttpContent yorumHttpContent = new StringContent(yorumJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync(_yorumApiUrl + "(" + yorum.Id + ")", yorumHttpContent);
            return result.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteYorumAsync(int id)
        {
            var result = await _httpClient.DeleteAsync(_yorumApiUrl + "/" + id);
            return result.StatusCode;
        }
    }
}
