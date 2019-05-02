using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System;

namespace senia1._2.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Page Today;
        private Page NextSevenDays;
        private Page Calendar;

        private Page _currentPage;
        public Page CurrentPage { get { return _currentPage; } set { _currentPage = value; RaisePropertyChanged(() => CurrentPage); } }

        private double _frameOpacity;
        public double FrameOpacity { get { return _frameOpacity; } set { _frameOpacity= value; RaisePropertyChanged(() => FrameOpacity); } }

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        private DateTime date = DateTime.Now;
        private string _day;
        public string Day { get { return _day; } set { _day = value; RaisePropertyChanged(() => Day); } }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Today = new View.Pages.TodayPage();
            NextSevenDays = new View.Pages.NextSevenDaysPage();
            Calendar = new View.Pages.CalendarPage();

            FrameOpacity = 1;
            CurrentPage = Today;

            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, e) => { Day = date.ToString("dd"); };
            timer.Start();
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

        private async void ShowOpacity(Page page)
        {
            await Task.Factory.StartNew(() =>
            {
                for(double i = 1.0; i > 0.0; i -= 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(50);
                }
                CurrentPage = page;
                for (double i = 0.0; i < 1.1; i += 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(50);
                }
            });
        }
    }
}