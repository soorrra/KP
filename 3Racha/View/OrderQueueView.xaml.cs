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
using System.Windows.Controls.Primitives;

namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для OrderQueueView.xaml
    /// </summary>
    public partial class OrderQueueView : UserControl
    {
        private SQLiteConnection connection;
        private StackPanel orderStackPanel;

        public OrderQueueView()
        {
            InitializeComponent();
            ShowData();
            //   orderStackPanel = new StackPanel();
            //    grid1.Children.Add(orderStackPanel);

            //    // Выполняем запрос на выборку записей из таблицы OrderQueue
            //    string sql = "SELECT * FROM OrderQueue ORDER BY order_id, id";
            //    SQLiteCommand command = new SQLiteCommand(sql, connection);
            //    SQLiteDataReader reader = command.ExecuteReader();

            //    int currentOrderId = -1;
            //    TextBox currentTextBox = null;

            //    // Обрабатываем результаты запроса
            //    while (reader.Read())
            //    {
            //        int orderId = reader.GetInt32(0);
            //        string text = reader.GetString(1);

            //        if (orderId != currentOrderId)
            //        {
            //            // Создаем новый TextBox для нового order_id
            //            currentTextBox = new TextBox();
            //            orderStackPanel.Children.Add(currentTextBox);
            //            currentOrderId = orderId;
            //        }

            //        // Добавляем запись в текущий TextBox
            //        currentTextBox.Text += text + "n";
            //    }

            //    reader.Close();
            //}
            //private void addButton_Click(object sender, RoutedEventArgs e)
            //{
            //    // Выполняем запрос на добавление записи в таблицу OrderQueue
            //    string sql = "INSERT INTO OrderQueue (order_id, text) VALUES (@order_id, @text)";
            //    SQLiteCommand command = new SQLiteCommand(sql, connection);
            //    command.Parameters.AddWithValue("@order_id", 1); // TODO: заменить на актуальное значение
            //    command.Parameters.AddWithValue("@text", "Новая запись");
            //    command.ExecuteNonQuery();
            //}

            //private void deleteButton_Click(object sender, RoutedEventArgs e)
            //{
            //    // Выполняем запрос на удаление записи из таблицы OrderQueue
            //    string sql = "DELETE FROM OrderQueue WHERE order_id = @order_id";
            //    SQLiteCommand command = new SQLiteCommand(sql, connection);
            //    command.Parameters.AddWithValue("@order_id", 1); // TODO: заменить на актуальное значение
            //    command.ExecuteNonQuery();
            //}

            //private void CreateTable()
            //{
            //    try
            //    {
            //        // создаем таблицу OrderQueue с полями id, order_id, name, quantity
            //        sql_con.Open();
            //        string create_query = "CREATE TABLE OrderQueue (id INTEGER PRIMARY KEY, order_id INT, name VARCHAR(20), quantity INT)";
            //        sql_cmd = new SQLiteCommand(create_query, sql_con);
            //        sql_cmd.ExecuteNonQuery();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //    finally
            //    {
            //        if (sql_con.State == System.Data.ConnectionState.Open) sql_con.Close();
            //    }
            //}

            //private void FillTable()
            //{
            //    try
            //    {
            //        // добавляем данные в таблицу OrderQueue
            //        sql_con.Open();
            //        string insert_query = "INSERT INTO OrderQueue (order_id, name, quantity) VALUES (@order_id, @name, @quantity)";
            //        sql_cmd = new SQLiteCommand(insert_query, sql_con);
            //        sql_cmd.Parameters.AddWithValue("@order_id", 1);
            //        sql_cmd.Parameters.AddWithValue("@name", "Product 1");
            //        sql_cmd.Parameters.AddWithValue("@quantity", 10);
            //        sql_cmd.ExecuteNonQuery();

            //        sql_cmd = new SQLiteCommand(insert_query, sql_con);
            //        sql_cmd.Parameters.AddWithValue("@order_id", 1);
            //        sql_cmd.Parameters.AddWithValue("@name", "Product 2");
            //        sql_cmd.Parameters.AddWithValue("@quantity", 5);
            //        sql_cmd.ExecuteNonQuery();

            //        sql_cmd = new SQLiteCommand(insert_query, sql_con);
            //        sql_cmd.Parameters.AddWithValue("@order_id", 2);
            //        sql_cmd.Parameters.AddWithValue("@name", "Product 3");
            //        sql_cmd.Parameters.AddWithValue("@quantity", 8);
            //        sql_cmd.ExecuteNonQuery();

            //        sql_cmd = new SQLiteCommand(insert_query, sql_con);
            //        sql_cmd.Parameters.AddWithValue("@order_id", 2);
            //        sql_cmd.Parameters.AddWithValue("@name", "Product 4");
            //        sql_cmd.Parameters.AddWithValue("@quantity", 3);
            //        sql_cmd.ExecuteNonQuery();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //    finally
            //    {
            //        if (sql_con.State == System.Data.ConnectionState.Open) sql_con.Close();
            //    }
            //}


            async Task ShowData()
            {
                // выбираем данные из таблицы OrderQueue
                string select_query = "SELECT * FROM OrderComplete WHERE status='Не готов' ORDER BY order_id";
                // создаем словарь, где ключ - order_id, значение - список записей
                Dictionary<int, List<string>> orders = new Dictionary<int, List<string>>();
                await Task.Run(() =>
                {
                    string connectionString = "Data Source=DB.db";
                    using (SQLiteConnection sql_con = new SQLiteConnection(connectionString))
                    {

                        sql_con.Open();
                        using (SQLiteCommand sql_cmd = new SQLiteCommand(select_query, sql_con))
                        {
                            using (SQLiteDataReader sql_datareader = sql_cmd.ExecuteReader())
                            {
                                // читаем записи из ридера и заполняем словарь
                                while (sql_datareader.Read())
                                {
                                    int order_id = Convert.ToInt32(sql_datareader["order_id"]);
                                    int dishID = Convert.ToInt32(sql_datareader["dishID"]);
                                    int price = Convert.ToInt32(sql_datareader["price"]);

                                    // Получение наименования блюда
                                    string name = "";
                                    string sql = "SELECT name FROM Dishes WHERE dishID=@dishID";
                                    using (SQLiteCommand command = new SQLiteCommand(sql, sql_con))
                                    {
                                        command.Parameters.AddWithValue("@dishID", dishID);
                                        name = (string)command.ExecuteScalar();
                                    }

                                    string order_item = name + ": " + price;
                                    if (orders.ContainsKey(order_id))
                                    {
                                        orders[order_id].Add(order_item);
                                    }
                                    else
                                    {
                                        orders.Add(order_id, new List<string> { order_item });
                                    }

                                }

                            }

                        }
                        sql_con.Close();
                    }
                });
                // создаем TextBlock и TextBox для каждого order_id
                foreach (var order in orders)
                {
                    int order_id = order.Key; // добавляем переменную order_id
                    TextBlock order_tb = new TextBlock();
                    order_tb.Margin = new Thickness(20 , 0, 20, 0); // добавляем отступы
                    order_tb.Text = "Order #" + order_id;
                    stackPanel.Children.Add(order_tb);
                    foreach (string item in order.Value)
                    {
                        System.Windows.Controls.TextBox item_tb = new System.Windows.Controls.TextBox();
                        item_tb.Text = item;
                        item_tb.Margin = new Thickness(20, 0, 20, 0); // добавляем отступы
                        stackPanel.Children.Add(item_tb);
                    }
                    System.Windows.Controls.Button item_bt = new System.Windows.Controls.Button();
                    item_bt.Content = "Готово";
                    item_bt.Width = 100; // фиксируем ширину кнопки
                    item_bt.Height = 30; // фиксируем высоту кнопки
                    item_bt.Margin = new Thickness(0, 10, 0, 20); // добавляем отступы
                    item_bt.Style = (Style)this.Resources["myButtonStyle"]; 
                    string param2 = "Готов";
                    item_bt.Click += async (sender, e) =>
                    {
                        try
                        {
                            string connectionString = "Data Source=DB.db";
                            using (SQLiteConnection con = new SQLiteConnection(connectionString))
                            {
                               
                                string queryUpdateStatus = "UPDATE OrderComplete SET status = @status WHERE order_id = @order_id";
                                await con.OpenAsync();
                                using (SQLiteTransaction transaction = con.BeginTransaction())
                                {

                                    using (SQLiteCommand cmdUpdateStatus = new SQLiteCommand(queryUpdateStatus, con, transaction))
                                    {
                                        cmdUpdateStatus.Parameters.AddWithValue("@order_id", order_id);
                                        cmdUpdateStatus.Parameters.AddWithValue("@status", param2);
                                        await cmdUpdateStatus.ExecuteNonQueryAsync();
                                    }
                                    transaction.Commit();
                                }
                                con.Close();
                            }
                            //grid1.Visibility = Visibility.Hidden;
                        }
                        catch (SQLiteException ex)
                        {
                            if (ex.Message.Contains("database is locked"))
                            {
                                await Task.Delay(100);
                                item_bt.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                            }
                            else
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    };
                    stackPanel.Children.Add(item_bt);
                }
            }
        }
        }

}

