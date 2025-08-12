using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic.ApplicationServices;

namespace ERP.Shell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int userID;
        public MainWindow()
        {
            InitializeComponent();
            subMenuGrid.Visibility = Visibility.Hidden;
        }
        public void setID(int _userID)
        {
            this.userID = _userID;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {  if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
        private void Border_MouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;



            if (border.Name == "AccountBut")
            {
                subMenuGrid.Visibility = Visibility.Visible;
                return;
            }
            if (border.Name == "adminBorder")
            {
                adminPanel AP=new adminPanel();
                AP.Show();
                return;
            }

            subMenuGrid.Visibility = Visibility.Hidden;
        }
        private void Border_MouseLeftButtonDown2(object sender, MouseButtonEventArgs e)
        {
            subMenuGrid.Visibility = Visibility.Hidden;
            grid mainWindow = new grid();
            mainWindow.Closed += (s, args) => this.Close();
            mainWindow.Show();
            this.Hide();
           
           
        }

        private void HamburgerBorder_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            hamburgerColm.Width = new GridLength(5, GridUnitType.Star);
        }

        private void HamburgerBorder_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            hamburgerColm.Width = new GridLength(1.5, GridUnitType.Star);
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchText.Text.Trim().ToLower();

            foreach (var child in MainGrid.Children)
            {
                if (child is System.Windows.Controls.Border border)
                {
                    if (border.Child is System.Windows.Controls.StackPanel stackPanel)
                    {
                        var label = stackPanel.Children.OfType<System.Windows.Controls.Label>().FirstOrDefault();
                        if (label != null)
                        {
                            string labelText = label.Content?.ToString().ToLower() ?? "";

                            if (!string.IsNullOrEmpty(searchText) && labelText.Contains(searchText))
                            {
                                border.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.SeaGreen);
                            }
                            else
                            {
                                border.Background = System.Windows.Media.Brushes.Transparent;
                            }
                        }
                    }
                }
            }
        }
    }
}