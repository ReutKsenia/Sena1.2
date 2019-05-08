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
                task.textBlock.Text = Today.Task1.Text;
                Today.l.Items.Add(task);
                taskRepository.add(new Model.Task(Today.Task1.Text, "Today", date, listRepository.getByName("Today").id, false, "не важно и не срочно"));
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
                    Today.l.Items.Add(task);
                }
            }
        }
    }
}
