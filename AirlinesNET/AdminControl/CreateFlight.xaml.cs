﻿using System;
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
using Xceed.Wpf;

namespace AirlinesNET.AdminControl
{
    /// <summary>
    /// Interaction logic for EditFlight.xaml
    /// </summary>
    public partial class CreateFlight : Window
    {

        private DataContext db = new DataContext();

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

        public CreateFlight()
        {
            InitializeComponent();
            fetchCompanies();
            fetchAirports();

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCompany = (Company)companyGrid.SelectedItem;
            var selectedStartPoint = (Airport)startPointGrid.SelectedItem;
            var selectedEndPoint = (Airport)endPointGrid.SelectedItem;
            var departure = departureTime.Value;
            var arrive = arriveTime.Value;
            var price = Convert.ToDecimal(priceField.Text);

            Flight flight = new Flight
            {
                CompanyID = selectedCompany.CompanyID,
                StartPoint = selectedStartPoint.AirportID,
                EndPoint = selectedEndPoint.AirportID,
                Seats = Convert.ToInt32(seatsField.Text),
                FlightName = nameField.Text,
                DepartureTime = (DateTime)departure,
                ArriveTime = (DateTime)arrive,
                Price = price
            };

            db.Flights.Add(flight);
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
