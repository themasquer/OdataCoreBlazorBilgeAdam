using BlazorWasmUI.Configs;
using BlazorWasmUI.Converters;
using BlazorWasmUI.Models;
using BlazorWasmUI.Models.Api;
using BlazorWasmUI.Models.Filtering;
using BlazorWasmUI.Services.Bases;
using BlazorWasmUI.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorWasmUI.Extensions;
using BlazorWasmUI.Models.Excel;
using OfficeOpenXml;

namespace BlazorWasmUI.Services
{
    public class YapimciService : IYapimciService
    {
        private readonly HttpClient _httpClient;
        private readonly string _yapimciApiUrl = AppConfig.ApiUrl + "/yapimcilar";
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public YapimciService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions();
            _jsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
        }

        public async Task<YapimciApiModel> GetYapimcilarAsync()
        {
            YapimciApiModel yapimciApiModel = null;
            var yapimciResult = await _httpClient.GetAsync(_yapimciApiUrl + "/tumyapimcilar?$orderby=adi asc");
            if (yapimciResult.IsSuccessStatusCode)
            {
                yapimciApiModel = JsonSerializer.Deserialize<YapimciApiModel>(await yapimciResult.Content.ReadAsStringAsync());
            }
            return yapimciApiModel;
        }

        public async Task<YapimciApiModel> GetYapimciAsync(int id)
        {
            YapimciApiModel yapimciApiModel = null;
            var yapimciResult = await _httpClient.GetAsync(_yapimciApiUrl + "(" + id + ")");
            if (yapimciResult.IsSuccessStatusCode)
            {
                string yapimciJson = await yapimciResult.Content.ReadAsStringAsync();
                yapimciApiModel = JsonSerializer.Deserialize<YapimciApiModel>(yapimciJson);
                if (yapimciApiModel != null)
                {
                    yapimciApiModel.Yapimcilar = yapimciApiModel.Yapimcilar.Select(y => new YapimciModel()
                    {
                        Id = y.Id,
                        Adi = y.Adi
                    }).ToList(); // OData servisinden bize Where ile filtrelenmiş bir IQueryable döndüğü için önce List'e dönüştürüp sonra listedeki ilk kaydı Yapımcı'ya set ediyoruz.
                    yapimciApiModel.Yapimci = yapimciApiModel.Yapimcilar.FirstOrDefault();
                }
            }
            return yapimciApiModel;
        }

        public async Task<YapimciModel> AddYapimciAsync(YapimciModel yapimci)
        {
            YapimciModel yapimciSonuc = null;
            string yapimciJson = JsonSerializer.Serialize(yapimci); // servise gönderilen JSON
            HttpContent yapimciHttpContent = new StringContent(yapimciJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(_yapimciApiUrl, yapimciHttpContent);
            if (result.IsSuccessStatusCode)
            {
                yapimciJson = await result.Content.ReadAsStringAsync(); // ekleme işleminden sonra servisten dönen JSON
                yapimciSonuc = JsonSerializer.Deserialize<YapimciModel>(yapimciJson);
            }
            return yapimciSonuc;
        }

        public async Task<YapimciModel> UpdateYapimciAsync(YapimciModel yapimci)
        {
            YapimciModel yapimciSonuc = null;
            string yapimciJson = JsonSerializer.Serialize(yapimci); // servise gönderilen JSON
            HttpContent yapimciHttpContent = new StringContent(yapimciJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync(_yapimciApiUrl + "/" + yapimci.Id, yapimciHttpContent);
            if (result.IsSuccessStatusCode)
            {
                yapimciJson = await result.Content.ReadAsStringAsync(); // güncelleme işleminden sonra servisten dönen JSON
                yapimciSonuc = JsonSerializer.Deserialize<YapimciModel>(yapimciJson);
            }
            return yapimciSonuc;
        }

        public async Task<SonucApiModel> DeleteYapimciAsync(int id)
        {
            var result = await _httpClient.DeleteAsync(_yapimciApiUrl + "/" + id);
            var resultJson = await result.Content.ReadAsStringAsync();
            SonucApiModel sonucApiModel;
            if (result.IsSuccessStatusCode)
                sonucApiModel = new SonucApiModel(true);
            else
                sonucApiModel = JsonSerializer.Deserialize<SonucApiModel>(resultJson);
            return sonucApiModel;
        }

        public async Task<YapimciRaporApiModel> GetYapimciRapor(YapimciRaporFilterModel filtre)
        {
            string yapimciRaporApiUrl = AppConfig.ApiUrl + "/yapimcioyunyorumlar/tumyapimcioyunyorumlarinigetir";
            YapimciRaporApiModel yapimciRaporApiModel = null;

            // Content Type: application/x-www-form-urlencoded ile veri post etme:
            Dictionary<string, string> yapimciRaporFiltre = new Dictionary<string, string>();
            yapimciRaporFiltre.Add(nameof(filtre.YapimciId), filtre.YapimciId);
            yapimciRaporFiltre.Add(nameof(filtre.OyunAdi), filtre.OyunAdi);
            DateTime? oyunBaslangicTarihi = string.IsNullOrWhiteSpace(filtre.OyunBaslangicTarihi) ? null : DateTime.ParseExact(filtre.OyunBaslangicTarihi, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            yapimciRaporFiltre.Add(nameof(filtre.OyunBaslangicTarihi), DateTimeUtil.GetStringWithDateTimeOffsetFormatFromDate(oyunBaslangicTarihi));
            DateTime? oyunBitisTarihi = string.IsNullOrWhiteSpace(filtre.OyunBitisTarihi) ? null : DateTime.ParseExact(filtre.OyunBitisTarihi, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            yapimciRaporFiltre.Add(nameof(filtre.OyunBitisTarihi), DateTimeUtil.GetStringWithDateTimeOffsetFormatFromDate(oyunBitisTarihi));
            yapimciRaporFiltre.Add(nameof(filtre.OyunBaslangicPuani), filtre.OyunBaslangicPuani);
            yapimciRaporFiltre.Add(nameof(filtre.OyunBitisPuani), filtre.OyunBitisPuani);
            yapimciRaporFiltre.Add(nameof(filtre.YorumcuAdi), filtre.YorumcuAdi);
            HttpContent yapimciRaporFiltreHttpContent = new FormUrlEncodedContent(yapimciRaporFiltre);

            var result = await _httpClient.PostAsync(yapimciRaporApiUrl, yapimciRaporFiltreHttpContent);
            if (result.IsSuccessStatusCode)
            {
                string yapimciRaporJson = await result.Content.ReadAsStringAsync();
                yapimciRaporApiModel = JsonSerializer.Deserialize<YapimciRaporApiModel>(yapimciRaporJson, _jsonSerializerOptions);
                if (yapimciRaporApiModel != null)
                {
                    yapimciRaporApiModel.YapimciRaporList = yapimciRaporApiModel.YapimciRaporList.Select(y => new YapimciRaporModel()
                    {
                        Id = y.Id,
                        OyunAdi = y.OyunAdi,
                        YorumcuAdi = y.YorumcuAdi,
                        OyunPuani = y.OyunPuani,
                        OyunPuaniText = y.OyunPuani.ToString(new CultureInfo("tr")),
                        OyunTarihi = y.OyunTarihi,
                        OyunTarihiText = y.OyunTarihi.ToString("dd.MM.yyyy", new CultureInfo("tr")),
                        YapimciAdi = y.YapimciAdi,
                        YorumAciklamasi = y.YorumAciklamasi,
                        YorumTarihi = y.YorumTarihi,
                        YorumTarihiText = y.YorumTarihi.ToString("dd.MM.yyyy", new CultureInfo("tr"))
                    }).ToList();
                    yapimciRaporApiModel.Filtre = filtre;
                }
            }
            return yapimciRaporApiModel;
        }

        public byte[] GetYapimciRaporExcel(List<YapimciRaporModel> yapimciRaporList)
        {
            List<YapimciRaporExcelModel> excelList = yapimciRaporList.Select(y => new YapimciRaporExcelModel()
            {
                YapimciAdi = y.YapimciAdi,
                OyunAdi = y.OyunAdi,
                OyunPuani = y.OyunPuaniText,
                OyunTarihi = y.OyunTarihiText,
                YorumcuAdi = y.YorumcuAdi,
                YorumAciklamasi = y.YorumAciklamasi,
                YorumTarihi = y.YorumTarihiText
            }).ToList();
            DataTable excelDataTable = excelList.ToDataTable();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Yapımcı Raporu");
            excelWorksheet.Cells["A1"].LoadFromDataTable(excelDataTable, true);
            //excelWorksheet.Cells["A:AZ"].AutoFitColumns(); // Blazor'da hata veriyor!
            byte[] data = excelPackage.GetAsByteArray();
            return data;
        }
    }
}
