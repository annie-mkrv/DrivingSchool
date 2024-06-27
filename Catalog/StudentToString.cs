using Catalog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Catalog
{
    internal class StudentToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var student = value as Student;
            return $"{student.id}. {student.fio}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
