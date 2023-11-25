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
    /// Логика взаимодействия для CreateNewUser.xaml
    /// </summary>
    public partial class CreateNewUser : Window
    {
        private const string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";
        public CreateNewUser()
        {
            InitializeComponent();
        }

        private void CreateUs_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, заполнены ли все текстовые поля
            if (string.IsNullOrEmpty(UserFTextBox.Text) ||
                string.IsNullOrEmpty(UserITextBox.Text) ||
                string.IsNullOrEmpty(UserOTextBox.Text) ||
                string.IsNullOrEmpty(LoginTextBox.Text) ||
                string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Выполняем вставку данных в базу данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Получаем текущий максимальный ID_User из базы данных
                    string getMaxIdQuery = "SELECT MAX(ID_User) FROM [User]";
                    using (SqlCommand getMaxIdCommand = new SqlCommand(getMaxIdQuery, connection))
                    {
                        object maxIdResult = getMaxIdCommand.ExecuteScalar();
                        int newId;
                        if (int.TryParse(maxIdResult.ToString(), out int currentMaxId))
                        {
                            newId = currentMaxId + 1;
                        }
                        else
                        {
                            newId = 1;
                        }

                        string query = "INSERT INTO [User] (ID_User, ID_Role, ID_Group, UserF, UserI, UserO, User_login, User_pswd) " +
                                       "VALUES (@ID_User, @ID_Role, @ID_Group, @UserF, @UserI, @UserO, @User_login, @User_pswd)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID_User", newId);
                            command.Parameters.AddWithValue("@ID_Role", RoleTextBox.Text);
                            command.Parameters.AddWithValue("@ID_Group", GroupTextBox.Text);
                            command.Parameters.AddWithValue("@UserF", UserFTextBox.Text);
                            command.Parameters.AddWithValue("@UserI", UserITextBox.Text);
                            command.Parameters.AddWithValue("@UserO", UserOTextBox.Text);
                            command.Parameters.AddWithValue("@User_login", LoginTextBox.Text);
                            command.Parameters.AddWithValue("@User_pswd", PasswordTextBox.Text);

                            command.ExecuteNonQuery();

                            MessageBox.Show("Новый пользователь успешно создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                            // Очищаем текстовые поля
                            RoleTextBox.Clear();
                            GroupTextBox.Clear();
                            UserFTextBox.Clear();
                            UserITextBox.Clear();
                            UserOTextBox.Clear();
                            LoginTextBox.Clear();
                            PasswordTextBox.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса к базе данных:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
