using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using senia1._2.ViewModel.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace senia1._2.ViewModel.Pages
{
    public class OptionsViewModel : ViewModelBase
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged(() => UserName); }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; RaisePropertyChanged(() => Login); }
        }

        private int _visibilityUN;
        public int VisibilityUN
        {
            get { return _visibilityUN; }
            set { _visibilityUN = value; RaisePropertyChanged(() => VisibilityUN); }
        }

        public OptionsViewModel()
        {
            UserName = CurrentUser.User.UserName;
            Login = CurrentUser.User.Login;
            VisibilityUN = 2;
        }

        public ICommand ModifyUserName_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    
                });
            }
        }
    }
}
