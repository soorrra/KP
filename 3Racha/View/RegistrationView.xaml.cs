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
using _3Racha.View;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Reflection.Emit;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
   
    public partial class RegistrationView : Window
    { 
        string mmail = "", nname = "", message = "";
        int pro = 0;
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LoginView v = new LoginView();
       
            v.Show();
            Hide();
        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void number_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void createAcc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string postt="";
                string status = "";
                string adressTo = "";
                string userName = "";
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
                    if (postt=="Официант")
                    {
                        status = "waiter";
                    }
                    if (postt == "Повар")
                    {
                        status = "cook";
                    }
                    if (isEmailUnique && isphnUnique && isLoginUnique && status!="" && name.Text != "" && surname.Text != "" && number.Text != "" && mail.Text != "" && login.Text != "" && password.Text != "")
                    {
                        db.Users.Add(new User
                        {
                            name = name.Text,
                            surname = surname.Text,
                             email = mail.Text,
                            phone_number = number.Text,
                            login = login.Text,
                            password = password.Text,
                            post=status
                        });;
                        message = $"Добро прожаловать, {name}! Благодарим за регистрацию";
                        userName= name.Text;
                        adressTo=mail.Text;
                        db.SaveChanges();
                        LoginView v = new LoginView();
                       v.Show();
                        Hide();  
                         string messageText = message; //текст
                         //SendMessage(userName, adressTo, messageText);
           
                       
                    }
                    else throw new Exception("Не все поля заполнены корректно!");
                }
                
               
            
               }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
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
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}
