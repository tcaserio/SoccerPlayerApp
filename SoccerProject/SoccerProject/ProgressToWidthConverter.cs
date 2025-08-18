using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SoccerProject
{
    public class ProgressToWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert the progress percentage (0-100) to a width value
            double progress = (double)value / 100.0;
            return progress * Application.Current.MainPage.Width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
