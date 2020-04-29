using Room5.ViewModels;
using Windows.System.RemoteSystems;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Room5.Views
{
   /* public static class Templates
    {
      //  public DataTemplate RecurrentBookingTemplate { get; set; }
    }*/
    public class AvailableRoomsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MondayTemplate { get; set; }
        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            return MondayTemplate;
        }
    }

    
    }
}
