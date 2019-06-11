using Discovery.Core.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Discovery.Core.UI.Converters
{
    [ValueConversion(typeof(Sex), typeof(BitmapImage))]
    public class GenderToIconConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
            => new[]
            {
                new BitmapImage(new Uri("http://www.suence.art:10003/Discovery/DiscoveryWebFiles/Default/Images/MaleIcon.png")),
                new BitmapImage(new Uri("http://www.suence.art:10003/Discovery/DiscoveryWebFiles/Default/Images/FemaleIcon.png")),
                null
            }[(int)(Sex)value];

        public object ConvertBack(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture) 
            => throw new NotSupportedException();
    }
}
