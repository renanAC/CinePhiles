
using System;
using System.Globalization;
using Xamarin.Forms;

namespace TestCinephiles.Converters
{
    public class ItemSelectedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var itemSelectedEventArgs = value as SelectedItemChangedEventArgs;
            if (itemSelectedEventArgs == null)
            {
                throw new ArgumentException("Expected value to be of type SelectedItemChangedEventArgs", nameof(value));
            }
            return itemSelectedEventArgs.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
