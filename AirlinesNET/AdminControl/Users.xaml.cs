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
    public partial class Users : Window
    {

        private DataContext db = new DataContext();

        private void fetchData()
        {
            string searchText = searchField.Text;
            var data = from p in db.Profiles
                       where (p.FullName.Contains(searchText) || p.SeriesAndNumber.Contains(searchText))
                       select p;

            if (onlyAnon.IsChecked == true)
            {
                data = data.Where(d => d.UserID == null);
            }
            else
            {
                data = data.Where(d => d.UserID != null);
            }

            if (onlyPassport.IsChecked == true)
            {
                data = data.Where(d => d.DocumentType == 0);
            }
            if (onlyEmployee.IsChecked == true)
            {
                data = data.Where(d => d.User.Role != 0);
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

        public Users()
        {
            InitializeComponent();
            fetchData();
        }

        private void addEntity_Click(object sender, RoutedEventArgs e)
        {
            AddAnonUser addAnon = new AddAnonUser();
            addAnon.Show();
        }

        private void deleteEntity_Click(object sender, RoutedEventArgs e)
        {
            var selectedEntities = mainGrid.SelectedItems.Cast<Profile>().ToList();
            if(selectedEntities.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одного пользователя!");
                return;
            }

            var confirmation = MessageBox.Show("Вы уверены, что хотите удалить выбраных пользователей?", "Подтвердите удаление", MessageBoxButton.YesNo);
            if(confirmation == MessageBoxResult.No)
            {
                return;
            }
            

            foreach(var _entity in selectedEntities)
            {
                if (_entity.UserID == null)
                {
                    var entityProf = db.Profiles.Where(p => p.ProfileID == _entity.ProfileID);
                    db.Profiles.RemoveRange(entityProf);
                    continue;
                }
                var entity = db.Users.Where(u => u.UserID == _entity.UserID).FirstOrDefault();
                db.Users.Remove(entity);
            }
            db.SaveChanges();
            fetchData();
        }

        private void selectDocument_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var _selectedItem = (Profile)mainGrid.SelectedItem;
                var selectedItem = db.Profiles.Where(p => p.ProfileID == _selectedItem.ProfileID).FirstOrDefault();

                int newDocType = (int)((ComboBox)sender).SelectedValue;

                selectedItem = _selectedItem;
                selectedItem.DocumentType = newDocType;
                db.SaveChanges();
            }
            catch { }
        }

        private void mainGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                var _selectedItem = (Profile)e.Row.Item;
                var selectedItem = db.Profiles.Where(p => p.ProfileID == _selectedItem.ProfileID).FirstOrDefault();

                selectedItem = _selectedItem;
                db.SaveChanges();
            }
            catch { }
        }

    }
}
