using senia1._2.Repositories;
using senia1._2.View.UserControls;
using senia1._2.ViewModel;
using senia1._2.ViewModel.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace senia1._2.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public ListPage()
        {
            InitializeComponent();
            DataContext = new ListPageViewMdel();
        }

        private void NewTask_AddMouseClick(object sender, RoutedEventArgs e)
        {
            if (NewTask.Task1.Text == "")
            {
                NewTask.Add.Visibility = Visibility.Visible;
                NewTask.grid2.Visibility = Visibility.Hidden;
            }
            else
            {
                EFTaskRepository taskRepository = new EFTaskRepository();
                EFListRepository listRepository = new EFListRepository();
                DateTime date = DateTime.Now;

                Task task = new Task();
                task.textBlock.Text = NewTask.Task1.Text;
                NewTask.l.Items.Add(task);
                Model.Task task1 = new Model.Task(NewTask.Task1.Text, Title.Content.ToString(), date, listRepository.getByName(Title.Content.ToString()).id, false, "не важно и не срочно");
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
                    NewTask.l.Items.Remove(task);
                };
                NewTask.Task1.Text = "";
                NewTask.Task1.Focus();
            }
        }

        public void getTasks()
        {
            EFTaskRepository taskRepository = new EFTaskRepository();
            EFListRepository listRepository = new EFListRepository();
            NewTask.l.Items.Clear();
            var result = taskRepository.getByCategory(Title.Content.ToString()).ToList();
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
                    NewTask.l.Items.Add(task);

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
                        NewTask.l.Items.Remove(task);
                    };
                }
            }
            else
            {
                NewTask.l.Items.Clear();
            }
        }
    }
}
