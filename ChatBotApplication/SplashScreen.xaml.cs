using System;
using System.Media;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace ChatBotApplication
{
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

            // ✅ Start animations
            FadeAndSlideIn(TitleText, 0.2);
            FadeAndSlideIn(SubtitleText, 0.8);
            FadeAndSlideIn(FooterText, 1.4);
        }

        private void GetStarted_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

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

            // 👇 Fix: Ensure RenderTransform is a TranslateTransform
            if (!(element.RenderTransform is TranslateTransform))
                element.RenderTransform = new TranslateTransform();

            (element.RenderTransform as TranslateTransform)?.BeginAnimation(TranslateTransform.YProperty, move);
        }
    }
}
