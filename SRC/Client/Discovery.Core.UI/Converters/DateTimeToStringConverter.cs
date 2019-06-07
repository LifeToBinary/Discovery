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
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture)
        {
            var time = (DateTime)value;
            TimeSpan TimeDifference = DateTime.Now - time;
            TimeSpan[] timeReferences =
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromMinutes(1),
                TimeSpan.FromMinutes(60),
                TimeSpan.FromHours(24),
                TimeSpan.FromDays(30),
                TimeSpan.MaxValue
            };
            string[] values =
            {
                "1 秒前",
                $"{TimeDifference.Seconds} 秒前",
                $"{TimeDifference.Minutes} 分钟前",
                $"{TimeDifference.Hours} 小时前",
                $"{TimeDifference.Days} 天前",
                $"{time:yyyy年MM月dd日}"
            };
            return GetValue();
            string GetValue(int index = 0)
                => TimeDifference < timeReferences[index]
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
