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
    public partial class Companies : Window
    {

        private DataContext db = new DataContext();

        private void fetchData()
        {
            string searchText = searchField.Text;
            var data = from p in db.Companies
                       where p.Name.Contains(searchText) || p.PhoneNumber.Contains(searchText)
                       select p;
            if (onlyPhone.IsChecked == true)
            {
                data = data.Where(d => d.PhoneNumber != null);
            }
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

        public Companies()
        {
            InitializeComponent();
            fetchData();
        }

        private void addEntity_Click(object sender, RoutedEventArgs e)
        {
            CreateCompany createCompany = new CreateCompany();
            createCompany.ShowDialog();
            fetchData();
        }

        private void deleteEntity_Click(object sender, RoutedEventArgs e)
        {
            var selectedEntities = mainGrid.SelectedItems.Cast<Company>().ToList();
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
                var entity = db.Companies.Where(u => u.CompanyID == _entity.CompanyID).FirstOrDefault();
                db.Companies.Remove(entity);
            }
            db.SaveChanges();
            fetchData();
        }

        private void mainGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                var _selectedItem = (Company)e.Row.Item;
                var selectedItem = db.Companies.Where(p => p.CompanyID == _selectedItem.CompanyID).FirstOrDefault();

                selectedItem = _selectedItem;
                db.SaveChanges();
            }
            catch { }
        }

    }
}
