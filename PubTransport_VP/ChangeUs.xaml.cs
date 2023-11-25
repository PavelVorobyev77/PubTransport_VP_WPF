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
    /// Логика взаимодействия для ChangeUs.xaml
    /// </summary>
    public partial class ChangeUs : Window
    {
        private const string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";
        public ChangeUs()
        {
            InitializeComponent();
        }

        private void ReadUs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Получение значения ID_User для пользователя
                    int userId;
                    if (!int.TryParse(IDUserTextBox.Text, out userId))
                    {
                        MessageBox.Show("Некорректное значение ID пользователя.");
                        return;
                    }

                    // Подготовка запроса на получение данных пользователя по указанному ID_User
                    string query = "SELECT ID_User, ID_Role, ID_Group, UserF, UserI, UserO, User_login, User_pswd " +
                        "FROM [User] WHERE ID_User = @ID_User";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_User", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Заполнение текстовых полей значениями пользователя
                                RoleTextBox.Text = reader["ID_Role"].ToString();
                                GroupTextBox.Text = reader["ID_Group"].ToString();
                                UserFTextBox.Text = reader["UserF"].ToString();
                                UserITextBox.Text = reader["UserI"].ToString();
                                UserOTextBox.Text = reader["UserO"].ToString();
                                LoginTextBox.Text = reader["User_login"].ToString();
                                PasswordTextBox.Text = reader["User_pswd"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Пользователь с указанным ID не найден.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении пользователя: " + ex.Message);
            }
        }


        private void ChangeUs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Получение значения ID_User для пользователя
                    int userId;
                    if (!int.TryParse(IDUserTextBox.Text, out userId))
                    {
                        MessageBox.Show("Некорректное значение ID пользователя.");
                        return;
                    }

                    // Запрос на обновление данных пользователя
                    string query = "UPDATE [User] SET ID_Role = @ID_Role, ID_Group = @ID_Group, UserF = @UserF, " +
                        "UserI = @UserI, UserO = @UserO, User_login = @User_login, User_pswd = @User_pswd " +
                        "WHERE ID_User = @ID_User";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Установка значений параметров
                        command.Parameters.AddWithValue("@ID_User", userId);
                        command.Parameters.AddWithValue("@ID_Role", RoleTextBox.Text);
                        command.Parameters.AddWithValue("@ID_Group", GroupTextBox.Text);
                        command.Parameters.AddWithValue("@UserF", UserFTextBox.Text);
                        command.Parameters.AddWithValue("@UserI", UserITextBox.Text);
                        command.Parameters.AddWithValue("@UserO", UserOTextBox.Text);
                        command.Parameters.AddWithValue("@User_login", LoginTextBox.Text);
                        command.Parameters.AddWithValue("@User_pswd", PasswordTextBox.Text);

                        // Выполнение запроса
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные пользователя успешно обновлены!");
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с указанным ID не найден.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении пользователя: " + ex.Message);
            }
        }
    }
}
