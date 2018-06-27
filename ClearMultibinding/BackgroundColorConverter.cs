
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ClearMultibinding
{
    /// <summary>
    /// https://stackoverflow.com/questions/1224144/change-background-color-for-wpf-textbox-in-changed-state
    /// 
    /// Property changed
    /// </summary>
    public class BackgroundColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var colorRed = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFB0E0E6");
            var colorWhite = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("White");

            var unchanged = new SolidColorBrush(colorWhite);
            var changed = new SolidColorBrush(colorRed);

            if (values.Length == 2)
                if (values[0].Equals(values[1]))
                    return unchanged;
                else
                    return changed;
            else
                return changed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
