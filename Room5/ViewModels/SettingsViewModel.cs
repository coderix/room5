using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Room5.Helpers;
using Room5.Repository;
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
                ShowMysqlForm = false;
                ShowSqliteForm = true;
                ShowBtnActivateSqlite = false;
            }
            else
            {
                IsBtnLocalDatabaseChecked = false;
                IsBtnMysqlDatabaseChecked = true;
                ShowMysqlForm = true;
                ShowSqliteForm = false;
                ShowBtnActivateSqlite = true;
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
                   // App.localSettings.Values["database"] = "sqlite";
                    ShowMysqlForm = false;
                    ShowSqliteForm = true;
                    var database = (string)App.localSettings.Values["database"];
                    if (database == "sqlite")
                    {
                        ShowBtnActivateSqlite = false;
                        SqliteMessage = "Lokale Datenbank ist aktiviert";
                    }
                    else
                    {
                        ShowBtnActivateSqlite = true;
                        SqliteMessage = "Lokale Datenbank ist nicht aktiviert";
                    }
                }
               
                this.OnPropertyChanged();
            }
        }

        private string _sqliteMessage;
        public string SqliteMessage
        {
            get
            {
                return _sqliteMessage;
            }
            set
            {
                _sqliteMessage = value;
                OnPropertyChanged();
            }
        }

        public async void  BtnActivateSqliteClicked(object sender, RoutedEventArgs e) {
            App.localSettings.Values["database"] = "sqlite";
            App.DbOptions = new DbContextOptionsBuilder<Room5Context>().UseSqlite("Data Source=" + App.DatabasePath);
            App.Repository = new SQLRoom5Repository(App.DbOptions);
            ShowBtnActivateSqlite = false;
            SqliteMessage = "Lokale Datenbank ist aktiviert";
            ContentDialog dialog = new ContentDialog
            {
                Title = "Lokale Datenbank OK",
                Content = "Die lokale Datenbank ist aktiviert.",
                PrimaryButtonText = "OK"
            };

            ContentDialogResult result = await dialog.ShowAsync();
            OnPropertyChanged();
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
                    
                 
                    readMysqlSettings();
                    ShowMysqlForm = true;
                    ShowSqliteForm = false;

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

        private bool _showSqliteForm;
        public bool ShowSqliteForm
        {
            get => _showSqliteForm;
            set { _showSqliteForm = value; OnPropertyChanged(); }
        }

        private bool _showBtnActivateSqlite;
        public bool ShowBtnActivateSqlite
        {
            get => _showBtnActivateSqlite;
            set { _showBtnActivateSqlite = value; OnPropertyChanged(); }
        }

        private bool _showMysqlForm;
        public bool ShowMysqlForm { get => _showMysqlForm;
            set { _showMysqlForm = value; OnPropertyChanged(); }
        }

        private string _mysqlPassword;

        public async void BtnSaveMysqlClicked(object sender, RoutedEventArgs e)
        {
            string connection = "server=" + MysqlServer  + ";database=" + MysqlDatabase + ";user=" + MysqlUser + ";password=" + MysqlPassword + ";port=" + MysqlPort;

            try
            {
                App.DbOptions = new DbContextOptionsBuilder<Room5Context>().UseMySql(connection,
                     mysqlOptions => { }); 
                App.Repository = new SQLRoom5Repository(App.DbOptions);
            }
            catch (MySql.Data.MySqlClient.MySqlException  ex)
            {
               // string innerMessage = ex.InnerException.Message;
                string msg = ex.Message;
                ShowMysqlErrorDialog(msg);
                return;

            }
            catch (Exception ex)
            {
               
                ShowMysqlErrorDialog(ex.Message + ex.ToString());
                return;

            }
            Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();
            composite["server"] = MysqlServer;
            composite["database"] = MysqlDatabase;
            composite["user"] = MysqlUser;
            composite["password"] = MysqlPassword;
            composite["port"] = MysqlPort;

            App.localSettings.Values["mysqlSettings"] = composite;
            App.localSettings.Values["database"] = "mysql";
            ShowBtnActivateSqlite = true;
            ContentDialog dialog = new ContentDialog
            {
                Title = "Mysql OK",
                Content = "Die MySql-Datenbank ist jetzt aktiviert.",
                PrimaryButtonText = "OK"
            };

            ContentDialogResult result = await dialog.ShowAsync();

        }

        public async void ShowMysqlErrorDialog(String msg)
        {
            string title;
            if (msg.Contains("Unable to connect to any"))
            {
                title = "Keine Verbindung zum Datenbankserver";
                msg = "Mögliche Ursachen: keine Netzverbindung zum Server (funktioniert das Netzwerk?, läuft der Server?), falscher Servername oder falscher Port";
            }
            else
            {
                title = "Keine Verbindung zur Datenbank";
                msg = "Bitte überprüfen Sie Datenbankname, Benutzername und Passwort";
            }
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = msg ,
                PrimaryButtonText = "OK"
            };

            // ContentDialogResult result =
                await dialog.ShowAsync();

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
