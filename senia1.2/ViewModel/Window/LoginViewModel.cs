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
        EFListRepository listRepository = new EFListRepository();
        
        private DateTime date = DateTime.Now;

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
                        User user = new User(UserName, Login, Password);
                        userRepository.add(user);
                        listRepository.add(new List("Today", date, user.Id));
                        listRepository.add(new List("NextDay1", date.AddDays(1), user.Id));
                        listRepository.add(new List("NextDay2", date.AddDays(2), user.Id));
                        listRepository.add(new List("NextDay3", date.AddDays(3), user.Id));
                        listRepository.add(new List("NextDay4", date.AddDays(4), user.Id));
                        listRepository.add(new List("NextDay5", date.AddDays(5), user.Id));
                        listRepository.add(new List("NextDay6", date.AddDays(6), user.Id));
                        listRepository.add(new List("Calendar", date, user.Id));
                        listRepository.add(new List("Notepad", date, user.Id));
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
                    else
                    {
                        Speech1("Проверьте введённые данные");
                        return false;
                    }   
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

#pragma warning disable CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // Член скрывает унаследованный член: отсутствует новое ключевое слово

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
