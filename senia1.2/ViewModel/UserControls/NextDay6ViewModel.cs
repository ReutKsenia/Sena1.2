﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senia1._2.ViewModel.UserControls
{
    public class NextDay6ViewModel : ViewModelBase
    {
        private DateTime date = DateTime.Now;

        private string _day;
        public string Day
        {
            get { return _day; }
            set { _day = value; RaisePropertyChanged(() => Day); }
        }

        private string _dayWeek;
        public string DayWeek
        {
            get { return _dayWeek; }
            set { _dayWeek = value; RaisePropertyChanged(() => DayWeek); }
        }

        public NextDay6ViewModel()
        {
            var d = new StringBuilder(date.AddDays(6).ToString("dddd", CultureInfo.GetCultureInfo("ru-ru")));
            d[0] = char.ToUpper(d[0]);
            DayWeek = d.ToString();
            Day = date.AddDays(6).ToString("d MMMM", CultureInfo.GetCultureInfo("ru-ru"));
        }
    }
}

