using senia1._2.Repositories;
using senia1._2.ViewModel;
using senia1._2.ViewModel.Pages;
using senia1._2.ViewModel.Window;
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

namespace senia1._2.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для OptionsPage.xaml
    /// </summary>
    public partial class OptionsPage : Page
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public OptionsPage()
        {
            InitializeComponent();
            DataContext = new OptionsViewModel();
        }

        private void ModifyUN_Click(object sender, RoutedEventArgs e)
        {
            UN.Visibility = Visibility.Collapsed;
            UserName.Visibility = Visibility.Visible;
            ModifyUN.Visibility = Visibility.Collapsed;
            SaveUN.Visibility = Visibility.Visible;
            UserName.Focus();
        }

        private void SaveUN_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel mainViewModel = new MainViewModel();
            var result = unitOfWork.User.getByLogin(CurrentUser.User.Login);
            if(UserName.Text != "" && UserName.Text != CurrentUser.User.UserName)
            {
                unitOfWork.User.update(result, new Model.User(UserName.Text, result.Login, result.Password, result.Foto));
            }
            UN.Visibility = Visibility.Visible;
            UserName.Visibility = Visibility.Collapsed;
            ModifyUN.Visibility = Visibility.Visible;
            SaveUN.Visibility = Visibility.Collapsed;
            UN.Content = unitOfWork.User.getByLogin(CurrentUser.User.Login).UserName;
            mainViewModel.UserName = unitOfWork.User.getByLogin(CurrentUser.User.Login).UserName;
        }

        private void ModifyLogin_Click(object sender, RoutedEventArgs e)
        {
            Login.Visibility = Visibility.Collapsed;
            LoginTextBox.Visibility = Visibility.Visible;
            ModifyLogin.Visibility = Visibility.Collapsed;
            SaveLogin.Visibility = Visibility.Visible;
            LoginTextBox.Focus();
        }

        private void SaveLogin_Click(object sender, RoutedEventArgs e)
        {
            var result = unitOfWork.User.getByLogin(CurrentUser.User.Login);
            if (LoginTextBox.Text != "" && LoginTextBox.Text != CurrentUser.User.Login)
            {
                unitOfWork.User.update(result, new Model.User(result.UserName, LoginTextBox.Text, result.Password, result.Foto));
            }
            Login.Visibility = Visibility.Visible;
            LoginTextBox.Visibility = Visibility.Collapsed;
            ModifyLogin.Visibility = Visibility.Visible;
            SaveLogin.Visibility = Visibility.Collapsed;
            Login.Content = unitOfWork.User.getByLogin(CurrentUser.User.Login).UserName;
        }

        private void ModifyPassword_Click(object sender, RoutedEventArgs e)
        {
            Password.Visibility = Visibility.Collapsed;
            PasswordTextBox.Visibility = Visibility.Visible;
            ModifyPassword.Visibility = Visibility.Collapsed;
            SavePassword.Visibility = Visibility.Visible;
            PasswordTextBox.Focus();
        }
    }
}
