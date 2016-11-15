using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Orionik.EnglishTextsTrainer.Helpers;
using Orionik.EnglishTextsTrainer.Logic;
using Orionik.EnglishTextsTrainer.Models;
using Orionik.EnglishTextsTrainer.Repositories;
using Orionik.EnglishTextsTrainer.UIServices;
using Orionik.EnglishTextsTrainer.ViewModels.Command;

namespace Orionik.EnglishTextsTrainer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly UnitOfWork _unitOfWork;

        public MainViewModel()
        {
            IsOpenFileButtonEnabled = true;
            IsProjectMainPanelEnabled = false;
            ProjectTextWords = new ObservableCollection<string>();
            NewProjectWordToSave = new Word();
            _unitOfWork = new UnitOfWork(ConnectionStringHelper.Instance.Connection);
            DataBaseGrid = _unitOfWork.WordRepository.GetList();

            
        }
        public ICommand OpenFileCommand
        {
            get { return new RelayCommand(param => { OpenFile(); }, o => IsOpenFileButtonEnabled); }
        }

        private bool _isOpenFileButtonEnabled;
        public bool IsOpenFileButtonEnabled
        {
            get { return _isOpenFileButtonEnabled; }
            set
            {
                _isOpenFileButtonEnabled = value;
                OnPropertyChanged();
            }

        }

        private void OpenFile()
        {
            var dialog = new OpenFileDialogService(".txt", "Text documents (.txt)|*.txt");
            FilePath = dialog.OpenFile();
            if (!string.IsNullOrEmpty(FilePath))
            {
                IsOpenFileButtonEnabled = false;
                IsProjectMainPanelEnabled = true;
            }

            var text = TextFile.ReadFile(FilePath);
            var words = TextWords.FindWords(text);
            foreach (var word in words)
            {
                ProjectTextWords.Add(word);
            }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        private bool _isProjectMainPanelEnabled;
        public bool IsProjectMainPanelEnabled
        {
            get { return _isProjectMainPanelEnabled; }
            set
            {
                _isProjectMainPanelEnabled = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _projectTextWords;
        public ObservableCollection<string> ProjectTextWords
        {
            get { return _projectTextWords; }
            set
            {
                _projectTextWords = value;
                OnPropertyChanged();
            }
        }

        public string SelectedTextWord
        {
            get { return _newProjectWordToSave.Name; }
            set
            {
                Word dataWord = DataBaseGrid.FirstOrDefault(x => x.Name == value);
                if (dataWord != null)
                {
                    _newProjectWordToSave.Name = dataWord.Name;
                    _newProjectWordToSave.Id = dataWord.Id;
                    NewProjectWordToSave = dataWord;
                }
                else
                {
                    NewProjectWordToSave = new Word();
                    _newProjectWordToSave.Name = value;
                }
                OnPropertyChanged();
            }
        }

        private Word _newProjectWordToSave;
        public Word NewProjectWordToSave
        {
            get { return _newProjectWordToSave; }
            set
            {
                _newProjectWordToSave = value;
                OnPropertyChanged();
                OnPropertyChanged("NewProjectWordToSaveMeanings");
                OnPropertyChanged("NewProjectWordToSaveIgnore");
                OnPropertyChanged("NewProjectWordToSaveKnow");
            }
        }

        public string NewProjectWordToSaveMeanings
        {
            get { return _newProjectWordToSave.Meanings; }
            set
            {
                _newProjectWordToSave.Meanings = value;
                OnPropertyChanged();
            }
        }

        public bool NewProjectWordToSaveIgnore
        {
            get { return _newProjectWordToSave.Ignore; }
            set
            {
                _newProjectWordToSave.Ignore = value;
                OnPropertyChanged();
            }
        }

        public bool NewProjectWordToSaveKnow
        {
            get { return _newProjectWordToSave.Know; }
            set
            {
                _newProjectWordToSave.Know = value;
                OnPropertyChanged();
            }
        }

        private List<Word> _dataBaseGrid;
        public List<Word> DataBaseGrid
        {
            get { return _dataBaseGrid; }
            set
            {
                _dataBaseGrid = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveProjectWordCommand
        {
            get
            {
                return new RelayCommand(p => SaveProjectWord(), o => CanSaveProjectWord());
            }
        }

        private void SaveProjectWord()
        {
            Word dataWord = DataBaseGrid.FirstOrDefault(x => x.Name == NewProjectWordToSave.Name);
            if (dataWord != null)
            {
                _unitOfWork.WordRepository.Update(NewProjectWordToSave);
                DataBaseGrid[DataBaseGrid.IndexOf(dataWord)] = NewProjectWordToSave;
            }
            else
            {
                _unitOfWork.WordRepository.Insert(NewProjectWordToSave);
                DataBaseGrid.Add(NewProjectWordToSave);
            }

            var selected = SelectedTextWord;
            CleareSaveProjectWord();
            DeleteProjectSelectedWord(selected);
            
        }

        private void DeleteProjectSelectedWord(string selected)
        {
            ProjectTextWords.Remove(selected);
            OnPropertyChanged("ProjectTextWords");
        }

        private void CleareSaveProjectWord()
        {
            SelectedTextWord = string.Empty;
            NewProjectWordToSave = new Word();
        }

        private bool CanSaveProjectWord()
        {
            return !string.IsNullOrEmpty(NewProjectWordToSave.Name) &&
                   !string.IsNullOrEmpty(NewProjectWordToSave.Meanings);
        }

        //public object DeleteIgnoredCommand
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public object DeleteKnownCommand
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public object SavaAllNotKnownThatIsInBaseCommand
        //{
        //    get { throw new NotImplementedException(); }
        //}



        //public object SelectedTextWord
        //{
        //    get { throw new NotImplementedException(); }
        //}



        //public object NewProjectWordToSave
        //{
        //    get { throw new NotImplementedException(); }
        //}



        //public object IsDataAddPanelEnabled
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public object NewDataWordToSave
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public object SaveDataWordCommand
        //{
        //    get { throw new NotImplementedException(); }
        //}



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
