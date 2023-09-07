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

namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для OrderFinishView.xaml
    /// </summary>
    public partial class OrderFinishView : UserControl
    {
        public OrderFinishView()
        {
            InitializeComponent();
            // Заполнение данных

            // Установить соединение с базой данных SQLite
            string connectionString = "Data Source=DB.db";
            using (var connection = new SQLiteConnection(connectionString))
            {
                // Открыть соединение с базой данных
                connection.Open();

                // Выбрать данные из таблицы с нужными полями
                string query = "SELECT * FROM OrderComplete WHERE status='Выдан' ORDER BY order_id";
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
    }
}
