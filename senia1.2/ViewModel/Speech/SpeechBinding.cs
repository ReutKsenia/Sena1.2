using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Speech.Recognition;
using System.Windows.Input;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;
using SpeechRecognizer.ValueConverters;

namespace SpeechRecognizer
{
    public class SpeechBinding : InputBinding
    {
        
        protected static readonly SpeechRecognitionEngine recognizer
            = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("ru-ru"));
        static bool recognizerStarted = false;
        static readonly Dispatcher mainThreadDispatcher = Dispatcher.CurrentDispatcher;

        static SpeechBinding()
        {
            recognizer.RecognizerUpdateReached += recognizer_RecognizerUpdateReached;
        }

        static void recognizer_RecognizerUpdateReached(object sender, RecognizerUpdateReachedEventArgs e)
        {
            var grammar = e.UserToken as Grammar;
            recognizer.LoadGrammar(grammar);
            Debug.WriteLine("Loaded :" + grammar.Name);
            StartRecognizer();

        }

        [TypeConverterAttribute(typeof(SpeechGestureConverter))]
        public override InputGesture Gesture
        {
            get
            {
                return base.Gesture;
            }
            set
            {
                base.Gesture = value;
                LoadGrammar();

            }
        }

        protected virtual void LoadGrammar()
        {
            var speechStr = Gesture.ToString();
            var grammarBuilder = new GrammarBuilder(speechStr);
            grammarBuilder.Culture = recognizer.RecognizerInfo.Culture;
            var grammar = new Grammar(grammarBuilder) { Name = speechStr };
            grammar.SpeechRecognized += grammar_SpeechRecognized;
            Debug.WriteLine("Loading " + speechStr);
            recognizer.RequestRecognizerUpdate(grammar);


        }

        protected static void StartRecognizer()
        {
            if (recognizerStarted == false)
            {
                try
                {
                    recognizer.SetInputToDefaultAudioDevice();
                }
                catch (Exception ex)
                {
                    Messenger.Default.Send<Exception>(new Exception("Unable to set input from default audio device." + Environment.NewLine + "Check if micropone is availble and enabled.", ex));
                    return;
                }
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
                recognizerStarted = true;
            }
        }

        protected void grammar_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            mainThreadDispatcher.Invoke(new Action(() =>
            {
                OnSpeechRecognized(sender, e);

            }), DispatcherPriority.Input);
        }

        protected virtual void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (Command != null)
            {
                Command.Execute(e.Result.Text);
            }
        }
    }
}
