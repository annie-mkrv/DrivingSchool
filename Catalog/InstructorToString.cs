using Catalog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Catalog
{
    internal class InstructorToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var instructor = value as Instructor;
            return $"{instructor.id}. {instructor.fio}, {instructor.carId}. {instructor.car.car_number}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
