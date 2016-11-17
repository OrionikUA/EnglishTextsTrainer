using System;

namespace Orionik.EnglishTextsTrainer.Models
{
    [Serializable]
    public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Meanings { get; set; }
        public bool Ignore { get; set; }
        public bool Know { get; set; }

        public override string ToString()
        {
            return $"{Name}|{Meanings}";
        }
    }
}
