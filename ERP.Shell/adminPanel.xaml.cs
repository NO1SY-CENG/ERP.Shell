using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace ERP.Shell
{
    /// <summary>
    /// Interaction logic for adminPanel.xaml
    /// </summary>
    public partial class adminPanel : Window
    {
        public adminPanel()
        {
            InitializeComponent();
            alertText.Visibility = Visibility.Collapsed;
            saveBut.Visibility = Visibility.Collapsed;
        }

        private void saveBut_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button)
                AnimateButtonForeground(button, "#FF18AF0A", MyGradientStop);
        }

        private void saveBut_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button)
                AnimateButtonForeground(button, "#FF9CA6A7", MyGradientStop);
        }

        private void AnimateButtonForeground(System.Windows.Controls.Button button, string hexColor, GradientStop gradientStop = null)
        {
            var targetColor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(hexColor);

            if (!(button.Foreground is System.Windows.Media.SolidColorBrush brush))
            {
                brush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
                button.Foreground = brush;
            }

            // Foreground rengi animasyonu
            var colorAnimation = new System.Windows.Media.Animation.ColorAnimation
            {
                To = targetColor,
                Duration = TimeSpan.FromMilliseconds(200)
            };

            brush.BeginAnimation(System.Windows.Media.SolidColorBrush.ColorProperty, colorAnimation);

            // GradientStop animasyonu
            if (gradientStop != null)
            {
                var gradientAnimation = new System.Windows.Media.Animation.ColorAnimation
                {
                    To = targetColor,
                    Duration = TimeSpan.FromMilliseconds(200)
                };

                gradientStop.BeginAnimation(GradientStop.ColorProperty, gradientAnimation);
            }
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Database.InsertUser(userText.Text.ToString(), pswdText.Text.ToString());
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private static readonly Regex yasakliKarakterler = new Regex(@"[;'\-\\/*%&=""]");

        private void uPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (yasakliKarakterler.IsMatch(e.Text))
            {
                e.Handled = true; // Yasaklı karakter engellendi
                alertText.Visibility = Visibility.Visible;
            }
            else
            {
                alertText.Visibility = Visibility.Collapsed;
            }
        }

        private void u_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            string userTextValue = userText.EditValue?.ToString() ?? "";
            string pswdTextValue = pswdText.EditValue?.ToString() ?? "";

            bool gecersizMi = yasakliKarakterler.IsMatch(userTextValue) ||
                              yasakliKarakterler.IsMatch(pswdTextValue);

            bool alanlarDolu = !string.IsNullOrWhiteSpace(userTextValue) &&
                               !string.IsNullOrWhiteSpace(pswdTextValue);

            alertText.Visibility = gecersizMi ? Visibility.Visible : Visibility.Collapsed;
            saveBut.Visibility = (alanlarDolu && !gecersizMi) ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
