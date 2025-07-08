using DalilBus.Config;
using System.Net;
using System.Net.Http.Json;

namespace DalilBus.Helper
{
    public class ApiClient
    {
        public readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(ApiConfig.BaseUrl) };
            _httpClient.DefaultRequestHeaders.Add("apiKey", ApiConfig.ApiKey);
        }

        public async Task<T?> GetFromSubabaseAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = response.StatusCode switch
                {
                    HttpStatusCode.NotFound => new ErrorMessage(
                        "Data not found | البيانات غير موجودة",
                        "Could not fetch data | تعذر جلب البيانات"),

                    HttpStatusCode.Unauthorized => new ErrorMessage(
                        "Unauthorized | غير مصرح",
                        "Invalid API key | مفتاح API غير صالح"),

                    _ => new ErrorMessage(
                        $"Server error ({response.StatusCode}) | خطأ في الخادم ({response.StatusCode})", await response.Content.ReadAsStringAsync())
                };

                throw new HttpRequestException($"{errorMsg.Tittel}: {errorMsg.Details}");
            }

            return await response.Content.ReadFromJsonAsync<T>()
                ?? throw new HttpRequestException("Error loading data of | خطأ في تحميل بيانات");
        }

        private record ErrorMessage(string Tittel, string Details);
    }
}
