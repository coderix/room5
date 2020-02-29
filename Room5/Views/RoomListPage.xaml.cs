using System;

using Room5.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Room5.Views
{
    public sealed partial class RoomListPage : Page
    {
        public RoomListViewModel ViewModel { get; } = new RoomListViewModel();

        public RoomListPage()
        {
            InitializeComponent();
        }
    }
}
