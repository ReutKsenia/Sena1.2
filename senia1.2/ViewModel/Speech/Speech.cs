using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace senia1._2.ViewModel.Speech
{
    public class Speech
    {
        private static SpeechSynthesizer speech;
        private static CultureInfo ci;
        static Speech()
        {
            ci = new CultureInfo("ru-RU");
            speech = new SpeechSynthesizer();
            speech.SetOutputToDefaultAudioDevice();
            speech.SelectVoice("IVONA 2 Tatyana");
            speech.Rate = 2;
        }

        public async void SpeechSynthesis(string message)
        {
            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                speech.Speak(message);
            });
        }
    }
}
