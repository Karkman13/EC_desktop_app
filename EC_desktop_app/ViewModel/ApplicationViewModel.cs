using EC_desktop_app.Model;
using EC_desktop_app.Services;
using EC_desktop_app.Services.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EC_desktop_app.ViewModel
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        private UserData selectedUserData;

        private int selectedUserDataIndex;

        private ICollection<int> _magicnumbers;

        public ICollection<int> MagicNumbers
        {
            get
            {
                return _magicnumbers;
            }
        }

        private ICollection<DayOfWeek> _dayofweeks;

        public ICollection<DayOfWeek> DayOfWeeks
        {
            get
            {
                return _dayofweeks;
            }
        }

        private readonly IFileService _fileService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<UserData> UserDatas { get; set; }

        public UserData SelectedUserData
        {
            get
            {
                return selectedUserData;
            }
            set
            {
                selectedUserData = value;
                OnPropertyChanged("SelectedUserData");
            }
        }

        private string _viewUserText;

        public string ViewUserText
        {
            get
            {
                return _viewUserText;
            }
            set
            {
                if (_viewUserText == value) return;
                _viewUserText = value;
                SelectedUserData.UserText = value;
                OnPropertyChanged("ViewUserText");
                OnPropertyChanged("SelectedUserData");
            }
        }

        public int SelectedUserDataIndex
        {
            get
            {
                return selectedUserDataIndex;
            }
            set
            {
                selectedUserDataIndex = value;
            }
        }

        private Command _saveCommand;

        public Command SaveCommand
        {
            get
            {
                return _saveCommand ??
                    (_saveCommand = new Command(obj =>
                    {
                        try
                        {
                            if (_dialogService.SaveFileDialog() == true)
                            {
                                _fileService.Save(_dialogService.FilePath, UserDatas.ToList());
                                _dialogService.ShowMessage("File Save");
                            }
                        }
                        catch (Exception ex)
                        {
                            _dialogService.ShowMessage(ex.Message);
                        }
                    }));
            }
        }

        private Command _openCommand;

        public Command OpenCommand
        {
            get
            {
                return _openCommand ??
                    (_openCommand = new Command(obj =>
                    {
                        try
                        {
                            if (_dialogService.OpenFileDialog() == true)
                            {
                                var datas = _fileService.Open(_dialogService.FilePath);
                                UserDatas.Clear();
                                foreach (var userdata in datas)
                                {
                                    UserDatas.Add(userdata);
                                }

                                _dialogService.ShowMessage("FileOpen");
                            }
                        }
                        catch (Exception ex)
                        {
                            _dialogService.ShowMessage(ex.Message);
                        }
                    }));
            }
        }

        private Command _addCommand;

        public Command AddCommand
        {
            get
            {
                return _addCommand ??
                    (_addCommand = new Command(obj =>
                    {
                        if(UserDatas.Count == 0)
                        {
                            if(SelectedUserData.MagicNumber == 0)
                            {
                                _dialogService.ShowMessage("Error! Please select magic number");
                                return;
                            }
                            UserDatas.Insert(0, SelectedUserData);
                        }
                        else
                        {
                            var data = new UserData();
                            UserDatas.Insert(0, data);
                            SelectedUserData = data;
                        }
                    }));
            }
        }

        private Command _removeCommand;

        public Command RemoveCommand
        {
            get
            {
                return _removeCommand ??
                    (_removeCommand = new Command(obj =>
                    {
                        var data = obj as UserData;
                        var selectedindex = selectedUserDataIndex;
                        if (data != null)
                        {
                            UserDatas.Remove(data);
                        }
                        UpdateUserDatasListSelection(UserDatas.Count, selectedindex);
                    },
                    (obj) => UserDatas.Count > 0 && selectedUserDataIndex >= 0));
            }
        }

        private void UpdateUserDatasListSelection(int userdatasCount, int selectedIndex)
        {
            if(userdatasCount == 0)
            {
                SelectedUserData = null;
            }
            else if(userdatasCount <= selectedIndex)
            {
                SelectedUserData = UserDatas[userdatasCount - 1];
            }
            else
            {
                SelectedUserData = UserDatas[selectedIndex];
            }
        }
        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this._dialogService = dialogService;
            this._fileService = fileService;
            this._magicnumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            this._dayofweeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };

            UserDatas = new ObservableCollection<UserData>();
            SelectedUserData = new UserData();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
