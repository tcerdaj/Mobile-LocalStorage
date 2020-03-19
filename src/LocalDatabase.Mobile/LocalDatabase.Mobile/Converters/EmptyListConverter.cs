using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace LocalDatabase.Mobile.Converters
{
    public class EmptyListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            IEnumerable eList = value as IEnumerable;
            bool reverse = parameter != null && parameter.ToString().ToLower() == "invert";
            bool isEmptyList = eList == null || !eList.Cast<object>().Any();

            return reverse ? !isEmptyList : isEmptyList;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
