﻿using senia1._2.Repositories;
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
    /// Логика взаимодействия для NextDay5Control.xaml
    /// </summary>
    public partial class NextDay5Control : UserControl
    {
        public NextDay5Control()
        {
            InitializeComponent();
            DataContext = new ViewModel.UserControls.NextDay5ViewModel();

            getTasks();
        }

        private void AddTask_Clik(object sender, RoutedEventArgs e)
        {
            if (NextDay5.Task1.Text == "")
            {
                NextDay5.Add.Visibility = Visibility.Visible;
                NextDay5.grid2.Visibility = Visibility.Hidden;
            }
            else
            {
                EFTaskRepository taskRepository = new EFTaskRepository();
                EFListRepository listRepository = new EFListRepository();
                DateTime date = DateTime.Now;

                Task task = new Task();
                task.textBlock.Text = NextDay5.Task1.Text;
                NextDay5.l.Items.Add(task);
                Model.Task task1 = new Model.Task(NextDay5.Task1.Text, "NextDay5", date.AddDays(5), listRepository.getByName("NextDay5").id, false, "не важно и не срочно");
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
                    NextDay5.l.Items.Remove(task);
                };
                NextDay5.Task1.Text = "";
                NextDay5.Task1.Focus();


            }
        }

        private void getTasks()
        {
            EFTaskRepository taskRepository = new EFTaskRepository();

            var result = taskRepository.getByCategory("NextDay5").ToList();
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
                    NextDay5.l.Items.Add(task);
                }
            }
        }
    }
}
