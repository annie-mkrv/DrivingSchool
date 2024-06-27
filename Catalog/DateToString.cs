using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Catalog
{
    public class DateToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //if (value is DateTime)
            //{
            //    return ((DateTime) value).ToString("HH:mm");
            //}
            //else
            //{
            //    return DateTime.Parse(value.ToString(), null, DateTimeStyles.RoundtripKind).ToLocalTime().ToString("HH:mm");
            //}
            return DateTime.Parse(value.ToString(), null, DateTimeStyles.RoundtripKind).ToLocalTime().ToString("dd.MM.yyyy HH:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
