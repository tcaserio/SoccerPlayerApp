using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SoccerProject
{
    public class ProgressToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int progress)
            {
                // Convert progress to a color value
                var hue = (float)(progress / 100.0 * 120);
                var color = Color.FromHsv(hue, 1, 1);

                return color;
            }

            return Color.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
