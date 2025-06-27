using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ChatBotApplication
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath = "voice_greeting.wav";
                SoundPlayer player = new SoundPlayer(filePath);
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to play audio: " + ex.Message);
            }

            // Animate text elements
            FadeAndSlideIn(TitleText, 0);
            FadeAndSlideIn(SubtitleText, 0.5);
            FadeAndSlideIn(FooterText, 1);
        }

        private void GetStarted_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void SplashVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            SplashVideo.Position = TimeSpan.Zero;
            SplashVideo.Play();
        }

        // 🎯 Animates opacity and vertical position (fade & slide up)
        private void FadeAndSlideIn(UIElement element, double delaySeconds)
        {
            var fade = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1))
            {
                BeginTime = TimeSpan.FromSeconds(delaySeconds),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            var move = new DoubleAnimation(20, 0, TimeSpan.FromSeconds(1))
            {
                BeginTime = TimeSpan.FromSeconds(delaySeconds),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            element.BeginAnimation(UIElement.OpacityProperty, fade);
            if (element.RenderTransform is TranslateTransform transform)
            {
                transform.BeginAnimation(TranslateTransform.YProperty, move);
            }
        }
    }
}
