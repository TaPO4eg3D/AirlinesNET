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
    public partial class CreateCompany : Window
    {

        private DataContext db = new DataContext();

        public CreateCompany()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameField.Text;
            string phone = phoneField.Text;

            var _validation = Shortcuts.checkIfEmptyString(name);
            if (_validation)
            {
                MessageBox.Show("Заполните поля отмеченныее (*)!");
                return;
            }

            if (String.IsNullOrEmpty(phone))
                phone = null;

            Company company = new Company
            {
                Name = name,
                PhoneNumber = phone
            };
            db.Companies.Add(company);
            db.SaveChanges();

            MessageBox.Show("Успешно!");
            this.Close();

        }
    }
}
