using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Orionik.EnglishTextsTrainer.Views;

namespace Orionik.EnglishTextsTrainer
{
    class Startup
    {
        [STAThread]
        static void Main(string[] args)
        {
            App app = new App();
            MainView mainView = new MainView();
            app.Run(mainView);
        }
    }
}
