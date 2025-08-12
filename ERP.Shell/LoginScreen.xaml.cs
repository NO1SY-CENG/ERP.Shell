using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;



// Namespace karışıklığını önlemek için alias
using MediaColor = System.Windows.Media.Color;
using MediaColorConverter = System.Windows.Media.ColorConverter;
using MediaButton = System.Windows.Controls.Button;
using MediaMouseEventArgs = System.Windows.Input.MouseEventArgs;


namespace ERP.Shell
{
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
            invalidText.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<User> userlist = Database.GetAllUsers();

            foreach (User user in userlist)
            {
                if (user.Username == userText.Text && user.Password == pswdText.Password)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.setID(user.Id);
                    this.Hide();
                    mainWindow.Closed += (s, args) => this.Close();
                    mainWindow.Show();
                    break;
                }
                else invalidText.Visibility = Visibility.Visible;
            }
        }

        private void saveBut_MouseEnter(object sender, MediaMouseEventArgs e)
        {
            if (sender is MediaButton button)
                AnimateButtonForeground(button, "#FF18AF0A", MyGradientStop);
        }

        private void saveBut_MouseLeave(object sender, MediaMouseEventArgs e)
        {
            if (sender is MediaButton button)
                AnimateButtonForeground(button, "#FF9CA6A7",MyGradientStop);
        }

        private void AnimateButtonForeground(MediaButton button, string hexColor, GradientStop gradientStop = null)
        {
            var targetColor = (MediaColor)MediaColorConverter.ConvertFromString(hexColor);

            var colorAnimation = new ColorAnimation
            {
                To = targetColor,
                Duration = new Duration(TimeSpan.FromMilliseconds(200))
            };

            if (button.Foreground is not SolidColorBrush brush)
            {
                brush = new SolidColorBrush(Colors.Transparent);
                button.Foreground = brush;
            }

            brush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

            if (gradientStop != null)
            {
                var gradientAnimation = new ColorAnimation
                {
                    To = targetColor,
                    Duration = new Duration(TimeSpan.FromMilliseconds(200))
                };

                gradientStop.BeginAnimation(GradientStop.ColorProperty, gradientAnimation);
            }
        }




    }
}

