﻿using System;

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

namespace Room5
{
    public sealed partial class App : Application
    {
        public static IRoom5Repository Repository { get; set; }


        
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
            MysqlDatabase();
            // SqliteDatabase();
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

        public static void MysqlDatabase()
        {
            //string connection = "server=192.168.178.31;user=root;password=Dyslexia - sibley - 1backer - Recopy5 - 3tassel - sheet7;port=3307";
            string connection = "server=DS218P;database=room;user=room;password=talky-polka-pause6-3selector-countable-Freebie9;port=3307";
            DbContextOptionsBuilder<Room5Context> dbOptions = new DbContextOptionsBuilder<Room5Context>().UseMySql(connection,
        mysqlOptions =>
        {
            /* mysqlOptions
                 .ServerVersion(new Version(5, 7, 17), ServerType.MySql);*/
        });
            Repository = new SQLRoom5Repository(dbOptions);
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
