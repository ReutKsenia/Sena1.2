using senia1._2.ViewModel;
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

namespace senia1._2.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для AddList.xaml
    /// </summary>
    public partial class AddList : UserControl
    {
        public AddList()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;
            InitializeComponent();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            grid2.Visibility = Visibility.Visible;
            TitleList.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            grid2.Visibility = Visibility.Hidden;
        }
    }
}
