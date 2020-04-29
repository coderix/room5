using System;
using Room5.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Room5.Views
{
   
    public sealed partial class AvailableRoomsPage : Page
    {
        public AvailableRoomsPageViewModel ViewModel { get; set; } =
           new AvailableRoomsPageViewModel();

        private async void AvailableRoomsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.BuildBookingRows();
        }
        public AvailableRoomsPage()
        {
            this.InitializeComponent();
            DataContext = ViewModel;
            Loaded += AvailableRoomsPage_Loaded;
        }
    }
}
