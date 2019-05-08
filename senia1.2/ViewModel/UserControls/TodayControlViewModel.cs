using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senia1._2.ViewModel.UserControls
{
    public class TodayControlViewModel : ViewModelBase
    {
        private DateTime date = DateTime.Now;
        private string _day;
        public string Day { get { return _day; } set { _day = value; RaisePropertyChanged(() => Day); } }

        public TodayControlViewModel()
        {
            Day = date.ToString("ddd, d MMMM", CultureInfo.GetCultureInfo("ru-ru"));
        }
    }
}
