using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для AddDishView.xaml
    /// </summary>
    public partial class AddDishView : Window
    {
        public AddDishView()
        {
            InitializeComponent();
        }
        private void Border_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void createAcc_Click(object sender, RoutedEventArgs e)
        { 
            int maxOrderId;
            try
            {
               

                using (ApplicationContext db = new ApplicationContext())
                {

                    string connectionString = "Data Source=DB.db";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        // Получение максимального значения order_id из таблицы OrderItems
                       
                        using (SQLiteCommand command = new SQLiteCommand("SELECT MAX(dishID) FROM Dishes", connection))
                        {
                            maxOrderId = Convert.ToInt32(command.ExecuteScalar());
                        }
                        maxOrderId++; // увеличение переменной на 1
                                      // Получение выделенных строк в datagrid1
                    }
                        bool isNameUnique = !db.Dishes.Any(u => u.name == Name.Text);
                    if (!isNameUnique)
                    {
                        throw new Exception("Данное блюдо уже внесено в базу данных, используйте другую.");
                    }
                    
                    if (isNameUnique && Name.Text != "" && price.Text != "" && weight.Text != ""&& Category.SelectedItem!=null)
                    {
                        db.Dishes.Add(new Dish
                        {
                            dishID=maxOrderId,
                            name = Name.Text,
                            category = Category.Text,
                            price = Convert.ToInt32(price.Text),
                            weight = Convert.ToInt32(weight.Text)

                        });;;;;
                        db.SaveChanges();
                       this.Close();
                    }
                    else throw new Exception("Не все поля заполнены корректно!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}


