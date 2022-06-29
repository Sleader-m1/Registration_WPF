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
using System.Windows.Shapes;

namespace DB_WPF_CSharp
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private static Registration _instance;
        public static Registration GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Registration();
            }
            return _instance;
        }
        private Registration()
        {
            InitializeComponent();
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
            String regLogin = Login.Text, regPassword1 = Password.Text,
                regPassword2 = Password_rep.Text, firstName = First_name.Text,
                lastName = Last_name.Text;


            if (regPassword1 != regPassword2)
            {
                MessageBox.Show("Пароли не совпадают!");
            }
            else
            {
                DB data_base = new DB();

                DataTable dTable = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM `user` WHERE `login` = @newLog", data_base.getConnection());
                command.Parameters.Add("@newLog", MySqlDbType.VarChar).Value = regLogin;

                adapter.SelectCommand = command;
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                }
                else
                {
                    data_base.openConnection();
                    {
                        command = new MySqlCommand("INSERT INTO `user` (login, password) VALUES (@regLog, @regPass)", data_base.getConnection());
                        command.Parameters.Add("@regLog", MySqlDbType.VarChar).Value = regLogin;
                        command.Parameters.Add("@regPass", MySqlDbType.VarChar).Value = regPassword1;
                        command.ExecuteNonQuery();
                    }
                    {
                        command = new MySqlCommand("INSERT INTO `customer` (customer_id, first_name, last_name) VALUES ((SELECT id FROM `user` " +
                            "WHERE login = @regLog AND password = @regPass),@fName, @lName)", data_base.getConnection());
                        command.Parameters.Add("@fName", MySqlDbType.VarChar).Value = firstName;
                        command.Parameters.Add("@lName", MySqlDbType.VarChar).Value = lastName;
                        command.Parameters.Add("@regLog", MySqlDbType.VarChar).Value = regLogin;
                        command.Parameters.Add("@regPass", MySqlDbType.VarChar).Value = regPassword1;
                        command.ExecuteNonQuery();
                    }
                    data_base.closeConnection();
                }
            }
            this.Close();
            MainWindow win = MainWindow.GetInstance();
            win.Show();
        }
    }
}
