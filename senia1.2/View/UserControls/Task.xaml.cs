using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using senia1._2.ViewModel.UserControls;

namespace senia1._2.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для Task.xaml
    /// </summary>
    public partial class Task : UserControl
    {
        public Task()
        {
            InitializeComponent();
            
        }
        public int Id { get; set; }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            textBlock.TextDecorations = TextDecorations.Strikethrough;

            EFTaskRepository taskRepository = new EFTaskRepository();
            var result = taskRepository.getById(this.Id);
            taskRepository.update(result, new Model.Task(result.Value, result.Category, result.DateExpected, result.ListId, true, result.Priority));
        }
        

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            textBlock.TextDecorations = null;

            EFTaskRepository taskRepository = new EFTaskRepository();
            var result = taskRepository.getById(this.Id);
            taskRepository.update(result, new Model.Task(result.Value, result.Category, result.DateExpected, result.ListId, false, result.Priority));
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            tas.Visibility = Visibility.Collapsed;
            modify.Visibility = Visibility.Visible;
            modify.Task1.Text = textBlock.Text;
            modify.Task1.Focus();
        }
    }
}
