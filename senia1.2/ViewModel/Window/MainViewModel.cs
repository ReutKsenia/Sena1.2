using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.Collections.ObjectModel;
using senia1._2.Model;
using senia1._2.ViewModel.Window;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Win32;
using senia1._2.Repositories;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using senia1._2.ViewModel.Pages;
using System.Linq;

namespace senia1._2.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region пол€ и свойства
        private Page Today;
        private Page NextSevenDays;
        private Page Calendar;
        private Page Notepad;
        private View.Pages.ListPage List;
        private Page Welcome;
        private Page Options;

        Speech.Speech speech;
        UnitOfWork unit = new UnitOfWork();

        private Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; RaisePropertyChanged(() => CurrentPage); }
        }

        private double _frameOpacity;
        public double FrameOpacity
        {
            get { return _frameOpacity; }
            set { _frameOpacity= value; RaisePropertyChanged(() => FrameOpacity); }
        }

        private DateTime date = DateTime.Now;
        private string _day;
        public string Day
        {
            get { return _day; }
            set { _day = value; RaisePropertyChanged(() => Day); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged(() => UserName); }
        }

        private BitmapImage _image;
        public BitmapImage  Image
        {
            get { return _image; }
            set { _image = value; RaisePropertyChanged(() => Image); }
        }

        private List<string> lists;
        public List<string> Lists
        {
            get { return lists; }
            set { lists = value; RaisePropertyChanged(() => Lists); }
        }
        #endregion

        public MainViewModel()
        {
            Today = new View.Pages.TodayPage();
            NextSevenDays = new View.Pages.NextSevenDaysPage();
            Calendar = new View.Pages.CalendarPage();
            List = new View.Pages.ListPage();
            Notepad = new View.Pages.NotepadPage();
            Welcome = new View.Pages.WelcomePage();
            Options = new View.Pages.OptionsPage();

            FrameOpacity = 1;
            CurrentPage = Welcome;

            Day = date.ToString("dd");
            UserName = CurrentUser.User.UserName;
            if(ConvertByteArrayToImage(CurrentUser.User.Foto) != null)
            {
                Image = ConvertByteArrayToImage(CurrentUser.User.Foto);
            }
            Lists = unit.List.getNameByUserId(CurrentUser.User.Id);

            List.DeleteList.Click += (o, a) =>
            {
                //var result = unit.List.getByName(List.listPageViewMdel.Title1);
                var result = unit.List.getByUserId(CurrentUser.User.Id).ToList();
                for(int i = 0; i<result.Count(); i++)
                {
                    if(result[i].Title == List.listPageViewMdel.Title1)
                    {
                        unit.List.delete(result[i]);
                        var allTasks = unit.Task.getByListId(result[i].id).ToList();
                        for (int j = 0; j < allTasks.Count(); j++)
                        {
                            unit.Task.delete(allTasks[j]);
                        }
                    }
                }
                
                Lists = unit.List.getNameByUserId(CurrentUser.User.Id);
                CurrentPage = Today;
            };
            speech = new Speech.Speech();
            speech.SpeechSynthesis("–ада вас приветствовать. „ем хотите зан€тьс€?");
        }

        #region команды
        public ICommand MenuToday_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if(CurrentPage != Today)
                    {
                        ShowOpacity(Today);
                    }
                });
            }
        }

        public ICommand MenuNextSevanDays_Click
        {
            get
            {
                return new RelayCommand(() => 
                {
                    if (CurrentPage != NextSevenDays)
                    {
                        ShowOpacity(NextSevenDays);
                    }
                });
            }
        }

        public ICommand MenuCalendar_Click
        {
            get
            {
                return new RelayCommand(() => 
                {
                    if (CurrentPage != Calendar)
                    {
                        ShowOpacity(Calendar);
                    }
                });
            }
        }

        public ICommand MenuNotepad_Click
        {
            get
            {
                return new RelayCommand(() => 
                {
                    if (CurrentPage != Notepad)
                    {
                        ShowOpacity(Notepad);
                    }
                });
            }
        }

        public ICommand Options_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (CurrentPage != Options)
                    {
                        ShowOpacity(Options);
                    }
                });
            }
        }

        public ICommand AddFoto_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    addFoto();
                    Image = ConvertByteArrayToImage(CurrentUser.User.Foto);
                });
            }
        }

        public ICommand GetTime
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var time = DateTime.Now;
                    speech.SpeechSynthesis(time.ToString("HH mm"));
                });
            }
        }
        #endregion

        #region методы
        public void SelectedList(string title)
        {
            ListPageViewMdel listPageView = (ListPageViewMdel)List.DataContext;
            listPageView.Title1 = title;
            List.getTasks();
            CurrentPage = List;
        }

        private async void ShowOpacity(Page page)
        {
            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                for(double i = 1.0; i > 0.0; i -= 0.2)
                {
                    FrameOpacity = i;
                    Thread.Sleep(50);
                }
                CurrentPage = page;
                for (double i = 0.0; i < 1.1; i += 0.2)
                {
                    FrameOpacity = i;
                    Thread.Sleep(50);
                }
            });
        }

        public BitmapImage ConvertByteArrayToImage(byte[] array)
        {
            if (array != null)
            {
                using (var ms = new MemoryStream(array, 0, array.Length))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            }
            return null;
        }

        private byte[] ConvertImageToByteArray(string fileName)
        {
            Bitmap bitMap = new Bitmap(fileName);
            ImageFormat bmpFormat = bitMap.RawFormat;
            var imageToConvert = System.Drawing.Image.FromFile(fileName);
            using (MemoryStream ms = new MemoryStream())
            {
                imageToConvert.Save(ms, bmpFormat);
                return ms.ToArray();
            }
        }

        private void addFoto()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "";
            dlg.Filter = "Image files (*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp|All Files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                CurrentUser.User.Foto = ConvertImageToByteArray(selectedFileName);
            }
            unit.User.update(CurrentUser.User, CurrentUser.User);
        }

        public void addLists(string title)
        {
            unit.List.add(new List(title, date, CurrentUser.User.Id));
            Lists = unit.List.getNameByUserId(CurrentUser.User.Id);
        }
        #endregion
    }
}