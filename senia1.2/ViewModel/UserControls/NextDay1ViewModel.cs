using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senia1._2.ViewModel.UserControls
{
    public class NextDay1ViewModel : ViewModelBase
    {
        private System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        private DateTime date = DateTime.Now;
        private string _day;
        public string Day { get { return _day; } set { _day = value; RaisePropertyChanged(() => Day); } }

        public NextDay1ViewModel()
        {
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, e) => { Day = date.AddDays(1).ToString("ddd,d MMMM", CultureInfo.GetCultureInfo("ru-ru")); };
            timer.Start();
        }
    }
}
