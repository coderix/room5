using System;

using Room5.Core.Models;
using Room5.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Room5.Views
{
    public sealed partial class RoomsDetailControl : UserControl
    {
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
            control.ForegroundElement.ChangeView(0, 0, 1);
            
        }

        private void addRoom(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
