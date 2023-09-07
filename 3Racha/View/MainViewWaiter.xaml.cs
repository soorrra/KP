using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts.Wpf.Charts.Base;
using LiveCharts.Wpf;
using LiveCharts.Helpers;
using System.Xml.Linq;
using LiveCharts;
using System.Security.Principal;

namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для MainViewWaiter.xaml
    /// </summary>
    public partial class MainViewWaiter : UserControl
    {
       
            public List<int> OrderCounts { get; set; }

        public MainViewWaiter()
        {
            InitializeComponent();

            // Подключение к базе данных
            string connectionString = "Data Source=DB.db";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Запрос на выборку трех наиболее часто упоминающихся значений dishID из таблицы OrderComplete
                string sql = @"SELECT dishID FROM OrderComplete
                   GROUP BY dishID
                   ORDER BY COUNT(*) DESC
                   LIMIT 3";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        int i = 1;
                        while (reader.Read())
                        {
                            int dishID = reader.GetInt32(0);
                            string name = "";
                            string sql_cmd = "SELECT name FROM Dishes WHERE dishID=@dishID";
                            using (SQLiteCommand cmd = new SQLiteCommand(sql_cmd, connection))
                            {
                                cmd.Parameters.AddWithValue("@dishID", dishID);
                                name = (string)cmd.ExecuteScalar();
                            }

                            TextBlock textBlock = (TextBlock)this.FindName("dishIDTextBlock" + i);
                            if (textBlock != null)
                            {
                                textBlock.Text = $"{name}";
                                i++;
                            }
                        }
                    }
                }
            }








            GreetUser();
            int Id = GetUserId(); // вызов метода для получения IdS пользователя
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = db.Users.FirstOrDefault(u => u.userID == Id); // поиск пользователя в БД
                if (user != null)
                {
                    //name.Text = user.name; // установка имени пользователя
                }
            }

            List<OrderComplete> orderRecords = new List<OrderComplete>();
            string query = "SELECT order_id, dataTime FROM OrderComplete";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderComplete record = new OrderComplete();
                            record.order_id = reader.GetInt32(0);
                            record.dataTime = reader.GetDateTime(1);
                            orderRecords.Add(record);
                        }
                    }
                }
            }

            // Формируем список заказов с указанием времени заказа
            Dictionary<int, List<DateTime>> orderDictionary = new Dictionary<int, List<DateTime>>();
            foreach (OrderComplete record in orderRecords)
            {
                if (!orderDictionary.ContainsKey(record.order_id))
                {
                    orderDictionary.Add(record.order_id, new List<DateTime>());
                }
                orderDictionary[record.order_id].Add(record.dataTime);
            }

            List<int> orderCounts = new List<int>();
            for (int i = 0; i <= 23; i++)
            {
                DateTime fromDate = new DateTime(2023, 6, 23, i, 0, 0);
                DateTime toDate = new DateTime(2023, 6, 23, i, 59, 59);
                int count = orderDictionary.Values.Count(x => x.Any(d => d >= fromDate && d <= toDate));
                orderCounts.Add(count);
            }

            SeriesCollection = new SeriesCollection
    {
        new LineSeries
        {
            Title = "Количество заказов",
            Values = new ChartValues<int>(orderCounts)
        }
    };

            HourLabels = new List<string>();
            for (int i = 0; i <= 23; i++)
            {
                HourLabels.Add(i.ToString("00"));
            }

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public List<string> HourLabels { get; set; }

        private int GetUserId()
        {
            int id = Get.Value;
            return id;
        }
        private void GreetUser()
        {
            string userName = "";
            int Id = GetUserId(); // вызов метода для получения IdS пользователя

            using (ApplicationContext db = new ApplicationContext())
            {
                var user = db.Users.FirstOrDefault(u => u.userID == Id); // поиск пользователя в БД
                if (user != null)
                {
                    userName = user.name; // установка имени пользователя
                }
            }

            DateTime currentTime = DateTime.Now;
            string greeting = GetGreeting(currentTime);
            string message = $"Доброго {greeting}, {userName}!";
            aniosue.Text = message;

            //// Подключение к базе данных
            //string connectionString = "Data Source=DB.db";
            //using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            //{
            //    connection.Open();

            //    // Запрос на выборку количества упоминаний userName из таблицы OrderComplete
            //    string sql = @"SELECT COUNT(*) FROM OrderComplete
            //       WHERE worker=@userName";
            //    using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            //    {
            //        command.Parameters.AddWithValue("@userName", "Имя работника");
            //        object result = command.ExecuteScalar();
            //        if (result != null && result is long)
            //        {
            //            long count = (long)result;
            //            countt.Text = $"Количество упоминаний работника: {count}";
            //        }
            //    }
            //}


        }

        private string GetGreeting(DateTime currentTime)
        {
            int hour = currentTime.Hour;
            if (hour >= 6 && hour < 12)
            {
                return "утра";
            }
            else if (hour >= 12 && hour < 18)
            {
                return "деня";
            }
            else
            {
                return "вечера";
            }
        }
        //private void InitializeChart()
        //{
        //    OrderCounts = new List<int>();
        //    for (int i = 0; i <= 23; i++)
        //    {
        //        int count = 0;
        //        string connectionString = "Data Source=DB.db";
        //        string query = $"SELECT COUNT(*) FROM OrderComplete WHERE strftime('%Y-%m-%d %H:%M:%f', dataTime) = '2023-06-23 {i.ToString("00")}:00:000000'";
        //        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        //        {
        //            connection.Open();
        //            using (SQLiteCommand command = new SQLiteCommand(query, connection))
        //            {
        //                count = Convert.ToInt32(command.ExecuteScalar());
        //            }
        //        }
        //        OrderCounts.Add(count);
        //    }
        //    DataContext = this;


    }
    
} 
