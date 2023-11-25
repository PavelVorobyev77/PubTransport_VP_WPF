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
    /// Логика взаимодействия для DeleteStop.xaml
    /// </summary>
    public partial class DeleteStop : Window
    {
        private const string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";
        public DeleteStop()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ChoiceRoute choiceRoute = new ChoiceRoute();
            choiceRoute.Show();
            Close();
        }
         
        private void DeleteSt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Получение значения ID_Stop для остановки
                    int stId;
                    if (!int.TryParse(IDStopTextBox.Text, out stId))
                    {
                        MessageBox.Show("Некорректное значение ID остановки.");
                        return;
                    }

                    // Подготовка SQL-запроса для удаления остановки по указанному ID
                    string query = "DELETE FROM [Stop] WHERE ID_Stop = @ID_Stop";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Stop", stId);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Остановка успешно удалена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            // Очистка текстового поля с ID после удаления
                            IDStopTextBox.Text = string.Empty;
                        }
                        else
                        {
                            MessageBox.Show("Остановка с указанным ID не найдена.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении остановки: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

