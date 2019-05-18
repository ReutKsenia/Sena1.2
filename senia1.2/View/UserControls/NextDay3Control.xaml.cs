using senia1._2.Repositories;
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

namespace senia1._2.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для NextDay3Control.xaml
    /// </summary>
    public partial class NextDay3Control : UserControl
    {
        public NextDay3Control()
        {
            InitializeComponent();
            DataContext = new ViewModel.UserControls.NextDay3ViewModel();

            getTasks();
        }

        private void AddTask_Clik(object sender, RoutedEventArgs e)
        {
            if (NextDay3.Task1.Text == "")
            {
                NextDay3.Add.Visibility = Visibility.Visible;
                NextDay3.grid2.Visibility = Visibility.Hidden;
            }
            else
            {
                EFTaskRepository taskRepository = new EFTaskRepository();
                EFListRepository listRepository = new EFListRepository();
                DateTime date = DateTime.Now;

                Model.Task task1 = new Model.Task();
                task1.Value = NextDay3.Task1.Text;
                task1.Category = "NextDay3";
                task1.DateExpected = date;
                task1.ListId = listRepository.getByName("NextDay3").id;
                task1.Completed = false;

                Task task = new Task();
                task.textBlock.Text = NextDay3.Task1.Text;
                if (NextDay3.Priority4.IsChecked == true)
                {
                    task1.Priority = "не важно и не срочно";
                }
                if (NextDay3.Priority3.IsChecked == true)
                {
                    task1.Priority = "не важно и срочно";
                    task.checkBox.BorderBrush = NextDay3.Priority3.BorderBrush;
                }
                if (NextDay3.Priority2.IsChecked == true)
                {
                    task1.Priority = "важно и не срочно";
                    task.checkBox.BorderBrush = NextDay3.Priority2.BorderBrush;
                }
                if (NextDay3.Priority1.IsChecked == true)
                {
                    task1.Priority = "важно и срочно";
                    task.checkBox.BorderBrush = NextDay3.Priority1.BorderBrush;
                }
                NextDay3.l.Items.Add(task);
                
                taskRepository.add(task1);
                task.Id = task1.id;

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
                    NextDay3.l.Items.Remove(task);
                };
                NextDay3.Task1.Text = "";
                NextDay3.Task1.Focus();


            }
        }

        private void getTasks()
        {
            EFTaskRepository taskRepository = new EFTaskRepository();

            var result = taskRepository.getByCategory("NextDay3").ToList();
            if (result.Count() > 0)
            {
                for (int i = 0; i < result.Count(); i++)
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

                    if (result[i].Priority == "не важно и срочно")
                    {
                        task.checkBox.BorderBrush = new SolidColorBrush(Color.FromRgb(65, 247, 28));
                    }
                    if (result[i].Priority == "важно и не срочно")
                    {
                        task.checkBox.BorderBrush = new SolidColorBrush(Color.FromRgb(254, 255, 8));
                    }
                    if (result[i].Priority == "важно и срочно")
                    {
                        task.checkBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 1, 1));
                    }
                    NextDay3.l.Items.Add(task);

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
                        NextDay3.l.Items.Remove(task);
                    };
                }
            }
        }

        private void Trash_Click(object sender, RoutedEventArgs e)
        {
            EFTaskRepository taskRepository = new EFTaskRepository();

            var result = taskRepository.getByCategory("NextDay3").ToList();
            if (result.Count() != 0)
            {
                for (int i = 0; i < result.Count(); i++)
                {
                    taskRepository.delete(result[i]);
                    NextDay3.l.Items.Clear();
                }
            }
        }
    }
}
