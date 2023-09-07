using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Forms;
using System.Data.Entity;
using _3Racha;
using System.Media;
using System.Windows.Media.Animation;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Net.Mail;
using System.Net;
using Microsoft.Office.Interop.Excel;

namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для CreateOrderView.xaml
    /// </summary>
    public partial class CreateOrderView : System.Windows.Controls.UserControl
    {
        int maxOrderId = 0;
        string selectedCategory;
        string place;
        int num;
        string message = "";
        Excel.Application exApp = new Excel.Application();
        Word.Application wordApp = new Word.Application();

        public delegate void AnimationDelegate(object sender, EventArgs e);
        public CreateOrderView()
        {
            InitializeComponent();
            using (ApplicationContext db = new ApplicationContext())//енам
            {
             


            }

        }
        public void Fill_DataBase(string selectedCategory)
        {
            //// ____________Заполнение__________

            // Установить соединение с базой данных SQLite
            string connectionString = "Data Source=DB.db";
            using (var connection = new SQLiteConnection(connectionString))
            {
                // Открыть соединение с базой данных
                connection.Open();

                // Выбрать данные из таблицы с нужными полями
                string query = $"SELECT dishID, name, price, weight FROM Dishes WHERE category='{selectedCategory}'";
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
        

        public void btn1_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = "Салаты / Salads";
            Fill_DataBase(selectedCategory);
            gridTop.Visibility = Visibility.Visible;
            borderMain.Visibility = Visibility.Visible;
            borderFirst.Visibility = Visibility.Hidden;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = "Супы / Soups";
            Fill_DataBase(selectedCategory);
            gridTop.Visibility = Visibility.Visible;
            borderMain.Visibility = Visibility.Visible;
            borderFirst.Visibility = Visibility.Hidden;
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = "Рыбные блюда / Fish dishes";
            Fill_DataBase(selectedCategory);
            gridTop.Visibility = Visibility.Visible;
            borderMain.Visibility = Visibility.Visible;
            borderFirst.Visibility = Visibility.Hidden;
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = "Мясные блюда / Meat dishes";
            Fill_DataBase(selectedCategory);
            gridTop.Visibility = Visibility.Visible;
            borderMain.Visibility = Visibility.Visible;
            borderFirst.Visibility = Visibility.Hidden;
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = "Десерты / Desserts";
            Fill_DataBase(selectedCategory);
            gridTop.Visibility = Visibility.Visible;
            borderMain.Visibility = Visibility.Visible;
            borderFirst.Visibility = Visibility.Hidden;
        }
       
        private void ButtonAdd_Click(object sender, RoutedEventArgs e) //Кнопка добавления из DataGrid 
        {

            string connectionString = "Data Source=DB.db";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Получение максимального значения order_id из таблицы OrderItems
                int maxOrderId;
                using (SQLiteCommand command = new SQLiteCommand("SELECT MAX(order_id) FROM OrderComplete", connection))
                {
                    maxOrderId = Convert.ToInt32(command.ExecuteScalar());
                }
                maxOrderId++; // увеличение переменной на 1
                              // Получение выделенных строк в datagrid1
                foreach (DataRowView row in dataGrid1.SelectedItems)
                {
                    // Получение значения dishID из выбранной строки в datagrid1
                    int dishID = Convert.ToInt32(row["dishID"]);
                    int price = Convert.ToInt32(row["price"]);

                    // Добавление данных в таблицу OrderItems
                    string sqlInsert = "INSERT INTO OrderItems (dishID, price, order_id) VALUES (@dishID, @price, @maxOrderId)";
                    using (SQLiteCommand command = new SQLiteCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@dishID", dishID);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@maxOrderId", maxOrderId);
                        command.ExecuteNonQuery();
                    }
                }
            }




        }
        private void Next_Click(object sender, RoutedEventArgs e)//Вывод в DataGrid предварительного просмотра заказа
        {
            
            Next.Visibility = Visibility.Visible;
            Next1.Visibility = Visibility.Visible;

            borderMain.Visibility = Visibility.Hidden;
            gridTop.Visibility = Visibility.Hidden; 

            //заполняем комбобокс
            int number = 3;
            for (int i = 1; i <= number; i++)
            {
                comboBox.Items.Add(i.ToString());
            }


            string connectionString = "Data Source=DB.db";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Получение максимального значения order_id из таблицы OrderItems
                int maxOrderId;
                using (SQLiteCommand command = new SQLiteCommand("SELECT MAX(order_id) FROM OrderComplete", connection))
                {
                    maxOrderId = Convert.ToInt32(command.ExecuteScalar());
                }
                maxOrderId++; // увеличение переменной на 1

                // Получение выделенных строк в datagrid1
                // Создать объект контекста базы данных
                var dbContext = new ApplicationContext();

                // Определить запрос на получение данных из таблицы Dishes
                var query = from o in dbContext.OrderItems
                            join d in dbContext.Dishes on o.dishID equals d.dishID
                            where o.order_id == maxOrderId // замените на нужный maxOrderID
                            select new { d.dishID, d.name, d.price };

                // Заполнить объект DataTable
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("dishID", typeof(int));
                dt.Columns.Add("name", typeof(string));
                dt.Columns.Add("price", typeof(int));
                foreach (var item in query)
                {
                    dt.Rows.Add(item.dishID, item.name, item.price);
                }

                // Назначить объект DataTable для отображения данных в контроле DataGrid
                dataGrid22.ItemsSource = dt.DefaultView;

            }
            Ex();
            Word();
            Txt();
        }

        private void Ex()
        {
            exApp.Workbooks.Add();
            Excel.Worksheet wsh = exApp.ActiveSheet as Excel.Worksheet;
            int i, j;
            for (i = 0; i < dataGrid22.Items.Count; i++)
            {
                for (j = 0; j < dataGrid22.Columns.Count; j++)
                {
                    wsh.Cells[i + 1, j + 1] = ((DataRowView)dataGrid22.Items[i])[j].ToString();
                }
            }
         
        }
        private void Word()
        {
            var wordDoc = wordApp.Documents.Add();
            var table = wordDoc.Tables.Add(wordDoc.Range(), dataGrid22.Items.Count, dataGrid22.Columns.Count);
            int i, j;
            for (i = 0; i < dataGrid22.Items.Count; i++)
            {
                for (j = 0; j < dataGrid22.Columns.Count; j++)
                {
                    table.Cell(i + 1, j + 1).Range.Text = ((DataRowView)dataGrid22.Items[i])[j].ToString();
                }
            }
           
        }
        private void Txt()
        {

            using (StreamWriter sw = new StreamWriter("output.txt"))
            {
                for (int i = 0; i < dataGrid22.Items.Count; i++)
                {
                    for (int j = 0; j < dataGrid22.Columns.Count; j++)
                    {
                        sw.Write(((DataRowView)dataGrid22.Items[i])[j].ToString());
                        sw.Write("\t"); //добавьте символ табуляции для разделения ячеек
                    }
                    sw.Write(sw.NewLine); // добавление переноса строки, чтобы перейти к следующей строке
                }
            }

           


        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            gridTop.Visibility = Visibility.Hidden;
            borderMain.Visibility = Visibility.Hidden;
            borderFirst.Visibility = Visibility.Visible;
        }

        private void OK_Click(object sender, RoutedEventArgs e)//Вывод суммы, предпоследний этап
        {
            Next.Visibility = Visibility.Hidden;
            Next1.Visibility = Visibility.Hidden;

            Final.Visibility = Visibility.Visible;
            Final1.Visibility = Visibility.Visible;
       
            string connectionString = "Data Source=DB.db";
            string query = "SELECT SUM(price) FROM OrderItems";

         
            num = comboBox.SelectedIndex;
            num++;
            if (Rb_There.IsChecked == true)
            {
                place = "На месте";
                
            }
            else
            {
                place = "На вынос";
                num = 0;
            }
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SQLiteCommand(query, connection))
                {
                    var result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        decimal sum = Convert.ToDecimal(result);

                        OrdersTextBlock.Text = ($"Номер стола: {num}, Место: {place}, ИТОГО: {sum}");
                    }
                    else
                    {
                        SumsTextBlock.Text = ("Sum of prices: 0");
                    }
                }
            }
        }

        public void Animation_Completed(object sender, EventArgs e)
        {
            animatedImage.Visibility = Visibility.Visible;
            DoubleAnimation fadeIn = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(new System.TimeSpan(0, 0, 2))
            };

            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(new System.TimeSpan(0, 0, 2))
            };

            fadeIn.Completed += FadeIn_Completed;

            animatedImage.BeginAnimation(OpacityProperty, fadeIn);
        }

        private void FadeIn_Completed(object sender, System.EventArgs e)
        {
            // Ждем 2 секунды перед началом анимации исчезновения картинки
            System.Threading.Thread.Sleep(2000);

            // Начинаем анимацию исчезновения картинки
            animatedImage.BeginAnimation(OpacityProperty, new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(new System.TimeSpan(0, 0, 2)) 
            });
        }
        private int GetUserId()
        {
            int id = Get.Value;
            return id;
        }
        private void CreateOrder_Click(object sender, RoutedEventArgs e)//Конечное оформление заказа, последний этап
        {
            borderFirst.Visibility = Visibility.Visible;
            Final.Visibility = Visibility.Hidden;
            Final1.Visibility = Visibility.Hidden;
            animatedImage.Visibility = Visibility.Visible;

            AnimationDelegate animationDelegate = new AnimationDelegate(Animation_Completed);
            animationDelegate.Invoke(sender, e);


            DateTime dateTime = DateTime.Now;
             string userName = "";
            using (ApplicationContext db = new ApplicationContext())
            {
               
                int Id = GetUserId();
                var user = db.Users.FirstOrDefault(u => u.userID == Id); // поиск пользователя в БД
                if (user != null)
                {
                    userName = user.name; // установка имени пользователя
                }
            }

            string param1 = "Не готов";
            string connectionString = "Data Source=DB.db";
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();

                string queryCopyData = "INSERT INTO OrderComplete (order_id, dishID, price, status, table_num, place, dataTime, worker) SELECT order_id, dishID, price, @status, @table_num, @place, @dateTime, @worker FROM OrderItems";
                string queryClear = "DELETE FROM OrderItems";

                using (SQLiteTransaction transaction = con.BeginTransaction())
                {
                    using (SQLiteCommand cmdCopyData = new SQLiteCommand(queryCopyData, con, transaction))
                    {
                        cmdCopyData.Parameters.AddWithValue("@status", param1);
                        cmdCopyData.Parameters.AddWithValue("@table_num", num);
                        cmdCopyData.Parameters.AddWithValue("@place", place);
                        cmdCopyData.Parameters.AddWithValue("@dateTime", dateTime);
                        cmdCopyData.Parameters.AddWithValue("@worker", userName);
                        cmdCopyData.ExecuteNonQuery();
                    }
                    using (SQLiteCommand cmdClear = new SQLiteCommand(queryClear, con, transaction))
                    {
                        cmdClear.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                con.Close();
            }

            if (email.Text != "")
            {
                string userNamee = "";
                string userMail= email.Text;

                using (ApplicationContext db = new ApplicationContext())
                {

                    int Id = GetUserId();
                    var user = db.Users.FirstOrDefault(u => u.userID == Id); // поиск пользователя в БД
                    if (user != null)
                    {
                        userNamee = user.name; // установка имени пользователя
                    }
                }

                message = $"Благодарим за посещение нашего ресторана, {userName}! Ваш чек:\n\n";
                for (int i = 0; i < dataGrid22.Items.Count; i++)
                {
                    for (int j = 0; j < dataGrid22.Columns.Count; j++)
                    {
                        message += ((DataRowView)dataGrid22.Items[i])[j].ToString() + "\t";
                    }
                    message += "\n";
                }
                string messageText = message; //текст

                SendMessage(userNamee,userMail,messageText);
            }

        }

        private void NOTOK_Click(object sender, RoutedEventArgs e)
        {
            borderFirst.Visibility = Visibility.Visible;
            Grid1.Visibility = Visibility.Visible;
            Next.Visibility = Visibility.Hidden;
            Next1.Visibility = Visibility.Hidden;

        }


        static void SendMessage(string userName, string adressTo, string messageText)
        {
            try
            {
                string from = @"kp.stankevich@mail.ru"; // адреса отправителя
                string pass = "uc3qjAhdAzCvQ5pkvXjU";  // пароль отправителя
                MailMessage mess = new MailMessage();
                mess.To.Add(adressTo); // адрес получателя
                mess.From = new MailAddress(from);
                mess.Body = messageText; // текст сообщения
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.mail.ru"; //smtp-сервер отправителя
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], pass);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mess); // отправка пользователю
                mess.To.Remove(mess.To[0]);
                mess.To.Add(from); //для сообщения на свой адрес
                mess.Subject = "Отправлено сообщение";
                mess.Body = "Пользователю " + userName + " отправлено сообщение";
                client.Send(mess); // отправка себе
                mess.Dispose();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void Exel_Click(object sender, RoutedEventArgs e)
        {
           
            exApp.Visible = true;
        }

        private void Word_Click(object sender, RoutedEventArgs e)
        {
            wordApp.Visible = true;

        }

        private void Txt_Click(object sender, RoutedEventArgs e)
        {
            string text = File.ReadAllText("output.txt");
            System.Windows.MessageBox.Show(text);
        }
    }
}
