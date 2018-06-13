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

namespace AirlinesNET.AdminControl
{
    /// <summary>
    /// Interaction logic for AddPurchase.xaml
    /// </summary>
    public partial class AddPurchase : Window
    {

        private DataContext db = new Models.DataContext();

        private void fetchClients()
        {
            var search = searchClientField.Text;
            var users = db.Profiles.Where(p => p.FullName.Contains(search) || p.SeriesAndNumber.Contains(search));
            if (onlyAnon.IsChecked == true)
            {
                users = users.Where(p => p.UserID == null);
            }
            else
            {
                users = users.Where(p => p.UserID != null);
            }
            clientsGrid.ItemsSource = users.ToList();
        }

        private void fetchFlights()
        {
            var search = searchFlightField.Text;
            var flights = db.Flights.Where(f => f.FlightName.Contains(search) || f.Company.Name.Contains(search) || f.Airport.Name.Contains(search) || f.Airport.Country.Contains(search) || f.Airport.City.Contains(search)).ToList();
            flightsGrid.ItemsSource = flights;
        }

        public AddPurchase()
        {
            InitializeComponent();
            fetchClients();
            fetchFlights();
        }

        private void checkBoxTrigger(object sender, RoutedEventArgs e)
        {
            fetchClients();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedFlight = (Flight)flightsGrid.SelectedItem;
            var selectedClient = (Profile)clientsGrid.SelectedItem;
            Purchase purchase = new Purchase
            {
                UserID = (int)selectedClient.UserID,
                FlightID = selectedFlight.FlightID,
                Status = true
            };
            db.Purchases.Add(purchase);
            db.SaveChanges();
            MessageBox.Show("Успешно!");
            this.Close();
        }

        private void searchField_TextChanged(object sender, TextChangedEventArgs e)
        {
            fetchFlights();
        }

        private void searchClientsField_TextChanged(object sender, TextChangedEventArgs e)
        {
            fetchClients();
        }
    }
}
