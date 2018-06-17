using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AirlinesNET.Models;
using Xceed.Words.NET;

namespace AirlinesNET.AdminControl
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {

        private DataContext db = new Models.DataContext();

        public Reports()
        {
            InitializeComponent();
        }

        private string formatDate(DateTime startDate, DateTime endDate)
        {
            var result = String.Format(@"{0}_{1}_{2}-{3}-{4}-{5}.docx",
                startDate.Year,
                startDate.Month,
                startDate.Day,
                endDate.Year,
                endDate.Month,
                endDate.Day
                );
            return result;
        }

        private void profitPrint_Click(object sender, RoutedEventArgs e)
        {
            var start = profitStartDate.SelectedDate;
            var end = profitEndDate.SelectedDate;

            if(start == null || end == null)
            {
                MessageBox.Show("Выберите корректные даты!");
                return;
            }

            var pastPurchases = db.PastPurchases.Where(pp => pp.PastFlight.DepartureTime >= start && pp.PastFlight.DepartureTime <= end).ToList();
            string path = String.Format("./Reports/Profit/{0}", formatDate((DateTime)start, (DateTime)end));
            using (var document = DocX.Create(path))
            {
                Formatting firstP = new Formatting();
                firstP.Size = 18;
                firstP.Bold = true;
                Paragraph p = document.InsertParagraph("Отчет о прибыли\n\n", false, firstP);
                p.Alignment = Alignment.center;
                Table table = document.AddTable(pastPurchases.Count+1, 3);
                table.Design = TableDesign.ColorfulGrid;
                table.Rows[0].Cells[0].Paragraphs.First().Append("Клиент");
                table.Rows[0].Cells[1].Paragraphs.First().Append("Рейс");
                table.Rows[0].Cells[2].Paragraphs.First().Append("Компания");
                table.Rows[0].Cells[3].Paragraphs.First().Append("Стоимость");
                for (int i = 1; i <= pastPurchases.Count; i++)
                {
                    table.Rows[i].Cells[0].Paragraphs.First().Append(pastPurchases[i-1].User.Profile.FullName);
                    table.Rows[i].Cells[0].Paragraphs.First().Append(pastPurchases[i-1].PastFlight.FlightName);
                    table.Rows[i].Cells[0].Paragraphs.First().Append(pastPurchases[i-1].PastFlight.Company.Name);
                    table.Rows[i].Cells[0].Paragraphs.First().Append(pastPurchases[i-1].PastFlight.Seats.ToString());
                }
                document.Save();
            }

        }
    }
}
