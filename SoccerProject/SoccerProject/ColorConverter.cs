using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SoccerProject
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return color;
            }

            return Binding.DoNothing;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
