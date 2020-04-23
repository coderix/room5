using Room5.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Room5.Views
{
    class BookingStyleSelector : StyleSelector
    {
        public Style RecurrentStyle { get; set; }

        public Style OneTimeStyle { get; set; }

        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            var cell = (item as Telerik.UI.Xaml.Controls.Grid.DataGridCellInfo);
            var bookingRow = cell.Item as BookingsRowModel;
            var booking = bookingRow.Tuesday;
            if (booking.Repeat == (int)App.Repeat.OneTime)
            {
                return this.OneTimeStyle;
            }

            return this.RecurrentStyle;
        }
    }

    class MondayStyleSelector : BookingStyleSelector
    {

        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            var cell = (item as Telerik.UI.Xaml.Controls.Grid.DataGridCellInfo);
            var bookingRow = cell.Item as BookingsRowModel;
            var booking = bookingRow.Monday;
            if (booking.Repeat == (int)App.Repeat.OneTime)
            {
                return this.OneTimeStyle;
            }

            return this.RecurrentStyle;
        }
    }

    class TuesdayStyleSelector : BookingStyleSelector
    {
      
        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            var cell = (item as Telerik.UI.Xaml.Controls.Grid.DataGridCellInfo);
            var bookingRow = cell.Item as BookingsRowModel;
            var booking = bookingRow.Tuesday;
            if (booking.Repeat == (int)App.Repeat.OneTime)
            {
                return this.OneTimeStyle;
            }

            return this.RecurrentStyle;
        }
    }
}
