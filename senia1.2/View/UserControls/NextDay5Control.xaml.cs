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
        }
    }
}
