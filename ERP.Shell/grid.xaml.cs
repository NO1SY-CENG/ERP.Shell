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
using System.Data;

namespace ERP.Shell
{
    /// <summary>
    /// Interaction logic for grid.xaml
    /// </summary>
    public partial class grid : Window
    {
        private DataView allCustomersView;
        public grid()
        {

            InitializeComponent();
            var dt = Database.GetCustomers();
            allCustomersView = dt.DefaultView;

            Dispatcher.Invoke(() =>
            {
                customersGrid.ItemsSource = allCustomersView;
            });

            SetupFilters();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
          

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow=new MainWindow();
            mainWindow.Show();
        }

        private void customersGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var grid = sender as DevExpress.Xpf.Grid.GridControl;
            if (grid == null)
                return;

            var clipRect = new RectangleGeometry()
            {
                Rect = new Rect(0, 0, grid.ActualWidth, grid.ActualHeight),
                RadiusX = 20,
                RadiusY = 20
            };
            grid.Clip = clipRect;
        }
        private void ApplyFilter(string departman, string sehir, string telefonTipi)
        {
            if (allCustomersView == null)
                return;

            var filters = new List<string>();

            if (!string.IsNullOrEmpty(departman) && departman != "Tümü")
                filters.Add($"Depart = '{departman.Replace("'", "''")}'");
            if (!string.IsNullOrEmpty(sehir) && sehir != "Tümü")
                filters.Add($"City = '{sehir.Replace("'", "''")}'");
            if (!string.IsNullOrEmpty(telefonTipi) && telefonTipi != "Tümü")
                filters.Add($"phoneType = '{telefonTipi.Replace("'", "''")}'");

            allCustomersView.RowFilter = string.Join(" AND ", filters);
        }


        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (allCustomersView == null)
                return;

            string departmanFilter = cmbDepartman?.SelectedItem?.ToString() ?? "";
            string sehirFilter = cmbSehir?.SelectedItem?.ToString() ?? "";
            string telefonTipiFilter = cmbTelefonTipi?.SelectedItem?.ToString() ?? "";

            var filters = new List<string>();

            if (!string.IsNullOrEmpty(departmanFilter) && departmanFilter != "Tümü")
                filters.Add($"Depart = '{departmanFilter.Replace("'", "''")}'");
            if (!string.IsNullOrEmpty(sehirFilter) && sehirFilter != "Tümü")
                filters.Add($"City = '{sehirFilter.Replace("'", "''")}'");
            if (!string.IsNullOrEmpty(telefonTipiFilter) && telefonTipiFilter != "Tümü")
                filters.Add($"phoneType = '{telefonTipiFilter.Replace("'", "''")}'");

            allCustomersView.RowFilter = string.Join(" AND ", filters);
        }

        private void SetupFilters()
        {
            var dt = Database.GetCustomers();

            var departmanlar = dt.AsEnumerable()
                                .Select(row => row.Field<string>("Depart"))
                                .Distinct()
                                .OrderBy(x => x)
                                .ToList();
            departmanlar.Insert(0, "Tümü");
            cmbDepartman.ItemsSource = departmanlar;

            var sehirler = dt.AsEnumerable()
                             .Select(row => row.Field<string>("City"))
                             .Distinct()
                             .OrderBy(x => x)
                             .ToList();
            sehirler.Insert(0, "Tümü");
            cmbSehir.ItemsSource = sehirler;

            var telefonTipleri = dt.AsEnumerable()
                                   .Select(row => row.Field<string>("phoneType"))
                                   .Distinct()
                                   .OrderBy(x => x)
                                   .ToList();
            telefonTipleri.Insert(0, "Tümü");
            cmbTelefonTipi.ItemsSource = telefonTipleri;

            cmbDepartman.SelectedIndex = 0;
            cmbSehir.SelectedIndex = 0;
            cmbTelefonTipi.SelectedIndex = 0;
        }



    }
}
