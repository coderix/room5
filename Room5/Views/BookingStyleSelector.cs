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

    class WednesdayStyleSelector : BookingStyleSelector
    {
        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            var cell = (item as Telerik.UI.Xaml.Controls.Grid.DataGridCellInfo);
            var bookingRow = cell.Item as BookingsRowModel;
            var booking = bookingRow.Wednesday;
            if (booking.Repeat == (int)App.Repeat.OneTime)
            {
                return this.OneTimeStyle;
            }

            return this.RecurrentStyle;
        }
    }

    class ThursdayStyleSelector : BookingStyleSelector
    {
        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            var cell = (item as Telerik.UI.Xaml.Controls.Grid.DataGridCellInfo);
            var bookingRow = cell.Item as BookingsRowModel;
            var booking = bookingRow.Thursday;
            if (booking.Repeat == (int)App.Repeat.OneTime)
            {
                return this.OneTimeStyle;
            }

            return this.RecurrentStyle;
        }
    }

    class FridayStyleSelector : BookingStyleSelector
    {
        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            var cell = (item as Telerik.UI.Xaml.Controls.Grid.DataGridCellInfo);
            var bookingRow = cell.Item as BookingsRowModel;
            var booking = bookingRow.Friday;
            if (booking.Repeat == (int)App.Repeat.OneTime)
            {
                return this.OneTimeStyle;
            }

            return this.RecurrentStyle;
        }
    }

    class SaturdayStyleSelector : BookingStyleSelector
    {
        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            var cell = (item as Telerik.UI.Xaml.Controls.Grid.DataGridCellInfo);
            var bookingRow = cell.Item as BookingsRowModel;
            var booking = bookingRow.Saturday;
            if (booking.Repeat == (int)App.Repeat.OneTime)
            {
                return this.OneTimeStyle;
            }

            return this.RecurrentStyle;
        }
    }

    class SundayStyleSelector : BookingStyleSelector
    {
        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            var cell = (item as Telerik.UI.Xaml.Controls.Grid.DataGridCellInfo);
            var bookingRow = cell.Item as BookingsRowModel;
            var booking = bookingRow.Sunday;
            if (booking.Repeat == (int)App.Repeat.OneTime)
            {
                return this.OneTimeStyle;
            }

            return this.RecurrentStyle;
        }
    }
}
