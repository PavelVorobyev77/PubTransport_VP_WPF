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
    /// Логика взаимодействия для CreateNewStop.xaml
    /// </summary>
    public partial class CreateNewStop : Window
    {
        private const string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";
        public CreateNewStop()
        {
            InitializeComponent();
        }

        private void CreateSt_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, заполнены ли все текстовые поля
            if (string.IsNullOrEmpty(AreaTextBox.Text) ||
                string.IsNullOrEmpty(StTextBox.Text) ||
                string.IsNullOrEmpty(PavTextBox.Text))
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
                    // Получаем текущий максимальный ID_Stop из базы данных
                    string getMaxIdQuery = "SELECT MAX(ID_Stop) FROM [Stop]";
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

                        string query = "INSERT INTO [Stop] (ID_Stop, ID_Area, StopName, Pavilion) " +
                                       "VALUES (@ID_Stop, @ID_Area, @StopName, @Pavilion)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID_Stop", newId);
                            command.Parameters.AddWithValue("@ID_Area", AreaTextBox.Text);
                            command.Parameters.AddWithValue("@StopName", StTextBox.Text);
                            command.Parameters.AddWithValue("@Pavilion", PavTextBox.Text);

                            command.ExecuteNonQuery();

                            MessageBox.Show("Новая остановка успешно создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                            // Очищаем текстовые поля
                            AreaTextBox.Clear();
                            StTextBox.Clear();
                            PavTextBox.Clear();
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
