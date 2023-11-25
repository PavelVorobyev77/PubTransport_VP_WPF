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
    /// Логика взаимодействия для CreateNewRoute.xaml
    /// </summary>
    public partial class CreateNewRoute : Window
    {
        private const string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";
        public CreateNewRoute()
        {
            InitializeComponent();
        }

        private void CreateRNum_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, заполнены ли все текстовые поля
            if (string.IsNullOrEmpty(RNumTextBox.Text) ||
                string.IsNullOrEmpty(GrTextBox.Text) ||
                string.IsNullOrEmpty(tTrTextBox.Text))
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
                    // Получаем текущий максимальный ID_Route_number из базы данных
                    string getMaxIdQuery = "SELECT MAX(ID_Route_number) FROM [Route_number]";
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

                        string query = "INSERT INTO [Route_number] (ID_Route_number, Route_number, ID_Group, ID_Transport_Type) " +
                                       "VALUES (@ID_Route_number, @Route_number, @ID_Group, @ID_Transport_Type)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID_Route_number", newId);
                            command.Parameters.AddWithValue("@Route_number", RNumTextBox.Text);
                            command.Parameters.AddWithValue("@ID_Group", GrTextBox.Text);
                            command.Parameters.AddWithValue("@ID_Transport_Type", tTrTextBox.Text);

                            command.ExecuteNonQuery();

                            MessageBox.Show("Новый номер маршрута успешно создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                            // Очищаем текстовые поля
                            RNumTextBox.Clear();
                            GrTextBox.Clear();
                            tTrTextBox.Clear();
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
            ChoiceRoute choiceRoute = new ChoiceRoute();
            choiceRoute.Show();
            Close();
        }
    }
}
