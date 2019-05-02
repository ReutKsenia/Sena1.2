using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senia1._2.ViewModel.UserControls
{
    public class NextDay4ViewModel : ViewModelBase
    {
        private System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        private DateTime date = DateTime.Now;

        private string _day;
        public string Day { get { return _day; } set { _day = value; RaisePropertyChanged(() => Day); } }

        private string _dayWeek;
        public string DayWeek { get { return _dayWeek; } set { _dayWeek = value; RaisePropertyChanged(() => DayWeek); } }

        public NextDay4ViewModel()
        {
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, e) => {
                var d = new StringBuilder(date.AddDays(4).ToString("dddd", CultureInfo.GetCultureInfo("ru-ru")));
                d[0] = char.ToUpper(d[0]);
                DayWeek = d.ToString();
            };
            timer.Tick += (o, e) => { Day = date.AddDays(4).ToString("d MMMM", CultureInfo.GetCultureInfo("ru-ru")); };
            timer.Start();
        }
    }
}

