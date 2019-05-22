using System.ComponentModel;
using System.Windows.Input;
using SpeechRecognizer.ValueConverters;

namespace SpeechRecognizer
{
    [TypeConverterAttribute(typeof(SpeechGestureConverter))]
    public class SpeechGesture : InputGesture
    {
        private string speechGestureString;

        public SpeechGesture(string speechString)
        {
            speechGestureString = speechString;
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            return false;
        }

        public override string ToString()
        {
            return speechGestureString;
        }
    }
}
