using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using senia1._2.Repositories;

namespace senia1._2.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TodayControl.xaml
    /// </summary>
    public partial class TodayControl : UserControl
    {
        public TodayControl()
        {
            InitializeComponent();
            DataContext = new ViewModel.UserControls.TodayControlViewModel();

            getTasks();
        }
        
        private void AddTask_AddMouseClick(object sender, RoutedEventArgs e)
        {
            if (Today.Task1.Text == "")
            {
                Today.Add.Visibility = Visibility.Visible;
                Today.grid2.Visibility = Visibility.Hidden;
            }
            else
            {
                EFTaskRepository taskRepository = new EFTaskRepository();
                EFListRepository listRepository = new EFListRepository();
                DateTime date = DateTime.Now;

                Task task = new Task();
                //task.Modify.Click += new RoutedEventHandler(ModifyClick);
                //task.Delete.Click += new RoutedEventHandler(DeleteClick);
                task.textBlock.Text = Today.Task1.Text;
                Today.l.Items.Add(task);
                Model.Task task1 = new Model.Task(Today.Task1.Text, "Today", date, listRepository.getByName("Today").id, false, "не важно и не срочно");
                taskRepository.add(task1);
                task.Id = task1.id;
                task.modify.Abort.Click += (o, w) =>
                {
                    task.tas.Visibility = Visibility.Visible;
                    task.modify.Visibility = Visibility.Collapsed;
                };
                task.modify.Save.Click += (o, q) =>
                {
                    if(task.modify.Task1.Text != "")
                    {
                        task.textBlock.Text = task.modify.Task1.Text;
                        task.tas.Visibility = Visibility.Visible;
                        task.modify.Visibility = Visibility.Collapsed;

                        taskRepository.update(task1, new Model.Task(task.textBlock.Text, task1.Category, task1.DateExpected, task1.ListId, task1.Completed, task1.Priority));
                    }
                    else
                    {
                        task.tas.Visibility = Visibility.Visible;
                        task.modify.Visibility = Visibility.Collapsed;
                    }
                    
                };
                task.Delete.Click += (o, i) =>
                {
                    var result = taskRepository.getById(task.Id);
                    taskRepository.delete(result);
                    Today.l.Items.Remove(task);
                };

                Today.Task1.Text = "";
                Today.Task1.Focus();

            }
            
        }

        private void getTasks()
        {
            EFTaskRepository taskRepository = new EFTaskRepository();
            EFListRepository listRepository = new EFListRepository();

            var result = taskRepository.getByCategory("Today").ToList();
            if(result.Count() > 0)
            {
                for(int i=0; i < result.Count(); i++)
                {
                    Task task = new Task();

                    task.textBlock.Text = result[i].Value;
                    task.Id = result[i].id;
                    if (result[i].Completed == true)
                    {
                        task.checkBox.IsChecked = true;
                    }
                    else
                    {
                        task.checkBox.IsChecked = false;
                    }
                    Today.l.Items.Add(task);


                    var result1 = taskRepository.getById(task.Id);
                    task.modify.Abort.Click += (o, w) =>
                    {
                        task.tas.Visibility = Visibility.Visible;
                        task.modify.Visibility = Visibility.Collapsed;
                    };
                    task.modify.Save.Click += (o, q) =>
                    {
                        if (task.modify.Task1.Text != "")
                        {
                            task.textBlock.Text = task.modify.Task1.Text;
                            task.tas.Visibility = Visibility.Visible;
                            task.modify.Visibility = Visibility.Collapsed;

                            taskRepository.update(result1, new Model.Task(task.textBlock.Text, result1.Category, result1.DateExpected, result1.ListId, result1.Completed, result1.Priority));
                        }
                        else
                        {
                            task.tas.Visibility = Visibility.Visible;
                            task.modify.Visibility = Visibility.Collapsed;
                        }

                    };
                    task.Delete.Click += (o, a) =>
                    {
                        taskRepository.delete(result1);
                        Today.l.Items.Remove(task);
                    };
                }
            }
        }
    }
}
