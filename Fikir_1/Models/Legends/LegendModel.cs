namespace Fikir_1.Models.Legends
{
    public class LegendModel
    {
        public int Id { get; set; }  // Her efsane için benzersiz ID
        public string Name { get; set; } // Efsanenin adı
        public string Description { get; set; } // Kısa açıklama (Kimdir, ne yapmıştır?)
        public string PersonalityTraits { get; set; } // Konuşma tarzı, kişilik özellikleri
        public string PhotoUrl { get; set; } // Efsanenin görsel yolu
        public string SampleQuestions { get; set; } // Kullanıcılara önerilen sorular (virgülle ayrılabilir)
        public float VoicePitch { get; set; } = 1.0f; // varsayılan ses tonu
        public string VoiceName { get; set; } // Kullanılacak sesin adı
        public string ImageData { get; set; } //Base64 encoded görsel
        public string Characteristics { get; set; } //Efsaneye dair bilgiler

    }
}
