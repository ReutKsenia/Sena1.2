using senia1._2.ViewModel;
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

namespace senia1._2.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public bool Recognize { get; set; }
        //InputBindingCollection collection;
        MainViewModel main = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = main;
            lists.SelectedItem = null;
            //collection = this.InputBindings;
            //this.InputBindings.Clear();
            
        }

        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Normal)
            {
                SystemCommands.MaximizeWindow(this);
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void MainWindow1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            grid2.Visibility = Visibility.Visible;
            exp.IsExpanded = true;
            TitleList.Focus();
        }

        private void AddList_Click(object sender, RoutedEventArgs e)
        {
            if(TitleList.Text == "")
            {
                grid2.Visibility = Visibility.Collapsed;
            }
            else
            {
                main.addLists(TitleList.Text);
                TitleList.Text = "";
                TitleList.Focus();
            }
        }

        private void Abort_Click(object sender, RoutedEventArgs e)
        {
            grid2.Visibility = Visibility.Collapsed;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lists.SelectedItem != null)
            {
                main.SelectedList(lists.SelectedItem.ToString());
            }
        }

        //private void RecognizeStart_Click_1(object sender, RoutedEventArgs e)
        //{
        //    if(RecognizeStart.IsChecked != true)
        //    {
        //        for(int i=0; i<collection.Count; i++)
        //        {
        //            this.InputBindings.Add(collection[i]);
        //        }
        //    }
        //    else
        //    {
        //        this.InputBindings.Clear();
        //    }
        //}
    }
}
