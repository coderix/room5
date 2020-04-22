using Room5.ViewModels;
using Windows.System.RemoteSystems;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Room5.Views
{
    public static class Templates
    {
      //  public DataTemplate RecurrentBookingTemplate { get; set; }
    }
    public class BookingTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RecurrentBookingTemplate { get; set; }
        public DataTemplate OneTimeBookingTemplate { get; set; }

        public DataTemplate NoBookingTemplate { get; set; }

        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            BookingsRowModel row = (BookingsRowModel)item;
            if (row.Thursday.Title == "")
            {
                return this.NoBookingTemplate;
            }
            else
            {
                return this.RecurrentBookingTemplate;
            }
        }
    }

    public class ThursdayTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RecurrentBookingTemplate { get; set; }
        public DataTemplate OneTimeBookingTemplate { get; set; }

        public DataTemplate NoBookingTemplate { get; set; }

        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            BookingsRowModel row = (BookingsRowModel)item;
            if (row.Thursday.Title == "")
            {
                return this.NoBookingTemplate;
            }
            else if (row.Thursday.Repeat == (int)App.Repeat.Weekly)
            {
                return this.RecurrentBookingTemplate;
            }
            else
            {
                return this.OneTimeBookingTemplate;
            }
        }
    }
}
