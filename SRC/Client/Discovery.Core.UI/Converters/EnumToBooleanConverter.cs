using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Discovery.Core.UI.Converters
{
    [ValueConversion(typeof(Enum), typeof(bool))]
    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value.Equals(parameter);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value.Equals(true) ? parameter : Binding.DoNothing;
    }
}
