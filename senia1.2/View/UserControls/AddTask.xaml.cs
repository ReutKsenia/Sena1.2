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
    /// Логика взаимодействия для AddTask.xaml
    /// </summary>
    public partial class AddTask : UserControl
    {
        public AddTask()
        {
            InitializeComponent();
            DataContext = new ViewModel.AddTaskViewModel();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (Task1.Text == "")
            {
                Add.Visibility = Visibility.Visible;
                grid2.Visibility = Visibility.Hidden;
            }
            else
            {
                Task task = new Task();
                task.textBlock.Text = Task1.Text;
                l.Items.Add(task);
                Task1.Text = "";
                Task1.Focus();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Add.Visibility = Visibility.Hidden;
            grid2.Visibility = Visibility.Visible;
            Task1.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add.Visibility = Visibility.Visible;
            grid2.Visibility = Visibility.Hidden;
        }
    }
}
