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
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Flights : Window
    {

        private DataContext db = new DataContext();

        /// <summary>
        /// Fetch data from database, include user's filter
        /// </summary>
        private void fetchData()
        {
            string searchText = searchField.Text;
            var data = from p in db.Flights.AsNoTracking()
                       where p.FlightName.Contains(searchText) || p.Company.Name.Contains(searchText) || p.Airport.Name.Contains(searchText) || p.Airport.Country.Contains(searchText) || p.Airport.City.Contains(searchText)
                       select p;
            mainGrid.ItemsSource = data.ToList();
        }

        /// <summary>
        /// Triggers filter(searchField)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchField_TextChanged(object sender, TextChangedEventArgs e)
        {
            fetchData();
        }

        /// <summary>
        /// Triggers filter(checkBox)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxTrigger(object sender, RoutedEventArgs e)
        {
            fetchData();
        }

        public Flights()
        {
            InitializeComponent();
            fetchData();
        }

        /// <summary>
        /// Add new entity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addEntity_Click(object sender, RoutedEventArgs e)
        {
            CreateFlight createFlight = new CreateFlight();
            createFlight.ShowDialog();
            fetchData();
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteEntity_Click(object sender, RoutedEventArgs e)
        {
            var selectedEntities = mainGrid.SelectedItems.Cast<Flight>().ToList();
            if(selectedEntities.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы один рейс!");
                return;
            }

            var confirmation = MessageBox.Show("Вы уверены, что хотите удалить выбраные рейсы?", "Подтвердите удаление", MessageBoxButton.YesNo);
            if(confirmation == MessageBoxResult.No)
            {
                return;
            }
            
            foreach(var entity in selectedEntities)
            {
                var flight = db.Flights.Where(f => f.FlightID == entity.FlightID);
                db.Flights.RemoveRange(flight);
            }
            db.SaveChanges();

            fetchData();
        }

        /// <summary>
        /// Manage editing grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                var _selectedItem = (Flight)e.Row.Item;
                var selectedItem = db.Flights.Where(p => p.FlightID == _selectedItem.FlightID).FirstOrDefault();

                selectedItem = _selectedItem;
                db.SaveChanges();
            }
            catch { }
        }

        /// <summary>
        /// Edit entity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editEntity_Click(object sender, RoutedEventArgs e)
        {
            var selectedFlight = (Flight)mainGrid.SelectedItem;
            if(selectedFlight == null)
            {
                MessageBox.Show("Выберите рейс!");
                return;
            }
            EditFlight editFlight = new EditFlight(selectedFlight.FlightID);
            editFlight.ShowDialog();
            fetchData();
        }

        private void archivedEntity_Click(object sender, RoutedEventArgs e)
        {
            ArchivedFlights archivedFlights = new ArchivedFlights();
            archivedFlights.ShowDialog();
        }

        private void addToArchive_Click(object sender, RoutedEventArgs e)
        {
            var selectedFlights = mainGrid.SelectedItems.Cast<Flight>().ToList();
            if (selectedFlights.Count == 0)
            {
                MessageBox.Show("Выберите рейс(ы)!");
                return;
            }

            var confirmation = MessageBox.Show("Вы уверены, что хотите удалить выбраные рейсы?", "Подтвердите удаление", MessageBoxButton.YesNo);
            if (confirmation == MessageBoxResult.No)
            {
                return;

            }

            foreach(var selectedFlight in selectedFlights)
            {
                PastFlight pastFlight = new PastFlight
                {
                    FlightID = selectedFlight.FlightID,
                    FlightName = selectedFlight.FlightName,
                    CompanyID = selectedFlight.CompanyID,
                    StartPoint = selectedFlight.StartPoint,
                    EndPoint = selectedFlight.EndPoint,
                    DepartureTime = selectedFlight.DepartureTime,
                    ArriveTime = selectedFlight.ArriveTime,
                    Seats = selectedFlight.Seats,
                    Price = selectedFlight.Price
                };
                db.PastFlights.Add(pastFlight);
                db.SaveChanges();
                var purchasesQuery = db.Purchases.Where(p => p.FlightID == selectedFlight.FlightID);
                var purchases = purchasesQuery.ToList();
                foreach (var purchase in purchases)
                {
                    PastPurchase pastPurchase = new PastPurchase
                    {
                        UserID = purchase.UserID,
                        FlightID = purchase.FlightID,
                        Status = purchase.Status
                    };
                    db.PastPurchases.Add(pastPurchase);
                }
                if (purchases.Count == 0)
                {
                    db.Purchases.RemoveRange(purchasesQuery);
                }
                db.Flights.Attach(selectedFlight);
                db.Flights.Remove(selectedFlight);
                db.SaveChanges();
            }
            MessageBox.Show("Успешно перенесено в архив!");
            fetchData();
        }
    }
}
