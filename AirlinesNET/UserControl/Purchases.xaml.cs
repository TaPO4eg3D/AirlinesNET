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
using Xceed.Words.NET;
using System.IO;
using System.Diagnostics;

namespace AirlinesNET.UserControl
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
                       where p.UserID == Utilities.Shortcuts.AuthUserID && (p.Flight.FlightName.Contains(searchText) || db.Profiles.Where(pp => pp.UserID == p.UserID).FirstOrDefault().FullName.Contains(searchText))
                       select p;
            mainGrid.ItemsSource = data.ToList();
        }

        private void searchField_TextChanged(object sender, TextChangedEventArgs e)
        {
            fetchData();
        }

        public Purchases()
        {
            InitializeComponent();
            fetchData();
        }


        private void deleteEntity_Click(object sender, RoutedEventArgs e)
        {
            var selectedEntities = mainGrid.SelectedItems.Cast<Purchase>().ToList();
            if(selectedEntities.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одну покупку!");
                return;
            }

            var confirmation = MessageBox.Show("Вы уверены, что хотите удалить выбранную покупку?", "Подтвердите удаление", MessageBoxButton.YesNo);
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

    }
}
