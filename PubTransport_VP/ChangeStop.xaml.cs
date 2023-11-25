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
    /// Логика взаимодействия для ChangeStop.xaml
    /// </summary>
    public partial class ChangeStop : Window
    {
        private const string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";
        public ChangeStop()
        {
            InitializeComponent();
        }

        private void ReadSt_Click(object sender, RoutedEventArgs e)
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

                    // Подготовка запроса на получение данных остановки по указанному ID_Stop
                    string query = "SELECT ID_Stop, ID_Area, StopName, Pavilion " +
                        "FROM [Stop] WHERE ID_STOP = @ID_Stop";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Stop", stId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Заполнение текстовых полей значениями остановки
                                StTextBox.Text = reader["StopName"].ToString();
                                AreaTextBox.Text = reader["ID_Area"].ToString();
                                PavTextBox.Text = reader["Pavilion"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Остановка с указанным ID не найдена.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении остановки: " + ex.Message);
            }
        }


        private void ChangeSt_Click(object sender, RoutedEventArgs e)
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

                    // Запрос на обновление данных остановки
                    string query = "UPDATE [Stop] SET ID_Area = @ID_Area, StopName = @StopName, " +
                        "Pavilion = @Pavilion " +
                        "WHERE ID_Stop = @ID_Stop";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Установка значений параметров
                        command.Parameters.AddWithValue("@ID_Stop", stId);
                        command.Parameters.AddWithValue("@ID_Area", AreaTextBox.Text);
                        command.Parameters.AddWithValue("@StopName", StTextBox.Text);
                        command.Parameters.AddWithValue("@Pavilion", PavTextBox.Text);
                        

                        // Выполнение запроса
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные остановки успешно обновлены!");
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
                MessageBox.Show("Ошибка при обновлении остановки: " + ex.Message);
            }
        }
    }
}

