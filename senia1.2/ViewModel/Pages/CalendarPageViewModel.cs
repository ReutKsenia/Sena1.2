using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senia1._2.ViewModel
{
    public class CalendarPageViewModel : ViewModelBase
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        private string _time;
        public string Time { get { return _time; } set { _time = value; RaisePropertyChanged(() => Time); } }

        public CalendarPageViewModel()
        {
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, e) => { Time = DateTime.Now.ToString("HH:mm:ss"); };
            timer.Start();
        }
    }
}
