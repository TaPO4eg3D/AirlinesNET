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
    /// Interaction logic for AddAnonUser.xaml
    /// </summary>
    public partial class AddAnonUser : Window
    {

        private DataContext db = new DataContext();

        public AddAnonUser()
        {
            InitializeComponent();
            var documents = db.DocumentTypes.ToList();
            selectDocument.ItemsSource = documents;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = fullNameField.Text;
            string phone = phoneNumberField.Text;
            string address = addressField.Text;
            string serial = serialField.Text;

            var _validation = Shortcuts.checkIfEmptyString(fullName, phone, address, serial);
            if (_validation || selectDocument.SelectedItem == null)
            {
                MessageBox.Show("Все поля обязательны!");
                return;
            }

            var documentType = (DocumentType)selectDocument.SelectedItem;

            Profile profile = new Profile
            {
                FullName = fullName,
                DocumentType = documentType.DocumentTypeID,
                SeriesAndNumber = serial,
                Address = address,
                PhoneNumber = phone
            };
            db.Profiles.Add(profile);
            db.SaveChanges();

            MessageBox.Show("Успешно!");
            this.Close();
        }
    }
}
