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
    /// Логика взаимодействия для SuperAdminMain.xaml
    /// </summary>
    public partial class SuperAdminMain : Window
    {
        public SuperAdminMain()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else if (result == MessageBoxResult.No)
            {
                // Ничего не делаем, просто закрываем сообщение и остаемся в той же форме
            }
        }

        private void ViewRBtn_Click(object sender, RoutedEventArgs e)
        {
            // Создание новой формы ViewRoute
            ViewRoute viewRoute = new ViewRoute();

            // Строка подключения к базе данных
            string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";

            // SQL-запрос для получения данных
            string query = "SELECT RN.Route_number, R.Starting_Stop, Stop.StopName, R.Final_Stop, R.Driving_time " +
                           "FROM Route R " +
                           "INNER JOIN Transport T ON R.ID_Transport = T.ID_Transport " +
                           "INNER JOIN Route_number RN ON R.ID_Route_number = RN.ID_Route_number " +
                           "INNER JOIN Stop ON R.ID_Stop = Stop.ID_Stop " +
                           "ORDER BY R.ID_Route_number ASC";

            // Создание подключения к базе данных и выполнение запроса
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    adapter.Fill(dataTable);

                    // Передача данных в форму ViewRoute
                    viewRoute.SetData(dataTable.DefaultView);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса к базе данных:\n" + ex.Message,
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            // Открытие формы ViewRoute
            viewRoute.Show();
        }

        private void ViewUsBtn_Click(object sender, RoutedEventArgs e)
        {
            // Создание новой формы ViewRoute
            ViewUser viewUser = new ViewUser();

            // Строка подключения к базе данных
            string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";

            // SQL-запрос для получения данных
            string query = "SELECT US.ID_User, US.UserF, US.UserI, US.UserO, US.User_login, US.User_pswd, Role.RoleName, [Group].GroupName " +
                           "FROM [User] US " +
                           "INNER JOIN Role ON US.ID_Role = Role.ID_Role " +
                           "INNER JOIN [Group] ON US.ID_Group = [Group].ID_Group ";

            // Создание подключения к базе данных и выполнение запроса
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    adapter.Fill(dataTable);

                    // Передача данных в форму ViewUser
                    viewUser.SetData(dataTable.DefaultView);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса к базе данных:\n" + ex.Message,
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            // Открытие формы ViewUser
            viewUser.Show();
        }

        private void ViewTBtn_Click(object sender, RoutedEventArgs e)
        {
            // Создание новой формы ViewRoute
            ViewTicket viewTicket = new ViewTicket();

            // Строка подключения к базе данных
            string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";

            // SQL-запрос для получения данных
            string query = "SELECT Card.CardName, TC.Price " +
                           "FROM Ticket_Price_by_Card TC " +
                           "INNER JOIN Card ON TC.ID_Card = Card.ID_Card ";

            // Создание подключения к базе данных и выполнение запроса
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    adapter.Fill(dataTable);

                    // Передача данных в форму ViewTicket
                    viewTicket.SetData(dataTable.DefaultView);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса к базе данных:\n" + ex.Message,
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            // Открытие формы ViewTicket
            viewTicket.Show();
        }

        private void ChangeUSBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeUs changeUs = new ChangeUs();
            changeUs.Show();

        }

        private void CreateUsBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewUser createNewUser = new CreateNewUser();
            createNewUser.Show();

        }

        private void DeleteUsBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteUser deleteUser = new DeleteUser();
            deleteUser.Show();

        }

        private void ChangeRBtn_Click(object sender, RoutedEventArgs e)
        {
            ChoiceRoute choiceRoute = new ChoiceRoute();
            choiceRoute.Show();

        }

        private void SearchUsBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchUsers searchUsers = new SearchUsers();
            searchUsers.Show();
        }
    }
}
