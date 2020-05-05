using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Controls;

using Room5.Helpers;
using Room5.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
namespace Room5.ViewModels
{
    public class AvailableRoomsPageViewModel : INotifyPropertyChanged
    {
        // private MasterDetailsViewState viewState;
       

        public AvailableRoomsPageViewModel()
        {
            FirstMonday = DateHelper.FirstMonday(DateTime.Now);
            FirstWeek = DateHelper.getWeek(FirstMonday);
            CurrentWeek = DateHelper.getWeek(FirstMonday);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

      //  public ObservableCollection<BookingsViewModel> Bookings = new ObservableCollection<BookingsViewModel>();
        public ObservableCollection<BookingsViewModel> FutureBookings = new ObservableCollection<BookingsViewModel>();
        public ObservableCollection<AvailableRoomsRowModel> AvailableRoomsRows = new ObservableCollection<AvailableRoomsRowModel>();
        public IEnumerable<Room> Rooms;
        public IEnumerable<Booking> Bookings;

        private BookingsViewModel _selectedBooking;
        public DateTime FirstMonday;
        public Week FirstWeek;
        private Week _currentWeek;
        public Week CurrentWeek
        {
            get => _currentWeek;
            set
            {
                _currentWeek = value;
                //    return StartDate.ToString("D", App.culture) + "   -    " + Lesson + ". Stunde";

                CurrentWeekOutput = $"{CurrentWeek.Monday.ToString("m", App.culture)} bis {CurrentWeek.LastDayOfTheWeek.ToString("m", App.culture)}";

                if (CurrentWeek.Monday.Day == FirstMonday.Day)
                {
                    ShowPreviousButton = false;
                }
                else
                {
                    ShowPreviousButton = true;
                }
                OnPropertyChanged();
            }
        }

        private string _currentWeekOutput;
        public string CurrentWeekOutput
        {
            get => _currentWeekOutput;

            set
            {
                _currentWeekOutput = value;
                OnPropertyChanged();
            }
        }

        public BookingsViewModel SelectedBooking
        {
            get => _selectedBooking;
            set
            {
                _selectedBooking = value;
                OnPropertyChanged();
            }
        }

        public string RoomName
        {
            get => _roomName;
            set
            {
                _roomName = value;
                OnPropertyChanged();

            }
        }

        private string _roomName;
        private bool _showBookingForm = false;
        public bool ShowBookingForm
        {
            get => _showBookingForm;
            set
            {
                _showBookingForm = value;
                ShowMainContent = !value;
              
                OnPropertyChanged();
            }
        }

        private bool _showMainContent = true;
        public bool ShowMainContent
        {
            get => _showMainContent;
            set
            {
                _showMainContent = value;
                OnPropertyChanged();
            }
        }


        private bool _showPreviousButton = false;
        public bool ShowPreviousButton
        {
            get => _showPreviousButton;
            set
            {
                _showPreviousButton = value;
                OnPropertyChanged();
            }
        }

        private bool _showDeleteButton = false;
        public bool ShowDeleteButton
        {
            get => _showDeleteButton;
            set
            {
                _showDeleteButton = value;
                OnPropertyChanged();
            }
        }

        private bool _isWeeklyBookingAllowed = false;

        public bool IsWeeklyBookingAllowed
        {
            get => _isWeeklyBookingAllowed;
            set
            {
                _isWeeklyBookingAllowed = value;
                OnPropertyChanged();
            }
        }
        private bool _showFutureBookings = false;
        public bool ShowFutureBookings
        {
            get => _showFutureBookings;
            set
            {
                _showFutureBookings = value;
                OnPropertyChanged();
            }
        }

        private bool _isRadioButtonOneTimeChecked = true;
        public bool IsRadioButtonOneTimeChecked
        {
            get => _isRadioButtonOneTimeChecked;
            set
            {
                _isRadioButtonOneTimeChecked = value;
                OnPropertyChanged();
            }
        }
        private bool _isRadioButtonWeeklyChecked = true;
        public bool IsRadioButtonWeeklyChecked
        {
            get => _isRadioButtonWeeklyChecked;
            set
            {
                _isRadioButtonWeeklyChecked = value;
                OnPropertyChanged();
            }
        }


        public async Task BuildBookingRows()
        {
            DateTime currentDate = CurrentWeek.Monday;
            AvailableRoomsRows.Clear();
              Bookings = await App.Repository.Bookings.GetAsync();
              Rooms = await App.Repository.Rooms.GetAsync();

            
            for (int i = 1; i < 11; i++)
            {

                AvailableRoomsRowModel r1 = new AvailableRoomsRowModel();
                r1.LessonNumber = i;
                r1.Monday = buildList(lesson: i, day: 1, date: currentDate);
                r1.Tuesday = buildList(lesson: i, day: 2, date: currentDate.AddDays(1));
                r1.Wednesday = buildList(lesson: i, day: 3, date: currentDate.AddDays(2));
                r1.Thursday = buildList(lesson: i, day: 4, date: currentDate.AddDays(3));
                r1.Friday = buildList(lesson: i, day: 5, date: currentDate.AddDays(4));
                r1.Saturday = buildList(lesson: i, day: 6, date: currentDate.AddDays(5));
                r1.Sunday = buildList(lesson: i, day: 7, date: currentDate.AddDays(4));
                AvailableRoomsRows.Add(r1);
            }
            
        }

        public async void NextClicked(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            CurrentWeek = DateHelper.NextWeek(CurrentWeek.Monday);
            await BuildBookingRows();
        }

        public async void PreviousClicked(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            CurrentWeek = DateHelper.PreviousWeek(CurrentWeek.Monday);
            await BuildBookingRows();
        }

        public List<BookingsViewModel> buildList(int lesson, int day, DateTime date)
        {
            List<BookingsViewModel> list = new List<BookingsViewModel>();
            foreach (var room  in Rooms)
            {
                IEnumerable<Booking> bs = from b in Bookings
                                          where (b.Day == day
                                          && b.Lesson == lesson
                                          && b.Repeat != (int)App.Repeat.OneTime
                                          && b.RoomId == room.RoomId)
                 || (b.Day == day && b.Lesson == lesson
                 && b.Repeat == (int)App.Repeat.OneTime
                 && b.StartDate.ToShortDateString() == date.ToShortDateString()
                 && b.RoomId == room.RoomId)
                select b;

                if (bs.Count() == 0)
                {
                    BookingsViewModel bv = new BookingsViewModel(title: "", lesson: lesson, day: day, startDate: date, roomId: room.RoomId);
                    bv.RoomName = room.RoomName;
                    list.Add(bv);
                }
            }
            return list;
        }

        public void prepareForm(string roomId, int lesson, int day, string startDate, string roomName)
        {
            DateTime date = Convert.ToDateTime(startDate);
            SelectedBooking = new BookingsViewModel(day: day, lesson: lesson,startDate: date, roomId: Guid.Parse(roomId));
            SelectedBooking.RoomName = roomName;
            RoomName = roomName;

            FutureBookings.Clear();
            List<Booking> fb = App.Repository.Bookings.GetFutureBookings(SelectedBooking.Model);
            foreach (var item in fb)
            {
                FutureBookings.Add(new BookingsViewModel(item));
            }
            if (FutureBookings.Count > 0)
            {
                ShowFutureBookings = true;
                IsWeeklyBookingAllowed = false;
                IsRadioButtonOneTimeChecked = true;
            }
            else
            {
                ShowFutureBookings = false;
                IsWeeklyBookingAllowed = true;
                IsRadioButtonWeeklyChecked = true;
                IsRadioButtonOneTimeChecked = false;
            }
            ShowBookingForm = true;
           
        }

        public async void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(SelectedBooking.Title))
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Titel",
                    Content = "Bitte geben Sie der Buchung einen Titel",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await dialog.ShowAsync();

            }
            else
            {
                await App.Repository.Bookings.UpsertAsync(SelectedBooking.Model);
                await BuildBookingRows();
                ShowBookingForm = false;
            }

        }
        public void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            ShowBookingForm = false;
        }

        public void RepeatRadioButtonClicked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Tag != null)
            {
                string mode = rb.Tag.ToString();
                switch (mode)
                {
                    case "OneTime":
                        SelectedBooking.Repeat = (int)App.Repeat.OneTime;
                        break;
                    case "Weekly":
                        SelectedBooking.Repeat = (int)App.Repeat.Weekly;
                        break;
                    default:
                        SelectedBooking.Repeat = (int)App.Repeat.Weekly;
                        break;
                }
            }
        }
    }
}
