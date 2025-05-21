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

namespace projectstemwijzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void politiekknop_Click(object sender, RoutedEventArgs e)
        {
            poletiekenpartijpagina poletiekenpartijpagina = new poletiekenpartijpagina();
            poletiekenpartijpagina.Show();
            this.Close();
        }

        private void standpuntknop_Click(object sender, RoutedEventArgs e)
        {
            standpuntenpagina standpuntenpagina = new standpuntenpagina();
            standpuntenpagina.Show();
            this.Close();
        }

        private void standpuntperpartijknop_Click(object sender, RoutedEventArgs e)
        {
            standpuntperpartijpagina standpuntperpartijpagina = new standpuntperpartijpagina();
            standpuntperpartijpagina.Show();
            this.Close();
        }

        private void acountmaakknoo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void beheerknop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void resultaatknop_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}