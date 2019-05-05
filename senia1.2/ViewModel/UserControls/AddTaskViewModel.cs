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
    public class AddTaskViewModel : ViewModelBase
    {
        private bool _opacityButton;
        public bool OpacityButton { get { return _opacityButton; } set { _opacityButton = value; RaisePropertyChanged(() => OpacityButton); } }

        private bool _opacityControl;
        public bool OpacityControl { get { return _opacityControl; } set { _opacityControl = value; RaisePropertyChanged(() => OpacityControl); } }

        private bool _focusControl;
        public bool FocusControl { get { return _focusControl; } set { _focusControl = value; RaisePropertyChanged(() => FocusControl); } }

        public AddTaskViewModel()
        {
            OpacityControl = false;
            OpacityButton = true;
            FocusControl = false;
        }

        public ICommand AddTask1_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    OpacityButton = false;
                    OpacityControl = true;
                    FocusControl = true;
                });
            }
        }

        public ICommand AddTask2_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    View.UserControls.Task task = new View.UserControls.Task();
                });
            }
        }

        public ICommand Abort_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    OpacityButton = true;
                    OpacityControl = false;
                });
            }
        }
    }
}
