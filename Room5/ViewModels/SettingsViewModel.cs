using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

using Room5.Helpers;
using Room5.Services;

using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Room5.ViewModels
{
    // WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/settings.md
    public class SettingsViewModel : Observable, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        public new void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private bool firstCall = true;
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;

        public ElementTheme ElementTheme
        {
            get { return _elementTheme; }

            set { Set(ref _elementTheme, value); }
        }

        private string _versionDescription;

        public string VersionDescription
        {
            get { return _versionDescription; }

            set { Set(ref _versionDescription, value); }
        }

        private ICommand _switchThemeCommand;

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            ElementTheme = param;
                            await ThemeSelectorService.SetThemeAsync(param);
                        });
                }

                return _switchThemeCommand;
            }
        }

        public SettingsViewModel()
        {
            if ((string)App.localSettings.Values["database"] == "sqlite")
            {
                IsBtnLocalDatabaseChecked = true;
                IsBtnMysqlDatabaseChecked = false;
            }
            else
            {
                IsBtnLocalDatabaseChecked = false;
                IsBtnMysqlDatabaseChecked = true;
            }
            
            firstCall = false;
        }

        public async Task InitializeAsync()
        {
            VersionDescription = GetVersionDescription();
           
            await Task.CompletedTask;
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        private bool _isBtnLocalDatabaseChecked = true;
        public bool IsBtnLocalDatabaseChecked
        {
            get => _isBtnLocalDatabaseChecked;
            set
            {
                _isBtnLocalDatabaseChecked = value;
                if (value == true)
                {
                    App.localSettings.Values["database"] = "sqlite";
                    ShowMysqlForm = false;
                    
                   
                }
               
                this.OnPropertyChanged();
            }
        }

        private bool _isBtnMysqlDatabaseChecked = true;
        public bool IsBtnMysqlDatabaseChecked
        {
            get => _isBtnMysqlDatabaseChecked;
            set
            {
                _isBtnMysqlDatabaseChecked = value;
                if (value == true)
                {
                    
                    App.localSettings.Values["database"] = "mysql";
                    readMysqlSettings();
                    ShowMysqlForm = true;
                    
                }
               
                this.OnPropertyChanged();
            }
        }

        private void readMysqlSettings()
        {
            Windows.Storage.ApplicationDataCompositeValue mysqlSettings;

            mysqlSettings = (Windows.Storage.ApplicationDataCompositeValue)App.localSettings.Values["mysqlSettings"];
            if (mysqlSettings == null)
            {
                Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();
                composite["server"] = string.Empty;
                composite["database"] = string.Empty;
                composite["user"] = string.Empty;
                composite["password"] = string.Empty;
                composite["port"] = "3307";

                App.localSettings.Values["mysqlSettings"] = composite;
                mysqlSettings = (Windows.Storage.ApplicationDataCompositeValue)App.localSettings.Values["mysqlSettings"];
            }

            MysqlServer = mysqlSettings["server"].ToString();
            MysqlDatabase = mysqlSettings["database"].ToString();
            MysqlUser = mysqlSettings["user"].ToString();
            MysqlPassword = mysqlSettings["password"].ToString();
            MysqlPort = mysqlSettings["port"].ToString();
        }

       




        public string MysqlServer { get => _mysqlServer;
            set {  _mysqlServer = value; OnPropertyChanged(); }
        }
        private string _mysqlServer;

        public string MysqlPort
        {
            get => _mysqlPort;
            set { _mysqlPort = value; OnPropertyChanged(); }
        }
        private string _mysqlPort;

        public string MysqlDatabase
        {
            get => _mysqlDatabase;
            set { _mysqlDatabase = value; OnPropertyChanged(); }
        }
        private string _mysqlDatabase;

        public string MysqlUser
        {
            get => _mysqlUser;
            set { _mysqlUser = value; OnPropertyChanged(); }
        }
        private string _mysqlUser;

        public string MysqlPassword
        {
            get => _mysqlPassword;
            set { _mysqlPassword = value; OnPropertyChanged(); }
        }

        private bool _showMysqlForm;
        public bool ShowMysqlForm { get => _showMysqlForm;
            set { _showMysqlForm = value; OnPropertyChanged(); }
        }

        private string _mysqlPassword;

        public async void BtnSaveMysqlClicked(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();
            composite["server"] = MysqlServer;
            composite["database"] = MysqlDatabase;
            composite["user"] = MysqlUser;
            composite["password"] = MysqlPassword;
            composite["port"] = MysqlPort;

            App.localSettings.Values["mysqlSettings"] = composite;
            ContentDialog dialog = new ContentDialog
            {
                Title = "Einstellungen gesichert",
                Content = "Bitte starten Sie das Programm neu, um die Änderung anzuwenden.",
                PrimaryButtonText = "OK"
            };

            ContentDialogResult result = await dialog.ShowAsync();

        }
        public void BtnCancelMysqlClicked(object sender, RoutedEventArgs e)
        {
            readMysqlSettings();
        }

      /*  public async void ShowRestartDialog()
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Datenbankänderung",
                Content = "Bitte starten Sie das Programm neu, um die Änderung anzuwenden.",
                PrimaryButtonText = "OK"
            };

            ContentDialogResult result = await dialog.ShowAsync();
        }*/
        public void DatabaseButtonClicked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Tag != null)
            {
                string database = rb.Tag.ToString();
                App.localSettings.Values["database"] = database;
            }
        }
        public async Task WriteTestDataAsync()
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Testdaten Anlegen",
                Content = "Alle vorhandenen Daten werden gelöscht",
                PrimaryButtonText = "OK",
                CloseButtonText = "Abbruch"
            };

            ContentDialogResult result = await dialog.ShowAsync();


            if (result == ContentDialogResult.Primary)
            {

                await App.Repository.Rooms.DeleteAllRoomsAsync();
                RoomsViewModel newRoom = new RoomsViewModel(new Models.Room());
                newRoom.RoomName = "Computerraum";
                await App.Repository.Rooms.UpsertAsync(newRoom.Model);
                Guid roomId = newRoom.Model.RoomId;
                DateTime FirstMonday = DateHelper.FirstMonday(DateTime.Now);
                DateTime nextMonday = FirstMonday.AddDays(7);

                BookingsViewModel newBooking = new BookingsViewModel(title: "Mo 1 weekly ",
                    day: 1,
                    lesson: 1,
                    startDate: FirstMonday,
                    roomId: newRoom.Model.RoomId,
                    repeat: (int)App.Repeat.Weekly,
                    model: new Models.Booking()
                    );
                await App.Repository.Bookings.UpsertAsync(newBooking.Model);

                BookingsViewModel monday2 = new BookingsViewModel(title: "Mo 2 einmalig ",
                    day: 1,
                    lesson: 2,
                    startDate: FirstMonday,
                    roomId: newRoom.Model.RoomId,
                    repeat: (int)App.Repeat.OneTime,
                    model: new Models.Booking()
                    );
                await App.Repository.Bookings.UpsertAsync(monday2.Model);

                BookingsViewModel monday3 = new BookingsViewModel(title: "Mo 3 einmalig ",
                   day: 1,
                   lesson: 3,
                   startDate: nextMonday,
                   roomId: newRoom.Model.RoomId,
                   repeat: (int)App.Repeat.OneTime,
                   model: new Models.Booking()
                   );
                await App.Repository.Bookings.UpsertAsync(monday3.Model);

            }



        }
    }
}
