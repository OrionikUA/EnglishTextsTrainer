using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        #region Constructors

        public MainViewModel()
        {
            IsOpenFileButtonEnabled = true;
            IsProjectMainPanelEnabled = false;
            ProjectTextWords = new ObservableCollection<string>();
            NewProjectWordToSave = new Word();
            _unitOfWork = new UnitOfWork(ConnectionStringHelper.Instance.Connection);
            DataBaseGrid = _unitOfWork.WordRepository.GetList();
            ProjectWordList = new List<Word>();
            IgnoreFilter = Filter.All;
            KnowFilter = Filter.All;
        }

        #endregion

        #region CommandProperties

        public ICommand OpenFileCommand
        {
            get { return new RelayCommand(param => { OpenFile(); }, o => IsOpenFileButtonEnabled); }
        }

        public ICommand SaveProjectWordCommand
        {
            get { return new RelayCommand(p => SaveProjectWord(), o => CanSaveProjectWord()); }
        }

        public ICommand DeleteIgnoredCommand
        {
            get
            {
                return new RelayCommand(p =>
                {
                    var ignored = (from word in DataBaseGrid where word.Ignore select word).ToList();
                    var deleteList = new List<string>();

                    foreach (var word in ProjectTextWords)
                    {
                        if (ignored.FirstOrDefault(x => x.Name == word) != null)
                        {
                            deleteList.Add(word);
                        }
                    }
                    foreach (var word in deleteList)
                    {
                        DeleteProjectSelectedWord(word);
                    }
                });
            }
        }

        public ICommand DeleteKnownCommand
        {
            get
            {
                return new RelayCommand(p =>
                {
                    var ignored = (from word in DataBaseGrid where word.Know select word).ToList();
                    var deleteList = new List<string>();
                    foreach (var word in ProjectTextWords)
                    {
                        if (ignored.FirstOrDefault(x => x.Name == word) != null)
                        {
                            deleteList.Add(word);
                        }
                    }
                    foreach (var word in deleteList)
                    {
                        DeleteProjectSelectedWord(word);
                    }
                });
            }
        }

        public ICommand ExportDataCommand
        {
            get { return new RelayCommand(p =>
            {
                SaveToFile(DataFilterGrid.ConvertAll(x => x.ToString()));
            });}
        }
        #endregion

        #region Fileds

        private readonly UnitOfWork _unitOfWork;
        private bool _isOpenFileButtonEnabled;
        private List<Word> _projectWordList;
        private string _filePath;
        private bool _isProjectMainPanelEnabled;
        private ObservableCollection<string> _projectTextWords;
        private Word _newProjectWordToSave;
        private List<Word> _dataBaseGrid;
        private bool _projectChecked;
        private Filter _ignoreFilter;
        private Filter _knowFilter;

        #endregion

        #region Properties

        public bool IsOpenFileButtonEnabled
        {
            get { return _isOpenFileButtonEnabled; }
            set
            {
                _isOpenFileButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        public List<Word> ProjectWordList
        {
            get { return _projectWordList; }
            set
            {
                _projectWordList = value;
                OnPropertyChanged();
            }
        }

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        public bool IsProjectMainPanelEnabled
        {
            get { return _isProjectMainPanelEnabled; }
            set
            {
                _isProjectMainPanelEnabled = value;
                OnPropertyChanged();
            }
        }

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

        public List<Word> DataBaseGrid
        {
            get { return _dataBaseGrid; }
            set
            {
                _dataBaseGrid = value;
                OnPropertyChanged();
            }
        }

        public List<Word> DataFilterGrid
            => CheckKnowFilter(ProjectChecked ? CheckIgnoreFilter(_projectWordList) : CheckIgnoreFilter(_dataBaseGrid));

        public bool ProjectChecked
        {
            get { return _projectChecked; }
            set
            {
                _projectChecked = value;
                OnPropertyChanged();
                OnPropertyChanged("DataFilterGrid");
            }
        }

        public Filter IgnoreFilter
        {
            get { return _ignoreFilter; }
            set
            {
                _ignoreFilter = value;
                OnPropertyChanged();
                OnPropertyChanged("DataFilterGrid");
            }
        }

        public Filter KnowFilter
        {
            get { return _knowFilter; }
            set
            {
                _knowFilter = value;
                OnPropertyChanged();
                OnPropertyChanged("DataFilterGrid");
            }
        }
        #endregion

        #region PrivateMethods

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
            var words = TextWords.GetUniqueWordList(TextWords.FindWords(text));
            foreach (var word in words)
            {
                ProjectTextWords.Add(word);
            }
            OnPropertyChanged("ProjectTextWords");
        }

        private void SaveToFile(List<string> list)
        {
            var dialog = new SaveFileDialogService(".txt", "Text documents (.txt)|*.txt");
            var filePath = dialog.CreateAndOpenFile();
            if (!string.IsNullOrEmpty(filePath))
            {
                TextFile.WriteToFile(list, filePath);
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
            if (!NewProjectWordToSave.Ignore && !NewProjectWordToSave.Know)
            {
                ProjectWordList.Add(DataBaseGrid.First(x => x.Name == NewProjectWordToSave.Name));
                OnPropertyChanged("ProjectWordList");
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

        private List<Word> CheckIgnoreFilter(List<Word> list)
        {
            switch (IgnoreFilter)
            {
                case Filter.True:
                    return list.FindAll(x => x.Ignore);
                case Filter.False:
                    return list.FindAll(x => !x.Ignore);
                default:
                    return list;
            }
        }

        private List<Word> CheckKnowFilter(List<Word> list)
        {
            switch (KnowFilter)
            {
                case Filter.True:
                    return list.FindAll(x => x.Know);
                case Filter.False:
                    return list.FindAll(x => !x.Know);
                default:
                    return list;
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
