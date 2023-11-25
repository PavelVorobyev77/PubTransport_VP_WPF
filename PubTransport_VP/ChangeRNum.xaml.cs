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
    /// Логика взаимодействия для ChangeRNum.xaml
    /// </summary>
    public partial class ChangeRNum : Window
    {
        private const string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";
        public ChangeRNum()
        {
            InitializeComponent();
        }

        private void ReadRNum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Получение значения ID_Route_number для номера маршрута
                    int rId;
                    if (!int.TryParse(IDRNumTextBox.Text, out rId))
                    {
                        MessageBox.Show("Некорректное значение ID номера маршрута.");
                        return;
                    }

                    // Подготовка запроса на получение данных номера маршрута по указанному ID_Route_number
                    string query = "SELECT ID_Route_number, Transport_Type.ID_Transport_Type, Route_number.Route_number " +
                        "FROM  [Route_number] " +
                        "INNER JOIN Transport ON Route_number.ID_Transport_Type = Transport.ID_Transport " +
                        "INNER JOIN Transport_Type ON Transport.ID_Transport_Type = Transport_Type.ID_Transport_Type " +
                        "WHERE ID_Route_number = @ID_Route_number";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Route_number", rId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Заполнение текстовых полей значениями номера маршрута
                                RNumTextBox.Text = reader["Route_number"].ToString();
                                tTrTextBox.Text = reader["ID_Transport_Type"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Номер маршрута с указанным ID не найден.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении номера маршрута: " + ex.Message);
            }
        }


        private void ChangeRNum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Получение значения ID_Route_number для номера маршрута
                    int rId;
                    if (!int.TryParse(IDRNumTextBox.Text, out rId))
                    {
                        MessageBox.Show("Некорректное значение ID номера маршрута.");
                        return;
                    }

                    // Запрос на обновление данных номера маршрута
                    string query = "BEGIN TRANSACTION " +
                       "DECLARE @id1 int = (SELECT ID_Transport_Type FROM Route_number WHERE ID_Route_number = @ID_Route_number) " +
                       "UPDATE [Route_number] " +
                       "SET Route_number = @Route_number, " +
                       "ID_Transport_Type = @ID_Transport_Type " +
                       "WHERE ID_Route_number = @ID_Route_number " +
                       "COMMIT TRANSACTION ";
                    ;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Установка значений параметров
                        command.Parameters.AddWithValue("@ID_Route_number", rId);
                        command.Parameters.AddWithValue("@Route_number", RNumTextBox.Text);
                        command.Parameters.AddWithValue("@ID_Transport_Type", tTrTextBox.Text);


                        // Выполнение запроса
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные номера маршрута успешно обновлены!");
                        }
                        else
                        {
                            MessageBox.Show("Номер маршрута с указанным ID не найдена.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении номера маршрута: " + ex.Message);
            }
        }
    }
}

