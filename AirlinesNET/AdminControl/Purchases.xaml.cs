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
    public partial class Purchases : Window
    {

        private DataContext db = new DataContext();

        private void fetchData()
        {
            string searchText = searchField.Text;
            var data = from p in db.Purchases
                       where p.Flight.FlightName.Contains(searchText) || db.Profiles.Where(pp => pp.UserID == p.UserID).FirstOrDefault().FullName.Contains(searchText)
                       select p;
            mainGrid.ItemsSource = data.ToList();
        }

        private void searchField_TextChanged(object sender, TextChangedEventArgs e)
        {
            fetchData();
        }

        private void checkBoxTrigger(object sender, RoutedEventArgs e)
        {
            fetchData();
        }

        public Purchases()
        {
            InitializeComponent();
            fetchData();
        }

        private void addEntity_Click(object sender, RoutedEventArgs e)
        {
            AddPurchase addPurchase = new AddPurchase();
            addPurchase.ShowDialog();
            fetchData();
        }

        private void deleteEntity_Click(object sender, RoutedEventArgs e)
        {
            var selectedEntities = mainGrid.SelectedItems.Cast<Purchase>().ToList();
            if(selectedEntities.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одну компанию!");
                return;
            }

            var confirmation = MessageBox.Show("Вы уверены, что хотите удалить выбраные компании?", "Подтвердите удаление", MessageBoxButton.YesNo);
            if(confirmation == MessageBoxResult.No)
            {
                return;
            }
            
            foreach(var _entity in selectedEntities)
            {
                db.Purchases.Attach(_entity);
                db.Purchases.Remove(_entity);
            }
            db.SaveChanges();
            fetchData();
        }

        private void mainGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                var _selectedItem = (Airport)e.Row.Item;
                var selectedItem = db.Airports.Where(p => p.AirportID == _selectedItem.AirportID).FirstOrDefault();

                selectedItem = _selectedItem;
                db.SaveChanges();
            }
            catch { }
        }

    }
}
