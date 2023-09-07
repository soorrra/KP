using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using System.Data.Entity;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Reflection;

namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для StaffView.xaml
    /// </summary>
    public partial class StaffView : System.Windows.Controls.UserControl

    {
        //int ccId;
        //private SQLiteConnection sqlite_conn;
        //private SQLiteCommand sqlite_cmd;
        //private SQLiteDataReader sqlite_datareader;
        public StaffView()
        {
            InitializeComponent();

            // ____________Заполнение__________

            // Установить соединение с базой данных SQLite
            string connectionString = "Data Source=DB.db";
            using (var connection = new SQLiteConnection(connectionString))
            {
                // Открыть соединение с базой данных
                connection.Open();

                // Выбрать данные из таблицы с нужными полями
                string query = "SELECT userID, name, surname, email, phone_number, login, password, post FROM Users";
                using (var command = new SQLiteCommand(query, connection))
                {
                    // Создать адаптер данных и заполнить набор данных
                    var dataAdapter = new SQLiteDataAdapter(command);
                    var dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);

                    // Показать данные в DataGrid
                    dataGrid1.ItemsSource = dataSet.Tables[0].DefaultView;
                }
            }                
        } 
              //______________Удаление________________
        private void Button_Click(object sender, RoutedEventArgs e)

        {
            DataRowView dataRowView = (DataRowView)dataGrid1.SelectedItem; // Получаем выделенный элемент
            int userID = Convert.ToInt32(dataRowView["userID"]); // Получаем значение поля Id выделенного элемента
            string connectionString = "Data Source=DB.db";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = $"DELETE FROM Users WHERE userID = {userID}";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                int result = cmd.ExecuteNonQuery();
                connection.Close();
            }
            dataRowView.Row.Delete(); // Удаляем выделенный элемент из DataTable, связанного с DataGrid
        }

        private void redact_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.IsReadOnly = false;
            redOk.Visibility = Visibility.Visible;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            //______________добавление_________________
            AddStaffView addstaffview = new AddStaffView();
            addstaffview.Show();
            
        }
        private void redOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Соединение с бд SQLite
                string connectionString = "Data Source=DB.db";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Обход всех строк DataGrid
                    // Обход всех строк DataGrid
                    foreach (var item in dataGrid1.Items)
                    {
                        if (item is DataRowView row)
                        {
                            // Получение данных из строк DataGrid
                            int userID = Convert.ToInt32(row["userID"]);
                            string name = Convert.ToString(row["name"]);
                            string surname = Convert.ToString(row["surname"]);
                            string email = Convert.ToString(row["email"]);
                            string phone_number = Convert.ToString(row["phone_number"]);
                            string login = Convert.ToString(row["login"]);
                            string password = Convert.ToString(row["password"]);
                            string post = Convert.ToString(row["post"]);


                            // Обновление записи в бд SQLite
                            string updateQuery = "UPDATE Users SET name=@name, surname=@surname, email=@email, phone_number=@phone_number, login=@login, password=@password, post=@post WHERE userID=@userID;";
                            using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                            {
                                command.Parameters.AddWithValue("@name", name);
                                command.Parameters.AddWithValue("@surname", surname);
                                command.Parameters.AddWithValue("@email", email);
                                command.Parameters.AddWithValue("@phone_number", phone_number);
                                command.Parameters.AddWithValue("@login", login);
                                command.Parameters.AddWithValue("@password", password);
                                command.Parameters.AddWithValue("@post", post);
                                command.Parameters.AddWithValue("@userID", userID);


                                int result = command.ExecuteNonQuery();
                                if (result == 0)
                                {
                                    // Запись не обновлена
                                }
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            redOk.Visibility = Visibility.Hidden;
            dataGrid1.IsReadOnly = true;


        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            // подключение к базе данных SQLite
            SQLiteConnection connection = new SQLiteConnection("Data Source=DB.db");
            connection.Open();

            // команда для выполнения запроса
            string query = "SELECT name, surname, email, phone_number, login, password, post, userID FROM Users WHERE name LIKE @name";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@name", "%" + TextBox1.Text + "%");

            // создание адаптера для заполнения таблицы данными
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Users");

            // вывод данных в DataGrid
            dataGrid1.ItemsSource = dataSet.Tables["Users"].DefaultView;

            // закрытие соединения с базой данных
            connection.Close();
        }
    }
}
