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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public enum Colors
        {
            Red,
            Green,
            Blue
        }
        public Help()
        {
            InitializeComponent();
            var colorList = Enum.GetValues(typeof(Colors)).Cast<Colors>().ToList();
            ColorComboBox.ItemsSource = colorList;
            ColorComboBox.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, (object s, EventArgs ev) =>
            {
                this.myDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            }, this.Dispatcher);
            timer.Start();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {

            About registrationview = new About();
            registrationview.Show();
            this.Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {

            Help registrationview = new Help();
            registrationview.Show();
            this.Close();
        }
        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedColor = (Colors)ColorComboBox.SelectedItem;
            switch (selectedColor)
            {
                case Colors.Red:
                    BrushGrid.Background = Brushes.Red;
                    break;
                case Colors.Green:
                    BrushGrid.Background = Brushes.Green;
                    break;
                case Colors.Blue:
                    BrushGrid.Background = Brushes.Blue;
                    break;
            }
        }
    }
}
