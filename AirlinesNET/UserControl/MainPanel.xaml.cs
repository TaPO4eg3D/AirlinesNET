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

namespace AirlinesNET.UserControl
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

        private void availableFlights_Click(object sender, RoutedEventArgs e)
        {
            Flights flights = new Flights();
            flights.ShowDialog();
        }

        private void myPurchases_Click(object sender, RoutedEventArgs e)
        {
            Purchases purchases = new Purchases();
            purchases.ShowDialog();
        }
    }
}
