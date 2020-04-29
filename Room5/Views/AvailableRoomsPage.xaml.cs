using System;
using System.Collections;
using Room5.ViewModels;
using Windows.UI.Popups;
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

        private async void RoomClicked(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            StackPanel panel = (StackPanel)btn.Content;
            UIElementCollection list = panel.Children;
            TextBlock tb = (TextBlock)list[0];
            String s1 = tb.Text;
            var dialog = new MessageDialog($"Sender: {s1}");
            await dialog.ShowAsync();
        }
    }
}
