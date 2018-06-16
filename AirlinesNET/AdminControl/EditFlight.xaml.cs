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
    /// Interaction logic for EditFlight.xaml
    /// </summary>
    public partial class EditFlight : Window
    {

        private DataContext db = new DataContext();
        private int selectedFlight { get; set; }

        /// <summary>
        /// Fetch all companies
        /// </summary>
        private void fetchCompanies()
        {
            var companies = db.Companies.Where(c => c.Name.Contains(companySearch.Text)).ToList();
            companyGrid.ItemsSource = companies;
        }

        /// <summary>
        /// Fetch all airports
        /// </summary>
        private void fetchAirports()
        {
            var airports = db.Airports.Where(c => c.Name.Contains(pointsSeacrh.Text) || c.Country.Contains(pointsSeacrh.Text) || c.City.Contains(pointsSeacrh.Text)).ToList();
            startPointGrid.ItemsSource = airports;
            endPointGrid.ItemsSource = airports;
        }

        public EditFlight(int selectedFlight)
        {
            InitializeComponent();
            this.selectedFlight = selectedFlight;
            fetchCompanies();
            fetchAirports();

            // Select current values
            var _selectedFlight = db.Flights.Where(f => f.FlightID == selectedFlight).FirstOrDefault();
            var selectedCompany = _selectedFlight.Company;
            var selectedStartPoint = _selectedFlight.Airport; 
            var selectedEndPoint = _selectedFlight.Airport1;

            companyGrid.SelectedItem = selectedCompany;
            startPointGrid.SelectedItem = selectedStartPoint;
            endPointGrid.SelectedItem = selectedEndPoint;
            seatsField.Text = _selectedFlight.Seats.ToString();
            nameField.Text = _selectedFlight.FlightName;
            departureTime.Value = _selectedFlight.DepartureTime;
            arriveTime.Value = _selectedFlight.ArriveTime;

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var flight = db.Flights.Where(f => f.FlightID == selectedFlight).FirstOrDefault();

            Company company = (Company)companyGrid.SelectedItem;
            Airport startPoint = (Airport)startPointGrid.SelectedItem;
            Airport endPoint = (Airport)endPointGrid.SelectedItem;
            var departure = departureTime.Value;
            var arrive = arriveTime.Value;

            int seats;
            try
            {
                seats = Convert.ToInt32(seatsField.Text);
            }
            catch
            {
                MessageBox.Show("Вы не ввели количество сидений!");
                return;
            }
             
            flight.CompanyID = company.CompanyID;
            flight.StartPoint = startPoint.AirportID;
            flight.EndPoint = endPoint.AirportID;
            flight.Seats = seats;
            flight.FlightName = nameField.Text;
            flight.DepartureTime = (DateTime)departure;
            flight.ArriveTime = (DateTime)arrive;

            db.SaveChanges();

            MessageBox.Show("Успешно!");
            this.Close();
        }

        // Triggers search of Airports
        private void pointsSeacrh_TextChanged(object sender, TextChangedEventArgs e)
        {
            fetchAirports();
        }

        // Triggers search of Companies
        private void companySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            fetchCompanies();
        }
    }
}
