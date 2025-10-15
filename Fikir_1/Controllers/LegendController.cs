using Microsoft.AspNetCore.Mvc;
using Fikir_1.Models.Legends;
using Fikir_1.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fikir_1.Models.Legends;
using Fikir_1.Services;
using System.Text.Json;

namespace YourProject.Controllers
{
    public class LegendController : Controller
    {
        private static List<LegendModel> legends = new List<LegendModel>
{
    new LegendModel
    {
        Id = 1,
        Name = "Sokrates",
        Description = "Antik Yunan filozofu; sorgulayıcı düşünce tarzı ve ahlaki felsefesiyle tanınır.",
        PersonalityTraits = "Sorgulayıcı, mantıklı",
        PhotoUrl = "/images/sokrates.jpg",
        SampleQuestions = "Bilgelik nedir?, Mutluluk nasıl elde edilir?",
        VoicePitch = 0.9f,
        VoiceName = "Microsoft Tolga - Turkish (Turkey) (tr-TR)",
        Characteristics = "An ancient Greek philosopher with a long, curly beard, bald head with some white hair on the sides, "
                        + "and wearing a traditional Greek himation (draped robe). His expression is thoughtful, as if in deep discussion. "
                        + "He is standing in front of the Acropolis in Athens, with the Parthenon visible in the background."
    },

    new LegendModel
    {
        Id = 2,
        Name = "Isaac Newton",
        Description = "İngiliz fizikçi ve matematikçi; yerçekimi yasası ve hareket kanunlarıyla bilinir.",
        PersonalityTraits = "Meraklı, analitik",
        PhotoUrl = "/images/newton.jpg",
        SampleQuestions = "Yerçekimi nasıl çalışır?, Hareket yasalarınız nedir?",
        VoicePitch = 1.1f,
        VoiceName = "Google Türkçe (Male)",
        Characteristics = "A 17th-century physicist with shoulder-length wavy hair, wearing a long coat and waistcoat typical of his era. "
                        + "His expression is serious, and he holds an apple in his hand, symbolizing his discovery of gravity. "
                        + "He is standing in front of Trinity College, Cambridge, where he conducted much of his research."
    },

    
    new LegendModel
{
    Id = 3,
    Name = "Suleiman the Magnificent",
    Description = "Osmanlı'nın en uzun süre tahtta kalan padişahı; askeri fetihleri, hukuk reformları ile tanınır.",
    PersonalityTraits = "Powerful, strategic, diplomatic",
    PhotoUrl = "/images/mimarsinan.jpg",
    SampleQuestions = "Osmanlı'nın en büyük fetihleri nelerdir?, Kanunlarınızı nasıl belirlediniz?",
    VoicePitch = 1.0f,
    VoiceName = "Microsoft Yelda Online (Natural)",
    Characteristics = "A mighty 16th-century Ottoman Sultan, wearing an imperial kaftan with intricate gold embroidery, "
                    + "and a grand white turban adorned with jewels. His thick mustache and deep, piercing eyes reflect authority and wisdom. "
                    + "He holds a ceremonial sword and is standing in front of **Topkapi Palace in Istanbul**, the seat of his empire."
}
};


        private readonly OpenAIService _openAIService;



        public LegendController(OpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        public IActionResult RedirectToPhoto(int id)
        {
            var legend = legends.FirstOrDefault(l => l.Id == id);
            if (legend == null) return NotFound();

            return RedirectToAction("GenerateLegendImage", "Photo", new { legendName = legend.Name });
        }

        public IActionResult Index()
        {
            return View(legends);
        }

        public IActionResult Chat(int id)
        {
            var legend = legends.Find(l => l.Id == id);
            if (legend == null)
            {
                return NotFound();
            }

            // Kullanıcı yeni bir efsane seçtiğinde sohbet geçmişini temizle
            HttpContext.Session.Remove("ChatHistory");

            //Eğer oturumda sohbet geçmişi yoksa yeni bir liste başlat
            if(HttpContext.Session.GetString("ChatHistory") == null)
            {
                var charHistory = new List<object>
                {
                    new { role = "system", content = $"Sen {legend.Name} adlı tarihi bir figürsün. {legend.PersonalityTraits} bir kişiliğin var. Kullanıcının sorularına cevap ver."}
                };
                HttpContext.Session.SetString("ChatHistory", JsonSerializer.Serialize(charHistory));
            }


            return View(legend);
        }

        [HttpPost]
        public async Task<IActionResult> AskQuestion(int legendId, string questionText)
        {
            
            var legend = legends.Find(l => l.Id == legendId);
            if (legend == null)
            {
                return NotFound();
            }

            //Session'dan bir önceki sohbet geçmişini al
            var chatHistoryJson = HttpContext.Session.GetString("ChatHistory");
            var chatHistory = JsonSerializer.Deserialize<List<object>>(chatHistoryJson) ?? new List<object>();

            // Kullanıcının yeni mesajını ekle
            chatHistory.Add(new { role = "user", content = questionText });

            // OpenAI'ye tüm sohbet geçmişini göster
            string aiResponse = await _openAIService.GetAIResponse(JsonSerializer.Serialize(chatHistory));

            //AI'nın cevabını sohbet geçmişine ekle
            chatHistory.Add(new { role = "assistant", content = aiResponse });

            // Güncellenmiş sohbet geçmişini tekrar session'a kaydet
            HttpContext.Session.SetString("ChatHistory", JsonSerializer.Serialize(chatHistory));

            // Test için cevap çıktısını konsola yazdıralım
            Console.WriteLine("AI Response: " + aiResponse);

            ViewBag.RedirectUrl = Url.Action("GenerateLegendImage", "Photo", new { legendName = legend.Name });
            ViewBag.Answer = aiResponse;
            return View("Chat", legend);
           
        }
    }
}
