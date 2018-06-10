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

namespace AirlinesNET
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {

        private DataContext db = new DataContext();

        public RegisterWindow()
        {
            InitializeComponent();
            var documents = db.DocumentTypes.ToList();
            selectDocumentType.ItemsSource = documents;
            selectDocumentType.SelectedItem = documents[0];
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginField.Text;
            string password = passwordField.Password;
            string repeatPassword = repeatPasswordField.Password;

            bool _validation = Shortcuts.checkIfEmptyString(login, password, repeatPassword);
            if (_validation)
            {
                MessageBox.Show("Заполните поля логина и пароля!");
                return;
            }

            if(password != repeatPassword)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            string fullName = fullNameField.Text;
            int documentType = ((DocumentType)selectDocumentType.SelectedItem).DocumentTypeID;
            string seriesAndNumber = seriesAndNumberField.Text;
            string address = addressField.Text;
            string phoneNumber = phoneField.Text;

            _validation = Shortcuts.checkIfEmptyString(fullName, seriesAndNumber, address, phoneNumber);
            if (_validation)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            User user = new User
            {
                Username = login,
                Password = Shortcuts.hashPassword(password),
                Role = 0
            };

            db.Users.Add(user);
            db.SaveChanges();

            Profile profile = new Profile
            {
                FullName = fullName,
                DocumentType = documentType,
                SeriesAndNumber = seriesAndNumber,
                Address = address,
                PhoneNumber = phoneNumber,
                UserID = user.UserID
            };

            db.Profiles.Add(profile);
            db.SaveChanges();

            MessageBox.Show("Успешно!");
            this.Close();

        }
    }
}
