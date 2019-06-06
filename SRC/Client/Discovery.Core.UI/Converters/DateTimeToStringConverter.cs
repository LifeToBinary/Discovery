using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Discovery.Core.UI.Converters
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture)
        {
            TimeSpan time = DateTime.Now - (DateTime)value;
            TimeSpan[] timeReferences =
            {
                TimeSpan.FromMinutes(1),
                TimeSpan.FromMinutes(60),
                TimeSpan.FromHours(24),
                TimeSpan.FromDays(30),
                TimeSpan.MaxValue
            };
            string[] values =
            {
                "一分钟前",
                $"{time.Minutes} 分钟前",
                $"{time.Hours} 小时前",
                $"{time.Days} 天前",
                $"{time:yyyy年MM月dd日}"
            };
            return GetValue();
            string GetValue(int index = 0)
                => time < timeReferences[index]
                ? values[index]
                : GetValue(index + 1);
        }
       

        public object ConvertBack(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture)
            => throw new NotSupportedException();
    }
}
