using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Fikir_1.Services
{
    public class OpenAIService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public OpenAIService(IConfiguration configuration)
        {
            _apiKey = configuration["OpenAI:ApiKey"]; // appsettings.json'dan API Key al
            _httpClient = new HttpClient(); // HTTP isteklerini göndermek için HttpClient kullan
        }

        public async Task<string> GetAIResponse(string prompt)
        {
            // OpenAI API'ye gönderilecek JSON veri
            var requestBody = new
            {
                model = "gpt-3.5-turbo",  // Daha gelişmiş model kullanmak istersen "gpt-4" de olabilir
                messages = new[]
                {
                    new { role = "system", content = "You are a historical legend answering questions." },
                    new { role = "user", content = prompt }
                }
            };

            var requestJson = JsonSerializer.Serialize(requestBody);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

            // API Key’i yetkilendirme başlığına ekle
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            // OpenAI API'ye POST isteği gönder
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                return "AI servisiyle iletişim kurulamadı.";
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var aiResponse = doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            return aiResponse ?? "AI'dan bir cevap alınamadı.";
        }
    }
}
