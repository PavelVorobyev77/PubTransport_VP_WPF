using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    public partial class SearchUsers : Window
    {
        private const string connectionString = "Data Source=DESKTOP-BK1T0PD\\SQLEXPRESS;Initial Catalog=21.102-08-VP-PubTransport;Integrated Security=True";

        private DataTable usersDataTable = new DataTable();

        public SearchUsers()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            // Загрузка всех пользователей из базы данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM [User]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(usersDataTable);

                        UsersDataGrid.ItemsSource = usersDataTable.DefaultView;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса к базе данных:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Поиск пользователей по ФИО
            string searchQuery = SearchTextBox.Text.Trim();

            DataView dataView = usersDataTable.DefaultView;

            if (string.IsNullOrEmpty(searchQuery))
            {
                // Очищаем фильтр, чтобы показать всех пользователей
                dataView.RowFilter = string.Empty;
            }
            else
            {
                // Фильтруем пользователей по введенному запросу
                dataView.RowFilter = string.Format("UserF LIKE '%{0}%' OR UserI LIKE '%{0}%' OR UserO LIKE '%{0}%'", searchQuery);
            }

            UsersDataGrid.ItemsSource = dataView;

            // Если поисковый запрос пустой, очищаем текстовое поле поиска
            if (string.IsNullOrEmpty(searchQuery))
            {
                SearchTextBox.Clear(); // Или можно использовать SearchTextBox.Clear();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Разрешить редактирование данных в DataGrid
            UsersDataGrid.IsReadOnly = false;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            // Сохранить измененные данные в базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.SelectCommand = new SqlCommand("SELECT * FROM [User]", connection);
                    adapter.InsertCommand = commandBuilder.GetInsertCommand();
                    adapter.DeleteCommand = commandBuilder.GetDeleteCommand();
                    adapter.UpdateCommand = commandBuilder.GetUpdateCommand();

                    adapter.Update(usersDataTable);

                    MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса к базе данных:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    UsersDataGrid.IsReadOnly = true;
                }
            }
        }
    }
}
