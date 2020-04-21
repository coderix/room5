using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Room5.Core.Models;
using Room5.ViewModels;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Linq;
using Windows.UI.Popups;
using Room5.Models;
using Room5.Helpers;

namespace Room5.Views
{
    public sealed partial class RoomsDetailControl : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<BookingsViewModel> Bookings = new ObservableCollection<BookingsViewModel>();
        public ObservableCollection<BookingsRowModel> BookingRows = new ObservableCollection<BookingsRowModel>();
        private BookingsViewModel _selectedBooking;
        // public DateHelper DH = new DateHelper();
        public DateTime FirstMonday;
        public Week FirstWeek;
        private Week _currentWeek;
        public Week CurrentWeek
        {
            get => _currentWeek;
            set
            {
                _currentWeek = value;
               CurrentWeekOutput = $"{CurrentWeek.Monday.Day}.{CurrentWeek.Monday.Month}. - {CurrentWeek.Sunday.Day}.{CurrentWeek.Sunday.Month}.";

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

        public RoomsViewModel MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as RoomsViewModel; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(RoomsViewModel), typeof(RoomsDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public RoomsDetailControl()
        {
            InitializeComponent();
            FirstMonday = DateHelper.FirstMonday(DateTime.Now);
            FirstWeek = DateHelper.getWeek(FirstMonday);
            CurrentWeek = DateHelper.getWeek(FirstMonday);
         //   SelectedBooking = new BookingsViewModel();
         }

        private static RoomsDetailControl control;

        private static void buildBookingRows()
        {
            if (control.MasterMenuItem != null)
            {
                
                control.BookingRows.Clear();
                IEnumerable<Booking> bookings = App.Repository.Bookings.Get(control.MasterMenuItem.Model);

                IEnumerable<Booking> roomBookings;
                // Booking [] roomBookings;
                //  Booking booking;

                for (int i = 1; i < 11; i++)
                {   
                    BookingsRowModel r1 = new BookingsRowModel();

                    r1.LessonNumber = i;
                    r1.Monday = new BookingsViewModel(title: "", day: 1, lesson: i, roomId: control.MasterMenuItem.Model.RoomId,
                        startDate: control.CurrentWeek.Monday,
                        endDate: control.CurrentWeek.Monday
                    );
                    r1.Tuesday = new BookingsViewModel(title: "", day: 2, lesson: i, roomId: control.MasterMenuItem.Model.RoomId,
                        startDate: control.CurrentWeek.Tuesday,
                         endDate: control.CurrentWeek.Tuesday
                        );
                    r1.Wednesday = new BookingsViewModel(title: "", day: 3, lesson: i, roomId: control.MasterMenuItem.Model.RoomId,
                        startDate: control.CurrentWeek.Wednesday,
                         endDate: control.CurrentWeek.Wednesday
                        );
                    r1.Thursday = new BookingsViewModel(title: "", day: 4, lesson: i, roomId: control.MasterMenuItem.Model.RoomId,
                        startDate: control.CurrentWeek.Thursday,
                         endDate: control.CurrentWeek.Thursday
                    );
                    r1.Friday = new BookingsViewModel(title: "", day: 5, lesson: i, roomId: control.MasterMenuItem.Model.RoomId,
                        startDate: control.CurrentWeek.Friday,
                         endDate: control.CurrentWeek.Friday);
                    r1.Saturday = new BookingsViewModel(title: "", day: 6, lesson: i, roomId: control.MasterMenuItem.Model.RoomId,
                        startDate: control.CurrentWeek.Saturday,
                         endDate: control.CurrentWeek.Saturday
                        );
                    r1.Sunday = new BookingsViewModel(title: "", day: 7, lesson: i, roomId: control.MasterMenuItem.Model.RoomId,
                        startDate: control.CurrentWeek.Sunday,
                         endDate: control.CurrentWeek.Sunday
                        );
                    // in Buchungsliste suchen

                    for (int i2 = 1; i2 < 8; i2++)
                    {
                        roomBookings = from b in bookings
                                       where b.Day == i2 && b.Lesson == i
                                       select b;
                        if (roomBookings.Count() > 0)
                        {
                            switch (i2)
                            {
                                case 1: r1.Monday = new BookingsViewModel(model: roomBookings.First()); break;
                                case 2: r1.Tuesday = new BookingsViewModel(model: roomBookings.First());break;
                                case 3: r1.Wednesday = new BookingsViewModel(model: roomBookings.First()); break;
                                case 4: r1.Thursday = new BookingsViewModel(model: roomBookings.First()); break;
                                case 5: r1.Friday = new BookingsViewModel(model: roomBookings.First()); break;
                                case 6: r1.Saturday = new BookingsViewModel(model: roomBookings.First()); break;
                                case 7: r1.Sunday = new BookingsViewModel(model: roomBookings.First()); break;

                                default:
                                    break;
                            }
                        }
                    }

                    control.BookingRows.Add(r1);
                }
            }
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
            control = d as RoomsDetailControl;
            buildBookingRows();
            
            control.ForegroundElement.ChangeView(0, 0, 1);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

      
       

        private   void GridTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var physicalPoint = e.GetPosition(sender as RadDataGrid);
            var cell = (sender as RadDataGrid).HitTestService.CellInfoFromPoint(physicalPoint);

            if (cell != null)
            {
                /*
                 * Set a reference to the selected Booking

                 */
                  
              //  System.Diagnostics.Debug.WriteLine(cell.Value);
                BookingsRowModel bm = cell.Item as BookingsRowModel;
                string columnHeader = string.Format(cell.Column.Header.ToString());
                int lessonNumber = bm.LessonNumber;
                switch (columnHeader)
                {
                    case "Montag":
                        SelectedBooking = bm.Monday;
                        break;
                    case "Dienstag":
                        SelectedBooking = bm.Tuesday;
                        break;
                    case "Mittwoch":
                        SelectedBooking = bm.Wednesday;
                        break;
                    case "Donnerstag":
                        SelectedBooking = bm.Thursday;
                        break;
                    case "Freitag":
                        SelectedBooking = bm.Friday;
                        break;
                    case "Samstag":
                        SelectedBooking = bm.Saturday;
                        break;
                    case "Sonntag":
                        SelectedBooking = bm.Sunday;
                        break;
                    default:
                        break;
                }
                if (SelectedBooking.Model.Title == "")
                {
                    ShowDeleteButton = false;
                }
                else
                {
                    ShowDeleteButton = true;
                }

                if (SelectedBooking.Repeat == (int)App.Repeat.Weekly)
                {
                    IsRadioButtonOneTimeChecked = false;
                    IsRadioButtonWeeklyChecked = true;
                }
                else
                {
                    IsRadioButtonOneTimeChecked = true;
                    IsRadioButtonWeeklyChecked = false;
                }
                ShowBookingForm = true;
               /* var dialog = new MessageDialog(string.Format(cell.Column.Header.ToString()) + App.Weekdays[cell.Column.Header.ToString()] + " "   + bm.LessonNumber, "COLUMN HEADER: ");
                await dialog.ShowAsync();*/

               /* var dialog = new MessageDialog($"Info: {SelectedBooking.Info}");
                await dialog.ShowAsync();*/
            }
        }

        private async void SaveButtonClicked(object sender, RoutedEventArgs e)
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
                buildBookingRows();
                ShowBookingForm = false;
            }
            
        }
        private async void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Soll die Buchung gelöscht werden?",
                Content = "Sie kann nicht wiederhergestellt werden",
                PrimaryButtonText = "Löschen",
                CloseButtonText = "Abbruch"
            };

            ContentDialogResult result = await dialog.ShowAsync();


            if (result == ContentDialogResult.Primary && SelectedBooking != null)
            {
                await App.Repository.Bookings.DeleteAsync(SelectedBooking.Model.BookingId);
                buildBookingRows();
            }
              
            ShowBookingForm = false;
        }
        public void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            ShowBookingForm = false;
        }

       
        private  void NextClicked(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            CurrentWeek = DateHelper.NextWeek(CurrentWeek.Monday);
            buildBookingRows();
        }

        private void PreviousClicked(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            CurrentWeek = DateHelper.PreviousWeek(CurrentWeek.Monday);
            buildBookingRows();
        }

        private void RepeatRadioButtonClicked(object sender, RoutedEventArgs e)
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
