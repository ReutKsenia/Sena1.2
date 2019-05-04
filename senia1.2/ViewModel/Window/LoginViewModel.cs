using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace senia1._2.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private Model.Users _user;
        public Model.Users User { get { return _user; } set { _user = value; RaisePropertyChanged(() => User); } }

        public LoginViewModel()
        {
        }

        public ICommand Registration_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Model.ToDoListDBEntities1 toDoList = new Model.ToDoListDBEntities1();

                    Model.Users users = new Model.Users();
                    
                });
            }
        }
    }
}
