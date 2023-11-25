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
    /// Логика взаимодействия для ChoiceRoute.xaml
    /// </summary>
    public partial class ChoiceRoute : Window
    {
        public ChoiceRoute()
        {
            InitializeComponent();
        }

        private void ViewStBtn_Click(object sender, RoutedEventArgs e)
        {
            // Создание новой формы ViewStop
            ViewStop viewStop = new ViewStop();

            // Строка подключения к базе данных
            string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";

            // SQL-запрос для получения данных
            string query = "SELECT Area.AreaName, StopName, Pavilion " +
                           "FROM Stop S " +
                           "INNER JOIN Area ON S.ID_Area = Area.ID_Area ";

            // Создание подключения к базе данных и выполнение запроса
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    adapter.Fill(dataTable);

                    // Передача данных в форму ViewStop
                    viewStop.SetData(dataTable.DefaultView);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса к базе данных:\n" + ex.Message,
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            // Открытие формы ViewStop
            viewStop.Show();
        }

        private void ChangeStBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeStop changeSt = new ChangeStop();
            changeSt.Show();

        }
         
        private void ViewRNumBtn_Click(object sender, RoutedEventArgs e)
        {
            // Создание новой формы ViewRNum
            ViewRNum viewRNum = new ViewRNum();

            // Строка подключения к базе данных
            string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";

            // SQL-запрос для получения данных
            string query = "SELECT Transport_Type.TransportName, Route_number.Route_number " +
                           "FROM  Route_number  " +
                           "INNER JOIN Transport ON Route_number.ID_Transport_Type = Transport.ID_Transport  " +
                           "INNER JOIN Transport_Type ON Transport.ID_Transport_Type = Transport_Type.ID_Transport_Type ";

            // Создание подключения к базе данных и выполнение запроса
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    adapter.Fill(dataTable);

                    // Передача данных в форму ViewStop
                    viewRNum.SetData(dataTable.DefaultView);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса к базе данных:\n" + ex.Message,
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            // Открытие формы ViewStop
            viewRNum.Show();
        }

        private void ChangeRNumBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeRNum changeRNum = new ChangeRNum();
            changeRNum.Show();

        }

        private void CreateStBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewStop createNewStop = new CreateNewStop();
            createNewStop.Show();

        }

        private void DeleteStBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteStop deleteStop = new DeleteStop();
            deleteStop.Show();

        }

        private void CreateRNumBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewRoute createNewRoute = new CreateNewRoute();
            createNewRoute.Show();

        }

        private void DeleteRNumBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteRoute deleteRoute = new DeleteRoute();
            deleteRoute.Show();

        }
    }
}
