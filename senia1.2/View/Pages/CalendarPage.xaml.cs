using senia1._2.Model;
using senia1._2.Repositories;
using senia1._2.ViewModel.UserControls;
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
    /// Логика взаимодействия для CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        UnitOfWork unit = new UnitOfWork();
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
            var result = unit.List.getByUserId(CurrentUser.User.Id).ToList();
            for(int i = 0; i<result.Count(); i++)
            {
                if (result[i].Title == "Calendar")
                {
                    List<Model.Task> days = unit.Task.getByListId(result[i].id).ToList();
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

            var results = (from d in ctx.Task where d.DateExpected == e.Day.Date select d);
            

                if (results.Count() <= 0)
                {
                    Model.Task newTask = new Model.Task();
                    newTask.DateExpected = e.Day.Date;
                    newTask.Value = e.Day.Notes;
                    var result = unit.List.getByUserId(CurrentUser.User.Id).ToList();
                    for(int i = 0; i < result.Count(); i++)
                    {
                        if(result[i].Title == "Calendar")
                        {
                            newTask.ListId = result[i].id;
                            newTask.Category = "Calendar";
                            unit.Task.add(newTask);
                        }
                    }
                }
                else
                {
                    Model.Task oldTask = results.FirstOrDefault();
                    oldTask.Value = e.Day.Notes;
                    ctx.SaveChanges();
                    var results2 = (from d in ctx.Task where d.Value == "" select d);
                    if(results2.Count() > 0)
                    {
                        unit.Task.delete(oldTask);
                    }
                }
           
        }
    }
}
