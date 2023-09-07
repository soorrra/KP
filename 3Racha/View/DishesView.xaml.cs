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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для DishesView.xaml
    /// </summary>
    public partial class DishesView : System.Windows.Controls.UserControl
    {
        string selectedCategory;
        public DishesView()
        {
            
            InitializeComponent();
            using (ApplicationContext db = new ApplicationContext())//енам по модели
            {
                var dishes = db.Dishes.ToList();
                // Получаем неповторяющиеся значения моделей
                //var dishh = dishes.Select(c => c.category).Distinct().ToArray();
                
                //btn1.Content = dishh[0];
                //btn2.Content = dishh[1];
                //btn3.Content = dishh[2];
                //btn4.Content = dishh[3];
                //btn5.Content = dishh[4];


            }
           
        }
        public void Fill_DataBase(string selectedCategory)
        {
            // ____________Заполнение__________

            // Установить соединение с базой данных SQLite
            string connectionString = "Data Source=DB.db";
            using (var connection = new SQLiteConnection(connectionString))
            {
                // Открыть соединение с базой данных
                connection.Open();

                // Выбрать данные из таблицы с нужными полями
                string query = $"SELECT*FROM Dishes WHERE category='{selectedCategory}'";
                using (var command = new SQLiteCommand(query, connection))
                {
                    // Создать адаптер данных и заполнить набор данных
                    var dataAdapter = new SQLiteDataAdapter(command);
                    var dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);

                    // Установить первому столбцу свойство Visibility.Hidden
                    // Установить первому столбцу свойство Visibility.Hidden
                    //dataGrid1.Columns[0].Visibility = Visibility.Hidden;
                    // Показать данные в DataGrid
                    dataGrid1.ItemsSource = dataSet.Tables[0].DefaultView;
                }
            }
        }
        //______________Удаление________________
        private void Button_Click(object sender, RoutedEventArgs e)

        {
            DataRowView dataRowView = (DataRowView)dataGrid1.SelectedItem; // Получаем выделенный элемент
            int dishID = Convert.ToInt32(dataRowView["dishID"]); // Получаем значение поля Id выделенного элемента
            string connectionString = "Data Source=DB.db";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = $"DELETE FROM Dishes WHERE dishID = {dishID}";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                int result = cmd.ExecuteNonQuery();
                connection.Close();
            }
            dataRowView.Row.Delete();// Удаляем выделенный элемент из DataTable, связанного с DataGrid
        }

        private void redact_Click(object sender, RoutedEventArgs e)
        {   
       
            dataGrid1.IsReadOnly = false;
            redOk.Visibility = Visibility.Visible;

            //DataRowView selectedRow = (DataRowView)dataGrid1.SelectedItem;
            //string name = selectedRow["name"].ToString();
            //string price = selectedRow["price"].ToString();
            //string weight = selectedRow["weight"].ToString();
            //string category = selectedRow["category"].ToString();
            //Name.Text = name;
            //pprice.Text = price;
            //wweight.Text = weight;
            //Category.SelectedValue = category;
            // Обработчик нажатия кнопки сохранения изменений
           
               
            


        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            //______________добавление_________________
            AddDishView addstaffview = new AddDishView();
            addstaffview.Show();

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
                            int dishID = Convert.ToInt32(row["dishID"]);
                            string name = Convert.ToString(row["name"]);
                            string category = Convert.ToString(row["category"]);
                            decimal price = Convert.ToDecimal(row["price"]);

                            // Обновление записи в бд SQLite
                            string updateQuery = "UPDATE Dishes SET name=@name, category=@category, price=@price WHERE dishID=@dishID;";
                            using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                            {
                                command.Parameters.AddWithValue("@name", name);
                                command.Parameters.AddWithValue("@category", category);
                                command.Parameters.AddWithValue("@price", price);
                                command.Parameters.AddWithValue("@dishID", dishID);

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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.Visibility = Visibility.Hidden;
            gridTop.Visibility = Visibility.Hidden;
            borderFirst.Visibility = Visibility.Visible; 
            borderMain.Visibility = Visibility.Hidden; 
        }
    }
}
