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
using System.Threading;
using _3Racha.View;
using System.Globalization;
using System.Text.RegularExpressions;



namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для AddStaffView.xaml
    /// </summary>
    public partial class AddStaffView : Window
    {
        public AddStaffView()
        {
            InitializeComponent();
        }


        private void Border_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void createAcc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
             

                string postt = "";
                string status = "";
                postt = post.Text;

                string email = mail.Text;//проверка емэйла
                Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!emailRegex.IsMatch(email))
                {
                    throw new Exception("Некорректная почта!");
                }
                string phoneNumber = number.Text;

                using (ApplicationContext db = new ApplicationContext())
                {
                    bool isEmailUnique = !db.Users.Any(u => u.email == mail.Text);
                    if (!isEmailUnique)
                    {
                        throw new Exception("Данная почта уже занята, используйте другую.");
                    }
                    bool isphnUnique = !db.Users.Any(u => u.phone_number == number.Text);
                    if (!isphnUnique)
                    {
                        throw new Exception("Данная номер телефона уже занят, используйте другой.");
                    }
                    bool isLoginUnique = !db.Users.Any(u => u.login == login.Text);
                    if (!isLoginUnique)
                    {
                        throw new Exception("Данный логин уже занят, используйте другой.");
                    }

                    if (login.Text.Length < 4)
                    {
                        throw new Exception("Длина логина не должна быть меньше 4 символов");
                    }
                    if (login.Text.Length > 16)
                    {
                        throw new Exception("Длина логина не должна превышать 16 символов");
                    }

                    if (password.Text.Length < 4)
                    {
                        throw new Exception("Длина пароля не должна быть меньше 4 символов");
                    }
                    if (password.Text.Length > 16)
                    {
                        throw new Exception("Длина пароля не должна превышать 16 символов");
                    }
                    if (postt == "Официант")
                    {
                        status = "waiter";
                    }
                    if (postt == "Повар")
                    {
                        status = "cook";
                    }
                    if (postt == "Администратор")
                    {
                        status = "admin";
                    }
                    if (isEmailUnique && isphnUnique && isLoginUnique && status != "" && name.Text != "" && surname.Text != "" && number.Text != "" && mail.Text != "" && login.Text != "" && password.Text != "")
                    {
                        db.Users.Add(new User
                        {
                            name = name.Text,
                            surname = surname.Text,
                            email = mail.Text,
                            phone_number = number.Text,
                            login = login.Text,
                            password = password.Text,
                            post = status

                        });
                        db.SaveChanges();
                     
                        Hide();
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


