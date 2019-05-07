using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace senia1._2.ViewModel
{
    public class CalendarPageViewModel : ViewModelBase
    {
        readonly List<string> months = new List<string> { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        private string _time;
        public string Time
        {
            get { return _time; }
            set { _time = value; RaisePropertyChanged(() => Time); }
        }

        private List<string> _months;
        public List<string> Months
        {
            get { return _months; }
            set { _months = value; RaisePropertyChanged(() => Months); }
        }

        private string _selected;
        public string Selected
        {
            get { return _selected; }
            set { _selected = value; RaisePropertyChanged(() => Selected); }
        }

        public CalendarPageViewModel()
        {
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, e) => { Time = DateTime.Now.ToString("HH:mm:ss"); };
            timer.Start();
            Selected = months.FirstOrDefault(w => w == DateTime.Today.ToString("MMMM"));
            Months = months;
        }
    }
}
