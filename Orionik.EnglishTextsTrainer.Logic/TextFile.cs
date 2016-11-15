using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Orionik.EnglishTextsTrainer.Logger;

namespace Orionik.EnglishTextsTrainer.Logic
{
    public static class TextFile
    {
        public static string ReadFile(string path)
        {
            Logging.Instance.Write(typeof(TextFile), "Start ReadFile");
            var stringBuilder = new StringBuilder();
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        stringBuilder.Append(line);
                        stringBuilder.Append(" ");
                    }
                }
                
            }
            else
            {
                var exception = new FileNotFoundException("Cannot open file", path);
                Logging.Instance.Write(typeof(TextFile), exception);
                throw exception;
            }
            Logging.Instance.Write(typeof(TextFile), "End ReadFile");
            return stringBuilder.ToString().ToLower();
        }

        public static void WriteToFile(string text, string filePath)
        {
            Logging.Instance.Write(typeof(TextFile), "Start WriteToFile overload string");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (FileStream fs = File.Create(filePath))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(text);
                fs.Write(info, 0, info.Length);
            }
            Logging.Instance.Write(typeof(TextFile), "End WriteToFile overload string");
        }

        public static void WriteToFile(List<string> list, string filePath)
        {
            Logging.Instance.Write(typeof(TextFile), "Start WriteToFile overload list");
            var stringBuilder = new StringBuilder();
            foreach (var word in list)
            {
                stringBuilder.Append(word);
                stringBuilder.Append("\n");
            }
            WriteToFile(stringBuilder.ToString(), filePath);
            Logging.Instance.Write(typeof(TextFile), "End WriteToFile overload list");
        }
    }
}
