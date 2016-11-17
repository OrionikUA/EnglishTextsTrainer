using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orionik.EnglishTextsTrainer.Models;

namespace Orionik.EnglishTextsTrainer.Logic
{
    [Serializable]
    public class SerializeObject
    {
        public List<string> StringList { get; set; }
        public List<Word> WordList { get; set; }
    }
}
