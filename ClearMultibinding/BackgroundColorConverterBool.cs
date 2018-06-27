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
    /// Property changed and display it on a datagrid.
    /// 
    /// Boolean Converter
    /// </summary>

    public class BackgroundColorConverterBool : IMultiValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>True is property has changed</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is null || values[1] is null) return false;


            if (values.Length == 2)
                if (values[0].Equals(values[1]))
                    return false;
                else
                    return true;
            else
                return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}