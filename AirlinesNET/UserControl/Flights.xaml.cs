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
using System.Windows.Shapes;
using AirlinesNET.Models;
using AirlinesNET.Utilities;

namespace AirlinesNET.UserControl
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Flights : Window
    {

        private DataContext db = new DataContext();

        private void fetchData()
        {
            string searchText = searchField.Text;
            var data = from p in db.Flights.AsNoTracking()
                       where p.FlightName.Contains(searchText) || p.Company.Name.Contains(searchText) || p.Airport.Name.Contains(searchText) || p.Airport.Country.Contains(searchText) || p.Airport.City.Contains(searchText)
                       select p;
            mainGrid.ItemsSource = data.ToList();
        }

        private void searchField_TextChanged(object sender, TextChangedEventArgs e)
        {
            fetchData();
        }

        public Flights()
        {
            InitializeComponent();
            fetchData();
        }

        private void addEntity_Click(object sender, RoutedEventArgs e)
        {
            var selectedFlight = mainGrid.SelectedItem as Flight;
            if(selectedFlight == null)
            {
                MessageBox.Show("Выберите рейс!");
                return;
            }

            if (selectedFlight.SeatsLeft <= 0)
            {
                MessageBox.Show("Мест на этот рейс не осталось!");
                return;
            }

            Purchase purchase = new Purchase
            {
                FlightID = selectedFlight.FlightID,
                UserID = Shortcuts.AuthUserID,
                Status = false
            };
            db.Purchases.Add(purchase);
            try
            {
                db.SaveChanges();
            }
            catch {
                MessageBox.Show("Вы уже покупали билет на этот рейс!");
                return;
            }
            MessageBox.Show("Заявка успешно принята!");
            fetchData();
        }

    }
}
