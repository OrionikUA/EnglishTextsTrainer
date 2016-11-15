using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Orionik.EnglishTextsTrainer.Logger;

namespace Orionik.EnglishTextsTrainer.Logic
{
    public static class TextWords
    {
        public static List<string> FindWords(string text)
        {
            Logging.Instance.Write(typeof(TextWords), "Start FindWords");
            var pattern = new Regex(
                @"( [^\W_\d]( [^\W_\d] | [-'\d](?=[^\W_\d]))*[^\W_\d] )",
                RegexOptions.IgnorePatternWhitespace);
            Logging.Instance.Write(typeof(TextWords), "End FindWords");
            return (from Match match in pattern.Matches(text) select match.Groups[1].Value).ToList();

        }

        public static List<string> GetUniqueWordList(List<string> list)
        {
            Logging.Instance.Write(typeof(TextWords), "Start GetUniqueWordList");
            var wordList = new List<string>();

            foreach (var item in wordList)
            {
                if (!wordList.Contains(item))
                {
                    wordList.Add(item);
                }
            }
            Logging.Instance.Write(typeof(TextWords), "End GetUniqueWordList");
            return wordList;
        }

        public static Dictionary<string, int> GetUniqueWordDictionary(List<string> list)
        {
            Logging.Instance.Write(typeof(TextWords), "Start GetUniqueWordDictionary");
            var wordDictionary = new Dictionary<string, int>();

            foreach (var item in list)
            {
                if (wordDictionary.ContainsKey(item))
                {
                    wordDictionary[item]++;
                }
                else
                {
                    wordDictionary.Add(item, 1);
                }
            }
            Logging.Instance.Write(typeof(TextWords), "End GetUniqueWordDictionary");
            return wordDictionary;
        }
    }
}
