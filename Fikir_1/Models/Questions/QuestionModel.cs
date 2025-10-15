namespace Fikir_1.Models.Questions
{
    public class QuestionModel
    {
        public int Id { get; set; } // Soru kimliği
        public int LegendId { get; set; } // Hangi efsaneye sorulduğu
        public string UserId { get; set; } // Soruyu soran kullanıcı (opsiyonel)
        public string QuestionText { get; set; } // Kullanıcının sorduğu soru
        public string ResponseText { get; set; } // Efsanenin cevabı
        public DateTime AskedAt { get; set; } // Sorunun tarihi
    }
}
