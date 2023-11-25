using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace PubTransport_VP
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private const string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";

        public Login()
        {
            InitializeComponent();
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, заполнены ли все текстовые поля
            if (string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Выполняем проверку авторизации
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT UserF, UserI, UserO, ID_Role FROM [User] WHERE User_login = @User_login AND User_pswd = @User_pswd";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@User_login", LoginTextBox.Text);
                        command.Parameters.AddWithValue("@User_pswd", PasswordTextBox.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string userF = reader.GetString(0);
                                string userI = reader.GetString(1);
                                string userO = reader.GetString(2);
                                int roleId = reader.GetInt32(3);

                                MessageBox.Show($"С возвращением, {userF} {userI} {userO}!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                                // Перенаправляем пользователя на соответствующую форму в зависимости от ID_Role
                                if (roleId == 1)
                                {
                                    OperatorMain operatorMain = new OperatorMain();
                                    operatorMain.Show();
                                }
                                else if (roleId == 2)
                                {
                                    AdminMain adminMain = new AdminMain();
                                    adminMain.Show();
                                }
                                else if (roleId == 3)
                                {
                                    SuperAdminMain superAdmin = new SuperAdminMain();
                                    superAdmin.Show();
                                }
                                else
                                {
                                    VisitMain visitMain = new VisitMain();
                                    visitMain.Show();
                                }

                                // Закрываем текущую форму авторизации
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Такого пользователя не существует. Пожалуйста, проверьте введенные данные или зарегистрируйтесь.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса к базе данных:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // вернуться назад
            MainWindow MainForm = new MainWindow();
            MainForm.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Открыть форму регистрации
            Register registerForm = new Register();
            registerForm.Show();
            this.Close();
        }
        public string UserLogin
        {
            get { return LoginTextBox.Text; }
        }
    }
}
