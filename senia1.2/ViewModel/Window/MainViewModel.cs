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

namespace senia1._2.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Page Today;
        private Page NextSevenDays;
        private Page Calendar;
        //private Page Notepad;
        private Page List;

        private EFUserRepository userRepository = new EFUserRepository();
        private EFListRepository listRepository = new EFListRepository();

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

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
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

        

        public MainViewModel()
        {
            
            Today = new View.Pages.TodayPage();
            NextSevenDays = new View.Pages.NextSevenDaysPage();
            Calendar = new View.Pages.CalendarPage();
            List = new View.Pages.ListPage();

            FrameOpacity = 1;
            CurrentPage = Today;

            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, e) => { Day = date.ToString("dd"); };
            timer.Start();

            UserName = CurrentUser.User.UserName;
            if(ConvertByteArrayToImage(CurrentUser.User.Foto) != null)
            {
                Image = ConvertByteArrayToImage(CurrentUser.User.Foto);
            }
            Lists = listRepository.getNameByUserId(CurrentUser.User.Id);
        }

        public ICommand MenuToday_Click
        {
            get
            {
                return new RelayCommand(() => ShowOpacity(Today));
            }
        }

        public ICommand MenuNextSevanDays_Click
        {
            get
            {
                return new RelayCommand(() => ShowOpacity(NextSevenDays));
            }
        }

        public ICommand MenuCalendar_Click
        {
            get
            {
                return new RelayCommand(() => ShowOpacity(Calendar));
            }
        }

        public void SelectedList(string title)
        {
            ListPageViewMdel listPageView = (ListPageViewMdel)List.DataContext;
            listPageView.Title1 = title;
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
            userRepository.update(CurrentUser.User, CurrentUser.User);
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

        public void addLists(string title)
        {
            listRepository.add(new List(title, date, CurrentUser.User.Id));
            Lists = listRepository.getNameByUserId(CurrentUser.User.Id);
        }
    }
}