using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using senia1._2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace senia1._2.ViewModel.UserControls
{
    public class TaskViewModel : ViewModelBase
    {
        public Model.Task Task;
        //public Model.Task Task
        //{
        //    get { return task; }
        //    set { task = value; RaisePropertyChanged(() => Task); }
        //}
        private string _decoration;
        public string Decoration
        {
            get { return _decoration; }
            set { _decoration = value; RaisePropertyChanged(() => Decoration); }
        }

        private bool _state;
        public bool State
        {
            get { return _state; }
            set { _state = value; RaisePropertyChanged(() => State); }
        }

        public TaskViewModel()
        {
            
        }


        public ICommand Check_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    EFTaskRepository taskRepository = new EFTaskRepository();
                    if (State == true)
                    {
                        Decoration = "Strikethrough";
                        var result = taskRepository.getById(Task.id);
                        taskRepository.update(result, new Model.Task(result.Value, result.Category, result.DateExpected, result.ListId, true, result.Priority));
                    }
                   else
                    {
                        Decoration = null;
                        var result = taskRepository.getById(Task.id);
                        taskRepository.update(result, new Model.Task(result.Value, result.Category, result.DateExpected, result.ListId, false, result.Priority));
                    }
                });
            }
        }
    }
}
