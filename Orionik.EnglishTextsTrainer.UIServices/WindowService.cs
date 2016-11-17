using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Orionik.EnglishTextsTrainer.UIServices
{
    public static class WindowService
    {
        public static void ShowWindow(object viewModel)
        {
            Window win = new Window();
            win.Content = viewModel;
            win.Show();
        }
    }
}
