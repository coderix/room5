using System;
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
    public class SettingsViewModel : Observable
    {
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
