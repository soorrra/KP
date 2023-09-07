using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataGrid = System.Windows.Controls.DataGrid;

namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для RedactDishView.xaml
    /// </summary>
    public partial class RedactDishView : Window
    {
        public RedactDishView()
        {
            InitializeComponent();

            DishesView dishesView = new DishesView();
            DataGrid dataGrid1 = (DataGrid)dishesView.FindName("dataGrid1");
            DataRowView selectedRow = (DataRowView)dataGrid1.SelectedItem;
            if (selectedRow != null)
            {
                string dishID = selectedRow["dishID"].ToString();
                string name = selectedRow["name"].ToString();
                string price = selectedRow["price"].ToString();
                string weight = selectedRow["weight"].ToString();
                string category = selectedRow["category"].ToString();
                Name.Text = name;
                pprice.Text = price;
                wweight.Text = weight;
                Category.SelectedValue = category;
            }
            else
            {
                // обработка случая, когда строка не выбрана
            }
        }
    }
}
