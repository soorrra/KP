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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для MenuView.xaml
    /// </summary>
    public partial class MenuView : System.Windows.Controls.UserControl
    {
        string selectedCategory;

        public MenuView()
        {
            InitializeComponent();

            //using (ApplicationContext db = new ApplicationContext())//енам по модели
            //{
            //    var dishes = db.Dishes.ToList();
            //    // Получаем неповторяющиеся значения моделей
            //    var dishh = dishes.Select(c => c.category).Distinct().ToArray();

            //    btn1.Content = dishh[0];
            //    btn2.Content = dishh[1];
            //    btn3.Content = dishh[2];
            //    btn4.Content = dishh[3];
            //    btn5.Content = dishh[4];


            //}

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
                string query = $"SELECT name, price, weight FROM Dishes WHERE category='{selectedCategory}'";
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
            back_btn.Visibility= Visibility.Visible;
            selectedCategory = "Салаты / Salads";
            Fill_DataBase(selectedCategory);
            borderMain.Visibility = Visibility.Visible;
            borderFirst.Visibility = Visibility.Hidden;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = "Супы / Soups";
            Fill_DataBase(selectedCategory);
            borderMain.Visibility = Visibility.Visible;
            borderFirst.Visibility = Visibility.Hidden;
            back_btn.Visibility = Visibility.Visible;

        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = "Рыбные блюда / Fish dishes";
            Fill_DataBase(selectedCategory);
            borderMain.Visibility = Visibility.Visible;
            borderFirst.Visibility = Visibility.Hidden;
            back_btn.Visibility = Visibility.Visible;

        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = "Мясные блюда / Meat dishes";
            Fill_DataBase(selectedCategory);
            borderMain.Visibility = Visibility.Visible;
            borderFirst.Visibility = Visibility.Hidden;
            back_btn.Visibility = Visibility.Visible;

        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = "Десерты / Desserts";
            Fill_DataBase(selectedCategory);
            borderMain.Visibility = Visibility.Visible;
            borderFirst.Visibility = Visibility.Hidden;
            back_btn.Visibility = Visibility.Visible;

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            back_btn.Visibility = Visibility.Hidden;
            borderMain.Visibility = Visibility.Hidden;
            borderFirst.Visibility = Visibility.Visible;
        }
    }
}

