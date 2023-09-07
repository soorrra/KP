using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для WaiterView.xaml
    /// </summary>
    public partial class WaiterView : Window
    {
        public WaiterView()
        {
            InitializeComponent();
        }
    
    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        WindowInteropHelper helper = new WindowInteropHelper(this);
        SendMessage(helper.Handle, 161, 2, 0);
    }
    private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
    {
        this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
    }
    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
    private void btnMinimize_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }
    private void btnMaximize_Click(object sender, RoutedEventArgs e)
    {
        if (this.WindowState == WindowState.Normal)
            this.WindowState = WindowState.Maximized;
        else this.WindowState = WindowState.Normal;
    }

    private void RadioButton_Checked(object sender, RoutedEventArgs e)
    {

    }

    private void rb_Exit_Click(object sender, RoutedEventArgs e)
    {
        LoginView loginview = new LoginView();
        loginview.Show();
        this.Close();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Button1_Click(object sender, RoutedEventArgs e)
    {
        About aboutview = new About();
        aboutview.Owner = this;
        aboutview.ShowDialog();

    }

    private void Button2_Click(object sender, RoutedEventArgs e)
    {
        Help helpview = new Help();
        helpview.Owner = this;
        helpview.ShowDialog();

    }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
