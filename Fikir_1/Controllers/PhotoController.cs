using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System;
using Fikir_1.Models.Legends;
namespace Fikir_1.Controllers
{
    public class PhotoController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _openAIApiKey = "";
        public PhotoController()
        {
            _httpClient = new HttpClient();
        }


        [HttpPost]
        public async Task<IActionResult> GenerateLegendImage([FromBody] LegendModel legend)
        {
            if (string.IsNullOrEmpty(legend.ImageData))
            {
                return Json(new { success = false, message = "No image provided" });
            }

            string legendName = legend.Name; // Get legend's name
            byte[] imageBytes = Convert.FromBase64String(legend.ImageData); // Decode Base64 image

            // Ensure the upload directory exists
            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            // Generate a unique filename and save the image
            string fileName = $"{Guid.NewGuid()}.jpg";
            string filePath = Path.Combine(uploadDir, fileName);
            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            // Convert saved image back to Base64 for OpenAI
            string base64Image = Convert.ToBase64String(imageBytes);

            // 🔹 OpenAI GPT-4 Vision API Request
            var visionRequestBody = new
            {
                model = "gpt-4-turbo",
                messages = new object[]
                {
            new { role = "system", content = "Describe the main physical features of the person in this image." },
            new {
                role = "user",
                content = new object[]
                {
                    new { type = "text", text = "Describe this person's key facial features, hair color, eye color, and accessories." },
                    new { type = "image_url", image_url = new { url = $"data:image/jpeg;base64,{base64Image}" } } // ✅ Send Base64 to OpenAI
                }
            }
                },
                max_tokens = 300
            };

            var visionJsonContent = new StringContent(JsonSerializer.Serialize(visionRequestBody), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_openAIApiKey}");

            var visionResponse = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", visionJsonContent);
            var visionJsonResponse = await visionResponse.Content.ReadAsStringAsync();

            if (!visionResponse.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Image analysis failed.", error = visionJsonResponse });
            }

            using var visionDoc = JsonDocument.Parse(visionJsonResponse);
            string extractedFeatures = visionDoc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            // 🔹 Generate DALL·E 3 Prompt
            string prompt = $@"
A semi-minimalist cartoon-style illustration of a modern person, {extractedFeatures}.
They are prominently interacting with {legendName}, a famous historical figure.
The illustration must depict {legendName} **accurately**, following their **historical clothing and facial features**:
{legend.Characteristics}
The background must include {legendName}'s most iconic landmark **as accurately as possible**, ensuring that both the figure and their historical setting are unmistakable.
The composition should ensure that {legendName} and the landmark **are central and clearly recognizable**.
The illustration should balance clean, stylized facial details with a softly stylized background, creating a historically meaningful composition.
";
            var dalleRequestBody = new
            {
                model = "dall-e-3",
                prompt = prompt,
                n = 1,
                size = "1024x1024"
            };

            var dalleJsonContent = new StringContent(JsonSerializer.Serialize(dalleRequestBody), Encoding.UTF8, "application/json");
            var dalleResponse = await _httpClient.PostAsync("https://api.openai.com/v1/images/generations", dalleJsonContent);
            var dalleJsonResponse = await dalleResponse.Content.ReadAsStringAsync();

            if (!dalleResponse.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Image generation failed.", error = dalleJsonResponse });
            }

            using var dalleDoc = JsonDocument.Parse(dalleJsonResponse);
            string generatedImageUrl = dalleDoc.RootElement
                .GetProperty("data")[0]
                .GetProperty("url")
                .GetString();

            return Json(new { success = true, imageUrl = generatedImageUrl });
        }


    }
}
