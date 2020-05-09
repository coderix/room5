using System;

using Room5.Services;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.Storage;
using Microsoft.EntityFrameworkCore;
using Room5.Repository;
using Room5.Models;
using Windows.ApplicationModel;
using System.Collections.Generic;
using System.Globalization;
using Pomelo.EntityFrameworkCore.MySql;
using Windows.UI.Xaml.Controls;

namespace Room5
{
    public sealed partial class App : Application
    {
        public static IRoom5Repository Repository { get; set; }

       public static Windows.Storage.ApplicationDataContainer localSettings =  Windows.Storage.ApplicationData.Current.LocalSettings;

        public static Windows.Storage.ApplicationDataCompositeValue mysqlSettings =  new Windows.Storage.ApplicationDataCompositeValue();

        public static IDictionary<string, int> Weekdays = new Dictionary<string, int>()
        {
            {"Montag", 1 },
            {"Dienstag",2 },
            {"Mittwoch", 3},
            {"Donnerstag",4 },
            {"Freitag",5 },
            {"Samstag",6 },
            {"Sonntag",7 }
        };

        public enum Repeat
        {
            OneTime,
            Weekly,
            Biweekly,
            Monthly
        }

        public static CultureInfo culture = CultureInfo.CreateSpecificCulture("de-DE");
        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();
           

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
          //  localSettings.Values["database"] = "mysql";
            var database = (string)localSettings.Values["database"];
            if (database == "mysql")
            {
                try
                {
                    MysqlDatabase();
                }
                catch (Exception e)
                {
                    ShowErrorDialog(e);
                    localSettings.Values["database"] = "sqlite";
                    SqliteDatabase();
                }
               
            }
            else
            {
                SqliteDatabase();
            }
            
            //
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
           
        }

        public async void ShowErrorDialog(Exception e)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Fehler beim Datenbankzugriff",
                Content = "Bitte kontrollieren Sie die Einstellungen" + Environment.NewLine + e.ToString(),
                PrimaryButtonText = "OK"
            };

            ContentDialogResult result = await dialog.ShowAsync();
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.SettingsPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }

        public static void MysqlDatabase()
        {
            string connection;
           // connection = "server=DS218P;database=room;user=room;password=talky-polka-pause6-3selector-countable-Freebie9;port=3307";
            Windows.Storage.ApplicationDataCompositeValue composite = (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values["mysqlSettings"];
            if (composite == null)
            {
                SqliteDatabase();
            }
            else
            {
                connection = "server=" + composite["server"].ToString() + ";" 
                    + "database=" + composite["database"].ToString() + ";"
                    + "user=" + composite["user"].ToString() + ";"
                    + "password=" + composite["password"].ToString() + ";"
                    + "port=" + composite["port"].ToString() + ";"

                    ;

                DbContextOptionsBuilder<Room5Context> dbOptions = new DbContextOptionsBuilder<Room5Context>().UseMySql(connection,
                     mysqlOptions =>  { });

                Repository = new SQLRoom5Repository(dbOptions);
            }
           
        }



        public static void SqliteDatabase()
        {
            // string demoDatabasePath = Package.Current.InstalledLocation.Path + @"\Assets\Repository.db";
            string databasePath = ApplicationData.Current.LocalFolder.Path + @"\Room5.db";
         //funktioniert frühestens mit ef core 3.1.4
         //   string databasePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\room5\Room5.db";
            /* if (!File.Exists(databasePath))
             {
                 File.Copy(demoDatabasePath, databasePath);
             }*/
            DbContextOptionsBuilder<Room5Context> dbOptions = new DbContextOptionsBuilder<Room5Context>().UseSqlite("Data Source=" + databasePath);
            Repository = new SQLRoom5Repository(dbOptions);
        }
    }
}
