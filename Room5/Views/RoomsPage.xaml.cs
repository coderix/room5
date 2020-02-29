using System;

using Room5.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Room5.Views
{
    public sealed partial class RoomsPage : Page
    {
        public RoomsViewModel ViewModel { get; } = new RoomsViewModel();

        public RoomsPage()
        {
            InitializeComponent();
            Loaded += RoomsPage_Loaded;
        }

        private async void RoomsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDataAsync(MasterDetailsViewControl.ViewState);
        }
    }
}
