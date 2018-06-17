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
using System.Diagnostics;

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
            var result = String.Format(@"{0}_{1}_{2}-{3}_{4}_{5}.docx",
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

            var pastPurchases = db.PastPurchases.AsNoTracking().Where(pp => pp.PastFlight.DepartureTime >= start && pp.PastFlight.DepartureTime <= end).ToList();
            string path = String.Format("./Reports/Profit/{0}", formatDate((DateTime)start, (DateTime)end));
            using (var document = DocX.Create(path))
            {
                Formatting firstP = new Formatting();
                firstP.Size = 18;
                firstP.Bold = true;
                Paragraph p = document.InsertParagraph("Отчет о прибыли\n\n", false, firstP);
                p.Alignment = Alignment.center;
                Table table = document.AddTable(pastPurchases.Count+1, 4);
                table.Design = TableDesign.ColorfulList;
                table.Rows[0].Cells[0].Paragraphs.First().Append("Клиент");
                table.Rows[0].Cells[1].Paragraphs.First().Append("Рейс");
                table.Rows[0].Cells[2].Paragraphs.First().Append("Компания");
                table.Rows[0].Cells[3].Paragraphs.First().Append("Стоимость");
                decimal _finalPrice = 0;
                for (int i = 1; i <= pastPurchases.Count; i++)
                {
                    table.Rows[i].Cells[0].Paragraphs.First().Append(pastPurchases[i-1].User.Profile.FullName);
                    table.Rows[i].Cells[1].Paragraphs.First().Append(pastPurchases[i-1].PastFlight.FlightName);
                    table.Rows[i].Cells[2].Paragraphs.First().Append(pastPurchases[i-1].PastFlight.Company.Name);
                    table.Rows[i].Cells[3].Paragraphs.First().Append(pastPurchases[i-1].PastFlight.Price.ToString());
                    _finalPrice += pastPurchases[i - 1].PastFlight.Price;
                }
                document.InsertTable(table);
                document.InsertParagraph("\nИтого прибыли: " + _finalPrice);
                document.Save();
            }
            MessageBox.Show("Успешно!");
            Process.Start("WINWORD.EXE", path);
        }

        private void flightPrint_Click(object sender, RoutedEventArgs e)
        {
            var start = flightStartDate.SelectedDate;
            var end = flightEndDate.SelectedDate;

            if (start == null || end == null)
            {
                MessageBox.Show("Выберите корректные даты!");
                return;
            }
            var pastFlights = db.PastFlights.AsNoTracking().ToList();
            string path = String.Format("./Reports/Flights/{0}", formatDate((DateTime)start, (DateTime)end));
            using (var document = DocX.Create(path))
            {
                Formatting firstP = new Formatting();
                firstP.Size = 18;
                firstP.Bold = true;
                Paragraph p = document.InsertParagraph("Отчет о проведенных рейсах\n\n", false, firstP);
                p.Alignment = Alignment.center;
                Table table = document.AddTable(pastFlights.Count + 1, 7);
                table.Design = TableDesign.ColorfulList;
                table.Rows[0].Cells[0].Paragraphs.First().Append("Номер рейса");
                table.Rows[0].Cells[1].Paragraphs.First().Append("Компания");
                table.Rows[0].Cells[2].Paragraphs.First().Append("Вылет");
                table.Rows[0].Cells[3].Paragraphs.First().Append("Приземление");
                table.Rows[0].Cells[4].Paragraphs.First().Append("Время вылета");
                table.Rows[0].Cells[5].Paragraphs.First().Append("Время прибытия");
                table.Rows[0].Cells[6].Paragraphs.First().Append("Цена");
                for (int i = 1; i <= pastFlights.Count; i++)
                {
                    table.Rows[i].Cells[0].Paragraphs.First().Append(pastFlights[i-1].FlightName);
                    table.Rows[i].Cells[1].Paragraphs.First().Append(pastFlights[i-1].Company.Name);
                    table.Rows[i].Cells[2].Paragraphs.First().Append(pastFlights[i-1].Airport.Name);
                    table.Rows[i].Cells[3].Paragraphs.First().Append(pastFlights[i-1].Airport1.Name);
                    table.Rows[i].Cells[4].Paragraphs.First().Append(pastFlights[i-1].DepartureTime.ToString());
                    table.Rows[i].Cells[5].Paragraphs.First().Append(pastFlights[i-1].ArriveTime.ToString());
                    table.Rows[i].Cells[6].Paragraphs.First().Append(pastFlights[i-1].Price.ToString());
                }
                document.InsertTable(table);
                document.InsertParagraph("\nВсего рейсов: " + pastFlights.Count);
                document.Save();
            }
            MessageBox.Show("Успешно!");
            Process.Start("WINWORD.EXE", path);
        }
    }
}
