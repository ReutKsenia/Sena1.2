using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;


namespace senia1._2.ViewModel.Converters
{

        public class DayBorderColorConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string notes = (string)value;

                if (string.IsNullOrEmpty(notes)) return null;

                if (notes.Length > 0) return new SolidColorBrush(Color.FromRgb(0, 217, 22));

                return null;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
}
