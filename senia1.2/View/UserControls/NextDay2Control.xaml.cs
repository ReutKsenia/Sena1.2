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
    /// Логика взаимодействия для NextDay2Control.xaml
    /// </summary>
    public partial class NextDay2Control : UserControl
    {
        public NextDay2Control()
        {
            InitializeComponent();
            DataContext = new ViewModel.UserControls.NextDay2ViewModel();
        }
    }
}
