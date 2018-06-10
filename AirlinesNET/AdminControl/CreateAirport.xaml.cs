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

namespace AirlinesNET.AdminControl
{
    /// <summary>
    /// Interaction logic for CreateCompany.xaml
    /// </summary>
    public partial class CreateAirport : Window
    {

        private DataContext db = new DataContext();

        public CreateAirport()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameField.Text;
            string country = countryField.Text;
            string city = cityField.Text;

            var _validation = Shortcuts.checkIfEmptyString(name);
            if (_validation)
            {
                MessageBox.Show("Заполните поля отмеченныее (*)!");
                return;
            }

            if (String.IsNullOrEmpty(country))
                country = null;

            if (String.IsNullOrEmpty(city))
                city = null;

            Airport airport = new Airport
            {
                Name = name,
                Country = country,
                City = city
            };
            db.Airports.Add(airport);
            db.SaveChanges();

            MessageBox.Show("Успешно!");
            this.Close();

        }
    }
}
