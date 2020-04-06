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
            InitializeComponent();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
            var control = d as RoomsDetailControl;
            if (control.MasterMenuItem != null)
            {
                control.BookingRows.Clear();
               
                  IEnumerable<Booking> roomBookings;
                // Booking [] roomBookings;
                Booking booking;
                
                for (int i = 1;  i < 3; i++)
                {
                    BookingsRowModel r1 = new BookingsRowModel();
                    
                    r1.LessonNumber = i;
                    r1.Monday = new BookingsViewModel(title: "",day:1,lesson:i);
                    r1.Tuesday = new BookingsViewModel(title: "", day: 2, lesson: i);
                    r1.Wednesday = new BookingsViewModel(title: "", day: 3, lesson: i);
                    r1.Thursday = new BookingsViewModel(title: "", day: 4, lesson: i);
                    r1.Friday = new BookingsViewModel(title: "", day: 5, lesson: i);
                    r1.Saturday = new BookingsViewModel(title: "", day: 6, lesson: i);
                    r1.Sunday = new BookingsViewModel(title: "", day: 7, lesson: i);
                    // in Buchungsliste suchen

                    for(int i2 = 1; i2 < 8; i2++)
                    {
                        roomBookings = from b in control.MasterMenuItem.Bookings
                                       where b.Day == i2 && b.Lesson == i
                                       select b;
                        if(i2 == 1)
                        {
                            if (roomBookings.Count() > 0)
                            {
                                r1.Monday = new BookingsViewModel(model: roomBookings.First());
                            }
                          
                        }
                        else if (i2 == 2)
                        {
                            if (roomBookings.Count() > 0)
                            {
                                r1.Tuesday = new BookingsViewModel(model: roomBookings.First());
                            }
                          
                        }
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
                /*

                 */
                System.Diagnostics.Debug.WriteLine(cell.Value);
                BookingsRowModel bm = cell.Item as BookingsRowModel;
                var dialog = new MessageDialog(string.Format(cell.Column.Header.ToString()) + App.Weekdays[cell.Column.Header.ToString()] + " "   + bm.LessonNumber, "COLUMN HEADER: ");
                await dialog.ShowAsync();
            }
        }
    }
}
