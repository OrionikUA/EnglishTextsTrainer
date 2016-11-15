using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace Orionik.EnglishTextsTrainer.UIServices
{
    public class OpenFileDialogService
    {
        private readonly string _defaultExtension;
        private readonly string _filter;

        public OpenFileDialogService(string defaultExtension, string filter)
        {
            _defaultExtension = defaultExtension;
            _filter = filter;
        }

        public string OpenFile()
        {
            var fd = new OpenFileDialog
            {
                DefaultExt = _defaultExtension,
                Filter = _filter
            };
            var result = fd.ShowDialog() ?? false;
            return result ? fd.FileName : null;
        }
    }
}
