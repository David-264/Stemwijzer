using projectstemwijzer.DbClasses;
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
    /// Interaction logic for nieuwsbeheer.xaml
    /// </summary>
    public partial class nieuwsbeheer : Window
    {
        private readonly nieuwsdb database = new nieuwsdb();
        public nieuwsbeheer()
        {
            InitializeComponent();
            LaadDataGrid();
        }
        private void LaadDataGrid()
        {
            var nieuws = database.Getnieuws();
            uitkomstveld.ItemsSource = nieuws;
        }
        private void toevoegbtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titelbox.Text) || string.IsNullOrWhiteSpace(inhoudbox.Text))
            {
                MessageBox.Show("voer alle velden in");
                return;
            }
            database.VoegNieuwsToe(titelbox.Text, inhoudbox.Text);
            LaadDataGrid();
            titelbox.Text = string.Empty;
            inhoudbox.Text = string.Empty;
        }

        private void wijzigbtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (uitkomstveld.SelectedItem is nieuwsdb.nieuws geselecteerdNieuws)
            {
                wijzigwindow wijzig = new wijzigwindow("nieuwswindow", geselecteerdNieuws.nieuwsberichtID);
                wijzig.Show();
                wijzig.Closed += (s, args) => LaadDataGrid();
            }
            else
            {
                MessageBox.Show("Selecteer eerst een partij om te wijzigen.");
            }
        }

        private void verwijderbtn_Click(object sender, RoutedEventArgs e)
        {
            if (uitkomstveld.SelectedItem is nieuwsdb.nieuws geselecteerdNieuws)
            {
                database.VerwijderNieuws(geselecteerdNieuws.nieuwsberichtID);
                LaadDataGrid();
            }
            else
            {
                MessageBox.Show("Selecteer een nieuwsbericht om te verwijderen.");
            }
        }

        private void zoekbtn_Click(object sender, RoutedEventArgs e)
        {
            var nieuws = database.ZoekNieuws(titelbox.Text, inhoudbox.Text);
            uitkomstveld.ItemsSource = nieuws;
        }

        private void dashbordknop_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }
    }
}
