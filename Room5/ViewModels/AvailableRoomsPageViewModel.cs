using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Controls;

using Room5.Helpers;
using Room5.Models;
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

        public ObservableCollection<BookingsViewModel> Bookings = new ObservableCollection<BookingsViewModel>();
        public ObservableCollection<BookingsViewModel> FutureBookings = new ObservableCollection<BookingsViewModel>();
        public ObservableCollection<AvailableRoomsRowModel> AvailableRoomsRows = new ObservableCollection<AvailableRoomsRowModel>();
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
        private bool _showBookingForm = false;
        public bool ShowBookingForm
        {
            get => _showBookingForm;
            set
            {
                _showBookingForm = value;
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
            IEnumerable<Booking> bookings = await App.Repository.Bookings.GetAsync();
            for (int i = 1; i < 11; i++)
            {
                AvailableRoomsRowModel r1 = new AvailableRoomsRowModel();
                r1.LessonNumber = i;
                r1.Monday = new List<BookingsViewModel>();
                r1.Monday.Add(new BookingsViewModel(title: "Montag 1"));
                r1.Monday.Add(new BookingsViewModel(title: "Montag 2"));
                AvailableRoomsRows.Add(r1);
            }
            
        }
    }
}
