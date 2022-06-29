using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace DB_WPF_CSharp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private static MainWindow _instance;
        public static MainWindow GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MainWindow();
            }
            return _instance;
        }
        public MainWindow()
        {
             InitializeComponent();
        }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            String loginUser = Login.Text,
               passwordUser = Password.Password;
            DB data_base = new DB();

            DataTable dTable = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            data_base.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT id FROM `user` WHERE `login` = @logUsr and `password` = @pasUsr", data_base.getConnection());
            command.Parameters.Add("@logUsr", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@pasUsr", MySqlDbType.VarChar).Value = passwordUser;

            int student_id = (int)command.ExecuteScalar();

            adapter.SelectCommand = command;
            adapter.Fill(dTable);

            if (dTable.Rows.Count > 0)
            {
                MessageBox.Show("Вы успешно вошли!");
            }
            else
                MessageBox.Show("Неправильный логин или пароль");
            data_base.closeConnection();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool formIsMax = false;
        private void Full_Click(object sender, RoutedEventArgs e)
        {
            if (!formIsMax)
            {
                this.WindowState = WindowState.Maximized;

            }
            else
                this.WindowState = WindowState.Normal;
            formIsMax = !formIsMax;
        }

        private void Turn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Registration reg = Registration.GetInstance();
            this.Hide();
            reg.Show();
        }
    }
}
