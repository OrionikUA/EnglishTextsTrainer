using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Orionik.EnglishTextsTrainer.Helpers;
using Orionik.EnglishTextsTrainer.UIServices;
using Orionik.EnglishTextsTrainer.ViewModels.Command;

namespace Orionik.EnglishTextsTrainer.ViewModels
{
    public class ConnectViewModel : INotifyPropertyChanged
    {
        public ConnectViewModel()
        {
            ServerList = ConnectionStringHelper.Instance.Connection;
        }

        private string _selectedServer;
        public string SelectedServer
        {
            get { return _selectedServer; }
            set
            {
                _selectedServer = value;
                OnPropertyChanged();
            }
        }

        private List<string> _serverList;
        public List<string> ServerList
        {
            get { return _serverList; }
            set
            {
                _serverList = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand ConnectCommand
        {
            get
            {
                return new RelayCommand(p =>
                {
                    if (SelectedServer != null)
                    {
                        var conn = SelectedServer;
                        if (!string.IsNullOrEmpty(Password))
                        {
                            conn += Password;
                        }
                        WindowService.ShowWindow(new MainViewModel());
                        //TODO Connect
                    }
                });
            }
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
