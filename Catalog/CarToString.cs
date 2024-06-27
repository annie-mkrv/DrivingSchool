using Catalog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Catalog
{
    internal class CarToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var car = value as Car;
            return $"{car.id}. {car.car_number} {car.brand}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
