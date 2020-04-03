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

namespace Room5.Views
{
    public sealed partial class RoomsDetailControl : UserControl, INotifyPropertyChanged
    {
        /* private ObservableCollection<BookingsViewModel> _bookings = new ObservableCollection<BookingsViewModel>();
         public ObservableCollection<BookingsViewModel> Bookings { get => _bookings; }*/

       //   private <BookingsViewModel> _bookings = new ObservableCollection<BookingsViewModel>();
       // public List<BookingsViewModel> Bookings = new List<BookingsViewModel>();
        public ObservableCollection<BookingsViewModel> Bookings = new ObservableCollection<BookingsViewModel>();
        public ObservableCollection<BookingsRowModel> BookingRows = new ObservableCollection<BookingsRowModel>();
        public RoomsViewModel MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as RoomsViewModel; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(RoomsViewModel), typeof(RoomsDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public RoomsDetailControl()
        {
           
            
           


            /*for (int i = 0; i < 5; i++)
            {
                Bookings.Add(new BookingsViewModel("a" + i));
            }
            for (int i = 0; i < 5; i++)
            {
                Bookings.Add(new BookingsViewModel("b" + i));
            }*/
           


            InitializeComponent();
            
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
            var control = d as RoomsDetailControl;
            if (control.MasterMenuItem != null)
            {
                control.BookingRows.Clear();
                // List<Booking> roomBookings = control.MasterMenuItem.Bookings;
                // List<Booking> roomBookings;
                  IEnumerable<Booking> roomBookings;
                // Booking [] roomBookings;
                Booking booking;
                
                for (int i = 1;  i < 3; i++)
                {
                    BookingsRowModel r1 = new BookingsRowModel();
                    
                    r1.LessonNumber = i;
                    r1.Monday = new BookingsViewModel("1a");
                    r1.Tuesday = new BookingsViewModel("1a");
                    r1.Wednesday = new BookingsViewModel("1a");
                    r1.Thursday = new BookingsViewModel("1a");
                    r1.Friday = new BookingsViewModel("1a");
                    r1.Saturday = new BookingsViewModel("1a");
                    r1.Sunday = new BookingsViewModel("1a");
                    // in Buchungsliste suchen
                    roomBookings = from b in control.MasterMenuItem.Bookings
                                   where b.Day == 1 && b.Lesson == i
                                   select b;
                    if (roomBookings.Count() > 0)
                    {
                        r1.Monday = new BookingsViewModel(model: roomBookings.First());
                    }else
                    {
                        r1.Monday = new BookingsViewModel(title: "");
                    }
                    roomBookings = from b in control.MasterMenuItem.Bookings
                                   where b.Day == 2 && b.Lesson == i
                                   select b;
                    if (roomBookings.Count() > 0)
                    {
                        r1.Tuesday = new BookingsViewModel(model: roomBookings.First());
                    }
                    else
                    {
                        r1.Tuesday = new BookingsViewModel(title: "");
                    }

                   
                    control.BookingRows.Add(r1);
                }
                
                
            }
            control.ForegroundElement.ChangeView(0, 0, 1);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

      
       

        private  async void GridTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var physicalPoint = e.GetPosition(sender as RadDataGrid);
            var cell = (sender as RadDataGrid).HitTestService.CellInfoFromPoint(physicalPoint);

            if (cell != null)
            {
                System.Diagnostics.Debug.WriteLine(cell.Value);
                var dialog = new MessageDialog(string.Format(cell.Column.Header.ToString()) + cell.Item, "COLUMN HEADER: ");
                await dialog.ShowAsync();
            }
        }
    }
}
