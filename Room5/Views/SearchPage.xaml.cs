using System;

using Room5.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Room5.Views
{
    public sealed partial class SearchPage : Page
    {
        public SearchViewModel ViewModel { get; } = new SearchViewModel();

        public SearchPage()
        {
            InitializeComponent();
        }
    }
}
