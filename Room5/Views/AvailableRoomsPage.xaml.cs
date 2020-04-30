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

        private  void RoomClicked(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            StackPanel panel = (StackPanel)btn.Content;
            UIElementCollection list = panel.Children;
            TextBlock tb0 = (TextBlock)list[0];
            String roomId = tb0.Text;

            TextBlock tb1 = (TextBlock)list[1];
            int lesson = int.Parse(tb1.Text);

            TextBlock tb2 = (TextBlock)list[2];
            int day = int.Parse(tb2.Text);

            TextBlock tb3 = (TextBlock)list[3];
            String startDate = tb3.Text;

            TextBlock tb4= (TextBlock)list[4];
            String roomName = tb4.Text;


            ViewModel.prepareForm(roomId, lesson, day, startDate,roomName);

          /*  var dialog = new MessageDialog($"Sender: {s1}");
            await dialog.ShowAsync();*/
        }
    }
}
