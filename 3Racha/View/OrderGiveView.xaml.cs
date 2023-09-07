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
    /// Логика взаимодействия для OrderGiveView.xaml
    /// </summary>
    public partial class OrderGiveView : UserControl
    {
        public OrderGiveView()
        {
            InitializeComponent();
            ShowData();
        }
        async Task ShowData()
        {
            // выбираем данные из таблицы OrderQueue
            string select_query = "SELECT * FROM OrderComplete WHERE status='Готов' ORDER BY order_id";
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
                order_tb.Margin = new Thickness(20, 0, 20, 0); // добавляем отступы
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
                item_bt.Content = "Выдать";
                item_bt.Width = 100; // фиксируем ширину кнопки
                item_bt.Height = 30; // фиксируем высоту кнопки
                item_bt.Margin = new Thickness(0, 10, 0, 20); // добавляем отступы
                item_bt.Style = (Style)this.Resources["myButtonStyle"];
                string param2 = "Выдан";
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
