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

namespace projectstemwijzer
{
    /// <summary>
    /// Interaction logic for standpuntenpagina.xaml
    /// </summary>
    public partial class standpuntenpagina : Window
    {
        private string window = "standpuntpagina";
        public standpuntenpagina()
        {
            InitializeComponent();
            window = "standpuntpagina";
        }
        private void toevoegbtn_Click(object sender, RoutedEventArgs e)
        {
            string standpunt = standpuntbox.Text;
            string omschrijving = omschrijvingbox.Text;
        }
        private void verwijderbtn_Click(object sender, RoutedEventArgs e)
        {
            int verwijderindex = resultaatveld.SelectedIndex;
        }
        private void wijzigbtn_Click(object sender, RoutedEventArgs e)
        {
            int wijzigindex = resultaatveld.SelectedIndex;
            wijzigwindow wijzig = new wijzigwindow(window, wijzigindex);
            wijzig.Show();
        }
        private void zoekbtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
