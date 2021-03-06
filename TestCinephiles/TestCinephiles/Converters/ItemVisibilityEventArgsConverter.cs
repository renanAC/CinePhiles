﻿
using System;
using System.Globalization;
using Xamarin.Forms;

namespace TestCinephiles.Converters
{
    public class ItemVisibilityEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var itemSelectedEventArgs = value as ItemVisibilityEventArgs;
            if (itemSelectedEventArgs == null)
            {
                throw new ArgumentException("Expected value to be of type ItemVisibilityEventArgs", nameof(value));
            }
            return itemSelectedEventArgs.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
