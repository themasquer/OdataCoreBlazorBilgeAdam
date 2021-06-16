using BlazorWasmUI.Configs;
using BlazorWasmUI.Converters;
using BlazorWasmUI.Models;
using BlazorWasmUI.Models.Api;
using BlazorWasmUI.Models.Filtering;
using BlazorWasmUI.Models.Ordering;
using BlazorWasmUI.Models.Paging;
using BlazorWasmUI.Services.Bases;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorWasmUI.Services
{
    public class OyunService : IOyunService
    {
        private readonly HttpClient _httpClient;
        private readonly string _oyunApiUrl = AppConfig.ApiUrl + "/oyunlar";
        private readonly JsonSerializerOptions _jsonSerializerOptions; 

        public OyunService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions();
            _jsonSerializerOptions.Converters.Add(new JsonDateTimeConverter()); // OData'da tarih formatı "yyyy-MM-ddTHH:mm:ssZ" olduğu için kendi JsonConverter'ımızı yazmak zorunda kaldık
        }

        public async Task<OyunApiModel> GetOyunlarAsync(OyunFilterModel filtre, PageModel sayfa, OrderModel sira)
        {
            OyunApiModel oyunApiModel = null;
            string orderByQueryString = "$orderby=";
            if (!string.IsNullOrWhiteSpace(sira.Expression))
            {
                switch (sira.Expression)
                {
                    case "Oyun Adı": 
                        orderByQueryString += sira.DirectionAscending ? "adi asc" : "adi desc";
                        break;
                    case "Oyun Tarihi":
                        orderByQueryString += sira.DirectionAscending ? "tarihi asc" : "tarihi desc";
                        break;
                    case "Oyun Puanı":
                        orderByQueryString += sira.DirectionAscending ? "puani asc" : "puani desc";
                        break;
                    default:
                        orderByQueryString += sira.DirectionAscending ? "yapimci/adi asc" : "yapimci/adi desc";
                        break;
                }
            }
            // sorgu sonucunda tüm sonuçları getirebilmek ve and ile aşağıdaki filtreleri ekleyebilmek için 1 = 1 yazıyoruz
            string filtreQueryString = "$filter=1 eq 1"; 
            if (!string.IsNullOrWhiteSpace(filtre.Adi))
                filtreQueryString = filtreQueryString + " and contains(tolower(adi), trim(tolower('" + filtre.Adi + "')))";
            if (!string.IsNullOrWhiteSpace(filtre.YapimciIdText))
                filtreQueryString = filtreQueryString + " and yapimciid eq " + filtre.YapimciIdText;
            if (!string.IsNullOrWhiteSpace(filtre.BaslangicPuaniText))
                filtreQueryString = filtreQueryString + " and puani ge " + filtre.BaslangicPuaniText;
            if (!string.IsNullOrWhiteSpace(filtre.BitisPuaniText))
                filtreQueryString = filtreQueryString + " and puani le " + filtre.BitisPuaniText;
            if (!string.IsNullOrWhiteSpace(filtre.BaslangicTarihiText))
            {
                DateTime baslangicTarihi = DateTime.ParseExact(filtre.BaslangicTarihiText + " 00:00:00", "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                string baslangicTarihiText = baslangicTarihi.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                filtreQueryString = filtreQueryString + " and tarihi ge " + baslangicTarihiText;
            }
            if (!string.IsNullOrWhiteSpace(filtre.BitisTarihiText))
            {
                DateTime bitisTarihi = DateTime.ParseExact(filtre.BitisTarihiText + " 23:59:59", "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                string bitisTarihiText = bitisTarihi.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                filtreQueryString = filtreQueryString + " and tarihi le " + bitisTarihiText;
            }
            var oyunCountResult = await _httpClient.GetAsync(_oyunApiUrl + "/$count?" + filtreQueryString);
            if (oyunCountResult.IsSuccessStatusCode)
            {
                int oyunCount = Convert.ToInt32(await oyunCountResult.Content.ReadAsStringAsync());
                sayfa = new PageModel(oyunCount, sayfa.RecordsPerPageCount, sayfa.PageNumber);
                string skipTakeQueryString = "$skip=" + sayfa.Skip + "&$top=" + sayfa.Take;
                var oyunResult = await _httpClient.GetAsync(_oyunApiUrl
                                                            + "?$count=true&$expand=yapimci"
                                                            + "&" + orderByQueryString
                                                            + "&" + filtreQueryString
                                                            + "&" + skipTakeQueryString);
                if (oyunResult.IsSuccessStatusCode)
                {
                    string oyunJson = await oyunResult.Content.ReadAsStringAsync();
                    oyunApiModel = JsonSerializer.Deserialize<OyunApiModel>(oyunJson, _jsonSerializerOptions);
                    if (oyunApiModel != null)
                    {
                        oyunApiModel.Oyunlar = oyunApiModel.Oyunlar.Select(o => new OyunModel()
                        {
                            Id = o.Id,
                            Adi = o.Adi,
                            Puani = o.Puani,
                            PuaniText = o.Puani.ToString(new CultureInfo("tr")),
                            Tarihi = o.Tarihi,
                            TarihiText = o.Tarihi.ToString("dd.MM.yyyy", new CultureInfo("tr")),
                            YapimciId = o.YapimciId,
                            YapimciIdText = o.YapimciId.ToString(),
                            Yapimci = o.Yapimci
                        }).ToList();
                        oyunApiModel.Filtre = filtre;
                        oyunApiModel.Sayfa = sayfa;
                        oyunApiModel.Sira = sira;
                    }
                }
            }
            return oyunApiModel;
        }

        public async Task<OyunApiModel> GetOyunlarAsync()
        {
            OyunApiModel oyunApiModel = null;
            var oyunResult = await _httpClient.GetAsync(_oyunApiUrl + "?$orderby=adi asc");
            if (oyunResult.IsSuccessStatusCode)
            {
                string oyunJson = await oyunResult.Content.ReadAsStringAsync();
                oyunApiModel = JsonSerializer.Deserialize<OyunApiModel>(oyunJson, _jsonSerializerOptions);
                if (oyunApiModel != null)
                {
                    oyunApiModel.Oyunlar = oyunApiModel.Oyunlar.Select(o => new OyunModel()
                    {
                        Id = o.Id,
                        Adi = o.Adi,
                        Puani = o.Puani,
                        PuaniText = o.Puani.ToString(new CultureInfo("tr")),
                        Tarihi = o.Tarihi,
                        TarihiText = o.Tarihi.ToString("dd.MM.yyyy", new CultureInfo("tr")),
                        YapimciId = o.YapimciId,
                        YapimciIdText = o.YapimciId.ToString(),
                        Yapimci = o.Yapimci
                    }).ToList();
                }
            }
            return oyunApiModel;
        }

        public async Task<OyunApiModel> GetOyunAsync(int id)
        {
            OyunApiModel oyunApiModel = null;
            var oyunResult = await _httpClient.GetAsync(_oyunApiUrl + "(" + id + ")?$expand=yapimci");
            if (oyunResult.IsSuccessStatusCode)
            {
                string oyunJson = await oyunResult.Content.ReadAsStringAsync();
                oyunApiModel = JsonSerializer.Deserialize<OyunApiModel>(oyunJson, _jsonSerializerOptions);
                if (oyunApiModel != null)
                {
                    oyunApiModel.Oyunlar = oyunApiModel.Oyunlar.Select(o => new OyunModel()
                    {
                        Id = o.Id,
                        Adi = o.Adi,
                        Puani = o.Puani,
                        PuaniText = o.Puani.ToString(new CultureInfo("tr")),
                        Tarihi = o.Tarihi,
                        TarihiText = o.Tarihi.ToString("dd.MM.yyyy", new CultureInfo("tr")),
                        YapimciId = o.YapimciId,
                        YapimciIdText = o.YapimciId.ToString(),
                        Yapimci = o.Yapimci
                    }).ToList(); // OData servisinden bize Where ile filtrelenmiş bir IQueryable döndüğü için önce List'e dönüştürüp sonra listedeki ilk kaydı Oyun'a set ediyoruz.
                    oyunApiModel.Oyun = oyunApiModel.Oyunlar.FirstOrDefault();
                }
            }
            return oyunApiModel;
        }

        public async Task<HttpStatusCode> AddOyunAsync(OyunModel oyun)
        {
            oyun.Puani = Convert.ToDouble(oyun.PuaniText.Replace(",", "."), CultureInfo.InvariantCulture);
            oyun.Tarihi = DateTime.ParseExact(oyun.TarihiText, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            oyun.YapimciId = Convert.ToInt32(oyun.YapimciIdText);

            //var result = await _httpClient.PostAsJsonAsync(_oyunApiUrl, oyun); // OData servisi kullandığımız için hata veriyor
            string oyunJson = JsonSerializer.Serialize(oyun, _jsonSerializerOptions);
            HttpContent oyunHttpContent = new StringContent(oyunJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(_oyunApiUrl, oyunHttpContent);

            return result.StatusCode;
        }

        public async Task<HttpStatusCode> UpdateOyunAsync(OyunModel oyun)
        {
            oyun.Puani = Convert.ToDouble(oyun.PuaniText.Replace(",", "."), CultureInfo.InvariantCulture);
            oyun.Tarihi = DateTime.ParseExact(oyun.TarihiText, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            oyun.YapimciId = Convert.ToInt32(oyun.YapimciIdText);

            //var result = await _httpClient.PutAsJsonAsync(_oyunApiUrl, oyun); // OData servisi kullandığımız için hata veriyor
            string oyunJson = JsonSerializer.Serialize(oyun, _jsonSerializerOptions);
            HttpContent oyunHttpContent = new StringContent(oyunJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync(_oyunApiUrl + "/" + oyun.Id, oyunHttpContent);

            return result.StatusCode;
        }

        public async Task<SonucApiModel> DeleteOyunAsync(int id)
        {
            var result = await _httpClient.DeleteAsync(_oyunApiUrl + "/" + id);
            var resultJson = await result.Content.ReadAsStringAsync();
            SonucApiModel sonucApiModel;
            if (result.IsSuccessStatusCode)
                sonucApiModel = new SonucApiModel(true);
            else
                sonucApiModel = JsonSerializer.Deserialize<SonucApiModel>(resultJson);
            return sonucApiModel;
        }
    }
}
