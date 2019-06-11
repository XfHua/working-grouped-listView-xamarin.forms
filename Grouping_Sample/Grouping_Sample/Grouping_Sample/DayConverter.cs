using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Grouping_Sample
{
    public class DayConverter : IValueConverter
    {
        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddMilliseconds((long)value) // put your value here
                 .ToLocalTime().ToString("g");
            var spaceIndex = date.IndexOf(" ");
            var dateString = date.Substring(0, spaceIndex);
            DateTime now = DateTime.Parse(dateString);
            //return now.DayOfWeek.ToString();
            return now.ToString("ddd");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
