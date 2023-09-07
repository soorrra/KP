using System;
using System.Windows;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Data.SQLite;
using _3Racha.View;
using System.Linq;
using System.Windows.Controls;

namespace _3Racha
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public int userID;

        public LoginView()
        {
           
            InitializeComponent();
            

        }

        private void cl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool access = false;
                using (ApplicationContext db = new ApplicationContext())
                {
                    var users = db.Users.ToList();
                    foreach (User c in users)
                    {
                        if (login.Text == c.login && password.Text == c.password)
                        {
                            access = true;
                            userID = c.userID;
                        }
                    }
                }
                //if (login.Text == "admin" && password.Text == "admin")
                //{
                //    AdminView adminview = new AdminView();
                //    adminview.Show();
                //    Close();
                //}
                //else if (login.Text == "waiter" && password.Text == "waiter")
                //{
                //    WaiterView waiterview = new WaiterView();
                //    waiterview.Show();
                //    Close();
                //}
                //else if (login.Text == "cook" && password.Text == "cook")
                //{
                //    CookView cookview= new CookView();
                //    cookview.Show();
                //    Close();
                //}
               if (access == true)
                {
                    Get.Value = userID;
                    // Подключение к базе данных SQLite
                    SQLiteConnection conn = new SQLiteConnection("Data Source=DB.db");
                    conn.Open();

                    // Создание SQL-запроса для получения name по dishID
                    string sql = "SELECT post FROM Users WHERE userID=@userID";
                    SQLiteCommand command = new SQLiteCommand(sql, conn);
                    command.Parameters.AddWithValue("@userID", userID);

                    // Выполнение запроса и получение данных
                    string post = (string)command.ExecuteScalar();

                    // Закрытие соединения с базой данных
                    conn.Close();
                    if (post == "waiter") 
                    {
                        WaiterView ac = new WaiterView();
                        ac.Show();
                        this.Close();
                    }
                    else if (post == "cook")
                    {
                        CookView cookview = new CookView();
                        cookview.Show();
                        Close();
                    }
                    else if (post == "admin")
                    {
                        AdminView adminview = new AdminView();
                        adminview.Show();
                        Close();
                    }
                }
                else if (access == false)
                {
                    throw new Exception("Несоответствие логина или пароля, попробуйте снова.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }



        }
        private void Border_KeyDown(object sender, KeyEventArgs e)
        {
          
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void registration_Click(object sender, RoutedEventArgs e)
        {

            RegistrationView registrationview = new RegistrationView();
            registrationview.Show();
            this.Close();
        }
        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Hide();
            }
        }
        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Help helpview = new Help();
            helpview.Owner = this;
            helpview.ShowDialog();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            About aboutview = new About();
            aboutview.Owner = this;
            aboutview.ShowDialog();
        }
    }
}
