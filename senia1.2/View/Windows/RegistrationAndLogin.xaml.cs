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
using System.Windows.Shapes;
using senia1._2.ViewModel;
using senia1._2.Model;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Globalization;

namespace senia1._2.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationAndLogin.xaml
    /// </summary>
    public partial class RegistrationAndLogin : Window
    {
        LoginViewModel loginViewModel = new LoginViewModel();
        public RegistrationAndLogin()
        {
            InitializeComponent();
            DataContext = loginViewModel;
            
        }

        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void RegistrationAndLogin1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            loginViewModel.registration(pass1NameTextBox.Password, pass2NameTextBox.Password);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (loginViewModel.compareDataOfUser(pass.Password))
                Close();
        }
    }
}
