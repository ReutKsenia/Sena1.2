using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace SpeechRecognizer.ValueConverters
{
    public class SpeechGestureConverter : TypeConverter, IValueConverter
    {
        public SpeechGestureConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new SpeechGesture(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var gesture = value as SpeechGesture;
            return gesture == null ? null : gesture.ToString();
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return Convert(value, typeof(SpeechGesture), null, culture);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object source)
        {
            return Convert(source, typeof(SpeechGesture), null, culture);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
