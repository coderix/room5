using Room5.ViewModels;
using Windows.System.RemoteSystems;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Room5.Views
{
  
    public class AvailableRoomsMondaySelector : DataTemplateSelector
    {
        public DataTemplate MondayTemplate { get; set; }

        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            return this.MondayTemplate;
        }
    }
    public class AvailableRoomsTuesdaySelector : DataTemplateSelector
    {
        public DataTemplate TuesdayTemplate { get; set; }

        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            return this.TuesdayTemplate;
        }
    }
    public class AvailableRoomsWednesdaySelector : DataTemplateSelector
    {
        public DataTemplate WednesdayTemplate { get; set; }

        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            return this.WednesdayTemplate;
        }
    }
    public class AvailableRoomsThursdaySelector : DataTemplateSelector
    {
        public DataTemplate ThursdayTemplate { get; set; }

        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            return this.ThursdayTemplate;
        }
    }
    public class AvailableRoomsFridaySelector : DataTemplateSelector
    {
        public DataTemplate FridayTemplate { get; set; }

        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            return this.FridayTemplate;
        }
    }
    public class AvailableRoomsSaturdaySelector : DataTemplateSelector
    {
        public DataTemplate SaturdayTemplate { get; set; }

        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            return this.SaturdayTemplate;
        }
    }
    public class AvailableRoomsSundaySelector : DataTemplateSelector
    {
        public DataTemplate SundayTemplate { get; set; }

        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            return this.SundayTemplate;
        }
    }


}

