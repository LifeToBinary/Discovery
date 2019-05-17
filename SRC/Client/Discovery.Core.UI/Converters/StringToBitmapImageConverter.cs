using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Discovery.Core.UI.Converters
{
    /// <summary>
    /// string 类型和 BitmapImage 类型的值转换器
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class StringToBitmapImageConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
            => new BitmapImage(new Uri(value as string));

        public object ConvertBack(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture)
            => throw new NotSupportedException();
    }
}
