using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using senia1._2.Repositories;
using System.ComponentModel;
using senia1._2.Model;
using System.Threading;
using System.Globalization;
using System.Speech.Synthesis;
using senia1._2.ViewModel.Window;
using senia1._2.View.Windows;
using System.Text.RegularExpressions;

namespace senia1._2.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        EFUserRepository userRepository = new EFUserRepository();
        private string patternPassword = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
        string userName;
        string login;
        string login1;
        string password1;
        string password2;

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Login1
        {
            get { return login1; }
            set
            {
                login1 = value;
                OnPropertyChanged("Login1");
            }
        }
        public string Password1
        {
            get { return password1; }
            set
            {
                password1 = value;
                OnPropertyChanged("Password1");
            }
        }
        public string Password2
        {
            get { return password2; }
            set
            {
                password2 = value;
                OnPropertyChanged("Password2");
            }
        }

        public LoginViewModel()
        {
            CultureInfo ci = new CultureInfo("ru-RU");
            SpeechSynthesizer speech = new SpeechSynthesizer();
            speech.SetOutputToDefaultAudioDevice();
            speech.SelectVoice("IVONA 2 Tatyana");
            speech.Rate = 2;
            Speech1("Здравствуйте, меня зовут Сеня. и я ваш голосовой помошник. Войдите или зарегистрируйтесь");
        }

        //public ICommand Reg_Clik
        //{
        //    get
        //    {
        //        return new RelayCommand(() =>
        //        {
        //            Speech1("Чобы закрыть форму регистрации, нажмите на оранжевый ярлычёк");
        //        });
        //    }
        //}

        private async void Speech1(string message)
        {
            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                CultureInfo ci = new CultureInfo("ru-RU");
                SpeechSynthesizer speech = new SpeechSynthesizer();
                var voices = speech.GetInstalledVoices(ci);
                speech.SetOutputToDefaultAudioDevice();
                speech.SelectVoice("IVONA 2 Tatyana");
                speech.Speak(message);
            });
        }

        public bool registration(string password1, string password2)
        {
            Password1 = password1;
            Password2 = password2;
            if (!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Login) && !String.IsNullOrEmpty(Password1) && !String.IsNullOrEmpty(Password2))
            {
                if (Regex.IsMatch(Password1, patternPassword, RegexOptions.IgnoreCase))
                {
                    if (Password1.Equals(Password2))
                    {
                    
                    User tmp = userRepository.getByLogin(Login);
                    if (tmp == null)
                    {
                        string Password = User.getHash(Password1);
                        userRepository.add(new User(UserName, Login, Password));
                        Speech1("Вы зарегистрированы, теперь вы можете войти в приложение");
                        return true;
                    }
                    else
                    {
                        Speech1("Пользователь с таким логином уже существует, придумайте другой логин");
                        return false;
                    }

                    }

                    else
                    {
                    Speech1("Пароли должны совпадать");
                    return false;
                    }
                }
                else
                {
                    Speech1("Пароль должен содержать минимум 8 символов, минимум одну букву и одну цифру");
                    return false;
                }

            }
            else
            {
                Speech1("Вы не всё ввели");
                return false;
            }

        }

        public bool compareDataOfUser(string password)
        {
            if (!String.IsNullOrEmpty(Login1) && !String.IsNullOrEmpty(password))
            {
                User tmp = userRepository.getByLogin(Login1);
                if (tmp != null)
                {
                    if (User.getHash(password).Equals(tmp.password))
                    {
                        CurrentUser.User = tmp;
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        return true;
                    }
                    return false;
                }
                else
                {
                    Speech1("Проверьте введённые данные");
                    return false;
                }
            }
            else
            {
                Speech1("Вы не всё ввели");
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
