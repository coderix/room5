using System;

using Room5.Services;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.Storage;
using Microsoft.EntityFrameworkCore;
using Room5.Repository;
using Room5.Models;
using Windows.ApplicationModel;

namespace Room5
{
    public sealed partial class App : Application
    {
        public static IRoom5Repository Repository { get; set; }

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
            SqliteDatabase();
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
           
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.RoomsPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }

        public static void SqliteDatabase()
        {
            // string demoDatabasePath = Package.Current.InstalledLocation.Path + @"\Assets\Repository.db";
            string databasePath = ApplicationData.Current.LocalFolder.Path + @"\Room5.db";
            /* if (!File.Exists(databasePath))
             {
                 File.Copy(demoDatabasePath, databasePath);
             }*/
            DbContextOptionsBuilder<Room5Context> dbOptions = new DbContextOptionsBuilder<Room5Context>().UseSqlite("Data Source=" + databasePath);
            Repository = new SQLRoom5Repository(dbOptions);
        }
    }
}
