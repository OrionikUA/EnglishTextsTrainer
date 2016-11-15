using System;
using System.Diagnostics;
using System.IO;

namespace Orionik.EnglishTextsTrainer.Logger
{
    public class Logging
    {
        public static Logging Instance => _instance ?? (_instance = new Logging());

        private static Logging _instance;
        private readonly string _logFile;

        private Logging()
        {
            var dirPath = AppDomain.CurrentDomain.BaseDirectory + "/Log";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            var date = DateTime.Now;
            _logFile = $"{dirPath}/{date:yyyy-MM-dd-HH-mm-ss}.txt";
            if (!File.Exists(_logFile))
            {
                File.CreateText(_logFile).Close();
            }
        }

        public void Write(Type type, string message)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd (HH:mm:ss)} - {type}: {message}";
            Debug.WriteLine(logMessage);
            WriteIntoFile(logMessage);
        }

        public void Write(Type type, Exception exception)
        {
            var logMessage = $"Exception! {DateTime.Now:yyyy-MM-dd (HH:mm:ss)} - {type}: {exception.Message}";
            Debug.WriteLine(logMessage);
            WriteIntoFile(logMessage);
        }

        private void WriteIntoFile(string message)
        {
            using (StreamWriter sw = File.AppendText(_logFile))
            {
                sw.WriteLine(message);
            }
        }
    }
}
