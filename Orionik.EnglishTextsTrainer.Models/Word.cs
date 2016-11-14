namespace Orionik.EnglishTextsTrainer.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Meanings { get; set; }
        public bool Ignore { get; set; }
        public bool Know { get; set; }
    }
}
