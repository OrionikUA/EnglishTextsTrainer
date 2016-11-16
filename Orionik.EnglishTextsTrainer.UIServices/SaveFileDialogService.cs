using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Orionik.EnglishTextsTrainer.UIServices
{
    public class SaveFileDialogService
    {
        private readonly string _defaultExtension;
        private readonly string _filter;

        public SaveFileDialogService(string defaultExtension, string filter)
        {
            _defaultExtension = defaultExtension;
            _filter = filter;
        }

        public string CreateAndOpenFile()
        {
            var sd = new SaveFileDialog
            {
                Filter = _filter,
                DefaultExt = _defaultExtension
            };
            var result = sd.ShowDialog() ?? false;
            if (result)
            {
                if (File.Exists(sd.FileName))
                {
                    File.Delete(sd.FileName);
                }
                (File.Create(sd.FileName)).Close();
                return sd.FileName;
            }
            return null;
        }
    }
}
