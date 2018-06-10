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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AirlinesNET.Models;
using AirlinesNET.Utilities;

namespace AirlinesNET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DataContext db = new DataContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void toRegistrationLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginField.Text;
            string password = passwordField.Password;

            bool _validation = Shortcuts.checkIfEmptyString(login, password);
            if (_validation)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            password = Shortcuts.hashPassword(password);
            var user = db.Users.Where(u => u.Username == login && u.Password == password).FirstOrDefault();

            if(user == null)
            {
                MessageBox.Show("Неверный логин или пароль!");
                return;
            }

            this.Hide();
            switch (user.Role)
            {
                case 0:
                    break;
                case 1:
                    AdminControl.MainPanel mainPanel = new AdminControl.MainPanel();
                    mainPanel.Show();
                    mainPanel.Closed += MainPanel_Closed;
                    break;
                default:
                    this.Show();
                    break;
            }
            

        }

        private void MainPanel_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
