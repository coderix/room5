using System;

using Room5.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Room5.Views
{
    public sealed partial class RoomsPage : Page
    {
       

        public RoomsPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            Loaded += RoomsPage_Loaded;
        }

        public RoomsPageViewModel ViewModel { get; set; } =
           new RoomsPageViewModel();

        private async void RoomsPage_Loaded(object sender, RoutedEventArgs e)
        {
            //    // await ViewModel.LoadDataAsync(MasterDetailsViewControl.ViewState);
            await ViewModel.GetRoomListAsync();
        }
    }
}
