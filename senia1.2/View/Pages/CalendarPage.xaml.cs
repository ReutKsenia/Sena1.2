using senia1._2.Model;
using senia1._2.Repositories;
using senia1._2.ViewModel.UserControls;
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
    /// Логика взаимодействия для CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        public CalendarPage()
        {
            List<string> months = new List<string> { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
            //cboMonth.ItemsSource = months;

            InitializeComponent();
            DataContext = new ViewModel.CalendarPageViewModel();


            for (int i = -50; i < 50; i++)
            {
                cboYear.Items.Add(DateTime.Today.AddYears(i).Year);
            }

            cboMonth.SelectedItem = months.FirstOrDefault(w => w == DateTime.Today.ToString("MMMM"));
            cboYear.SelectedItem = DateTime.Today.Year;

            cboMonth.SelectionChanged += (o, e) => RefreshCalendar();
            cboYear.SelectionChanged += (o, e) => RefreshCalendar();

            getNotesFromDb();
        }

        private void getNotesFromDb()
        {
            EFTaskRepository taskRepository = new EFTaskRepository();

            List<Model.Task> days = taskRepository.getByCategory("Calendar").ToList();

            foreach (Model.Task dbDay in days)
            {
                foreach (senia1._2.ViewModel.UserControls.Day calendarDay in Calendar.Days)
                {
                    if (calendarDay.Date == dbDay.DateExpected)
                    {
                        calendarDay.Notes = dbDay.Value;
                    }
                }
            }
        }

        private void RefreshCalendar()
        {
            if (cboYear.SelectedItem == null) return;
            if (cboMonth.SelectedItem == null) return;

            int year = (int)cboYear.SelectedItem;

            int month = cboMonth.SelectedIndex + 1;

            DateTime targetDate = new DateTime(year, month, 1);

            Calendar.BuildCalendar(targetDate);

            getNotesFromDb();
        }

        private void Calendar_DayChanged(object sender, ViewModel.UserControls.DayChangedEventArgs e)
        {
            ToDoEntities1 ctx = new ToDoEntities1();
            EFListRepository listRepository = new EFListRepository();
            EFTaskRepository taskRepository = new EFTaskRepository();

            var results = (from d in ctx.Task where d.DateExpected == e.Day.Date select d);
            

                if (results.Count() <= 0)
                {
                    Model.Task newTask = new Model.Task();
                    newTask.DateExpected = e.Day.Date;
                    newTask.Value = e.Day.Notes;
                    newTask.ListId = listRepository.getByName("Calendar").id;
                    newTask.Category = "Calendar";

                    taskRepository.add(newTask);
                }
                else
                {
                    Model.Task oldTask = results.FirstOrDefault();
                    oldTask.Value = e.Day.Notes;
                    ctx.SaveChanges();
                    var results2 = (from d in ctx.Task where d.Value == "" select d);
                    if(results2.Count() > 0)
                    {
                        taskRepository.delete(oldTask);
                    }
                }
           
        }
    }
}
