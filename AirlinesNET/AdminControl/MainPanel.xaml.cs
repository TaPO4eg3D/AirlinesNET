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

namespace AirlinesNET.AdminControl
{
    /// <summary>
    /// Interaction logic for MainPanel.xaml
    /// </summary>
    public partial class MainPanel : Window
    {
        public MainPanel()
        {
            InitializeComponent();
        }

        private void invokeUsers_Click(object sender, RoutedEventArgs e)
        {
            Users users = new Users();
            users.ShowDialog();
        }

        private void invokeCompanies_Click(object sender, RoutedEventArgs e)
        {
            Companies companies = new Companies();
            companies.ShowDialog();
        }

        private void Airports_Click(object sender, RoutedEventArgs e)
        {
            Airports airports = new Airports();
            airports.ShowDialog();
        }

        private void invokeFlights_Click(object sender, RoutedEventArgs e)
        {
            Flights flights = new Flights();
            flights.ShowDialog();
        }

        private void invokePurchases_Click(object sender, RoutedEventArgs e)
        {
            Purchases purchases = new Purchases();
            purchases.ShowDialog();
        }

        private void invokeReports_Click(object sender, RoutedEventArgs e)
        {
            Reports reports = new Reports();
            reports.ShowDialog();
        }
    }
}
