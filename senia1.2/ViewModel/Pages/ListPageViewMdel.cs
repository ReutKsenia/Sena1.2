using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senia1._2.ViewModel.Pages
{

    public class ListPageViewMdel : ViewModelBase
    {
        private string _title;
        public string Title1
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(() => Title1); }
        }

        public ListPageViewMdel()
        {

        }


    }
}
