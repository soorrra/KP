using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3Racha.View
{
    /// <summary>
    /// Логика взаимодействия для RoomView.xaml
    /// </summary>
    public partial class RoomView : UserControl
    {
        private SoundPlayer player; // переменная для проигрывания звука
        public enum Colors
        {
            Red,
            Green,
            Blue
        }
        public RoomView()
        {
            InitializeComponent();
            var colorList = Enum.GetValues(typeof(Colors)).Cast<Colors>().ToList();
          
            player = new SoundPlayer(); // создаем экземпляр проигрывателя звука
            player.SoundLocation = ".\\melody.wav"; // указываем путь к файлу со звуком

            // Создаем анимацию для появления картинки
            DoubleAnimation fadeIn = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(new System.TimeSpan(0, 0, 2)) // Длительность анимации - 2 секунды
            };

            // Создаем анимацию для исчезновения картинки
            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(new System.TimeSpan(0, 0, 2)) // Длительность анимации - 2 секунды
            };

            // Устанавливаем обработчики событий для анимаций
            fadeIn.Completed += FadeIn_Completed;
            //fadeOut.Completed += FadeOut_Completed;

            // Начинаем анимацию появления картинки
            animatedImage.BeginAnimation(Image.OpacityProperty, fadeIn);
        }

        // Обработчик завершения анимации появления картинки
        private void FadeIn_Completed(object sender, System.EventArgs e)
        {
            // Ждем 2 секунды перед началом анимации исчезновения картинки
            //System.Threading.Thread.Sleep(2000);

            //// Начинаем анимацию исчезновения картинки
            //animatedImage.BeginAnimation(Image.OpacityProperty, new DoubleAnimation
            //{
            //    From = 1.0,
            //    To = 0.0,
            //    Duration = new Duration(new System.TimeSpan(0, 0, 2)) // Длительность анимации - 2 секунды
            //});
        }

        //// Обработчик завершения анимации исчезновения картинки

        //private async void FadeOut_Completed(object sender, System.EventArgs e)
        //{
        //    // Ждем 2 секунды перед началом анимации появления картинки
        //    await Task.Delay(2000);

        //    // Начинаем анимацию появления картинки
        //    animatedImage.BeginAnimation(Image.OpacityProperty, new DoubleAnimation
        //    {
        //        From = 0.0,
        //        To = 1.0,
        //        Duration = new Duration(new System.TimeSpan(0, 0, 2)) // Длительность анимации - 2 секунды
        //    });
        //}

        private void redact_Click(object sender, RoutedEventArgs e)
        {
            Thread soundThread = new Thread(new ThreadStart(PlaySound)); // создаем поток для проигрывания звука
            soundThread.Start(); // запускаем поток

        }
        private void PlaySound()
        {
            player.Play(); // воспроизводим звук
        }
        private void stop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }

        
    }
}