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
    /// Логика взаимодействия для DeleteRoute.xaml
    /// </summary>
    public partial class DeleteRoute : Window
    {
        private const string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";
        public DeleteRoute()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ChoiceRoute choiceRoute = new ChoiceRoute();
            choiceRoute.Show();
            Close();
        }

        private void DeleteRNum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Получение значения ID_Route_number
                    int rnId;
                    if (!int.TryParse(IDRNumTextBox.Text, out rnId))
                    {
                        MessageBox.Show("Некорректное значение ID номера маршрута.");
                        return;
                    }

                    // Подготовка SQL-запроса для удаления по указанному ID
                    string query = "DELETE FROM [Route_number] WHERE ID_Route_number = @ID_Route_number";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Route_number", rnId);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Номер маршрута успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            // Очистка текстового поля с ID после удаления
                            IDRNumTextBox.Text = string.Empty;
                        }
                        else
                        {
                            MessageBox.Show("Номер маршрута с указанным ID не найден.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении номера маршрута: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

