using senia1._2.Model;
using senia1._2.Repositories;
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
    /// Логика взаимодействия для NotepadPage.xaml
    /// </summary>
    public partial class NotepadPage : Page
    {
        UnitOfWork unit = new UnitOfWork();
        public NotepadPage()
        {
            InitializeComponent();
            (Notepad.Document.Blocks.FirstBlock).LineHeight = 2;
            this.Font.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            for (var i = 1; i < 72; i++)
            {
                FontSize.Items.Add((double)i);
            }

            var res = unit.List.getByUserId(CurrentUser.User.Id).ToList();
            for (int k = 0; k < res.Count(); k++)
            {
                if (res[k].Title == "Notepad")
                {
                    var result = unit.Task.getByListId(res[k].id).ToList();
                    if (result.Count() > 0)
                    {
                        Notepad.Document.Blocks.Clear();
                        Notepad.Document.Blocks.Add(new Paragraph(new Run(result[0].Value)));
                    }
                }
            }
        }

        private void FontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSize.SelectedItem != null && !Notepad.Selection.IsEmpty)
            {
                Notepad.Selection.ApplyPropertyValue(
                    TextBlock.FontSizeProperty, FontSize.SelectedItem);
            }
        }

        private void Notepad_SelectionChanged(object sender, RoutedEventArgs e)
        {
            FontSize.SelectedValue = (double)Notepad.Selection.GetPropertyValue(TextBlock.FontSizeProperty);
            ToDoEntities1 ctx = new ToDoEntities1();
            string richText = new TextRange(Notepad.Document.ContentStart, Notepad.Document.ContentEnd).Text;
            DateTime date = DateTime.Now;

            var result1 = unit.List.getByUserId(CurrentUser.User.Id).ToList();
            for (int i = 0; i < result1.Count(); i++)
            {
                if (result1[i].Title == "Notepad")
                {
                    var result = unit.Task.getByListId(result1[i].id);
                    if (richText != "")
                    {
                        if (result.Count() <= 0)
                        {
                            unit.Task.add(new Model.Task(richText, "Notepad", date, result1[i].id));
                        }
                        else
                        {
                            Model.Task oldTask = result.FirstOrDefault();
                            unit.Task.update(oldTask, new Model.Task(richText, "Notepad", date, result1[i].id));
                        }
                    }
                }
            }
            //var result = unit.Task.getByCategory("Notepad");
            //if(richText != "")
            //{
            //    if (result.Count() <= 0)
            //    {
            //        unit.Task.add(new Model.Task(richText, "Notepad", date, unit.List.getByName("Notepad").id));
            //    }
            //    else
            //    {
            //        Model.Task oldTask = result.FirstOrDefault();
            //        unit.Task.update(oldTask, new Model.Task(richText, "Notepad", date, unit.List.getByName("Notepad").id));
            //    }
            //}
        }
    }
}
