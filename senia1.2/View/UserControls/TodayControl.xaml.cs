﻿using System;
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
using senia1._2.ViewModel.Window;

namespace senia1._2.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TodayControl.xaml
    /// </summary>
    public partial class TodayControl : UserControl
    {
        UnitOfWork unit = new UnitOfWork();
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
                DateTime date = DateTime.Now;

                Model.Task task1 = new Model.Task();
                task1.Value = Today.Task1.Text;
                task1.Category = "Today";
                task1.DateExpected = date;
                var result1 = unit.List.getByUserId(CurrentUser.User.Id).ToList();
                for (int i = 0; i < result1.Count(); i++)
                {
                    if (result1[i].Title == "Today")
                    {
                        task1.ListId = result1[i].id;
                    }
                }
                task1.Completed = false;

                Task task = new Task();
                task.textBlock.Text = Today.Task1.Text;
                if(Today.Priority4.IsChecked == true)
                {
                    task1.Priority = "не важно и не срочно";
                }
                if(Today.Priority3.IsChecked == true)
                {
                    task1.Priority = "не важно и срочно";
                    task.checkBox.BorderBrush = Today.Priority3.BorderBrush;
                }
                if (Today.Priority2.IsChecked == true)
                {
                    task1.Priority = "важно и не срочно";
                    task.checkBox.BorderBrush = Today.Priority2.BorderBrush;
                }
                if (Today.Priority1.IsChecked == true)
                {
                    task1.Priority = "важно и срочно";
                    task.checkBox.BorderBrush = Today.Priority1.BorderBrush;
                }

                Today.l.Items.Add(task);
                
                unit.Task.add(task1);
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

                        unit.Task.update(task1, new Model.Task(task.textBlock.Text, task1.Category, task1.DateExpected, task1.ListId, task1.Completed, task1.Priority));
                    }
                    else
                    {
                        task.tas.Visibility = Visibility.Visible;
                        task.modify.Visibility = Visibility.Collapsed;
                    }
                    
                };
                task.Delete.Click += (o, i) =>
                {
                    var result = unit.Task.getById(task.Id);
                    unit.Task.delete(result);
                    Today.l.Items.Remove(task);
                };

                Today.Task1.Text = "";
                Today.Task1.Focus();

            }
            
        }

        private void getTasks()
        {
            var res = unit.List.getByUserId(CurrentUser.User.Id).ToList();
            for (int k = 0; k < res.Count(); k++)
            {
                if (res[k].Title == "Today")
                {
                    var result = unit.Task.getByListId(res[k].id).ToList();
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
                            Today.l.Items.Add(task);


                            var result1 = unit.Task.getById(task.Id);
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

                                    unit.Task.update(result1, new Model.Task(task.textBlock.Text, result1.Category, result1.DateExpected, result1.ListId, result1.Completed, result1.Priority));
                                }
                                else
                                {
                                    task.tas.Visibility = Visibility.Visible;
                                    task.modify.Visibility = Visibility.Collapsed;
                                }

                            };
                            task.Delete.Click += (o, a) =>
                            {
                                unit.Task.delete(result1);
                                Today.l.Items.Remove(task);
                            };
                        }
                    }
                }
            }
        }

        private void Trash_Click(object sender, RoutedEventArgs e)
        {
            var res = unit.List.getByUserId(CurrentUser.User.Id).ToList();
            for (int k = 0; k < res.Count(); k++)
            {
                if (res[k].Title == "Today")
                {
                    var result = unit.Task.getByListId(res[k].id).ToList();
                    if (result.Count() != 0)
                    {
                        for (int i = 0; i < result.Count(); i++)
                        {
                            unit.Task.delete(result[i]);
                            Today.l.Items.Clear();
                        }
                    }
                }
            }
        }
    }
}
