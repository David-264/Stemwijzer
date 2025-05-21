using System.Windows;
using projectstemwijzer.DbClasses;

namespace projectstemwijzer
{
    public partial class poletiekenpartijpagina : Window
    {
        private readonly PartijDatabase database = new PartijDatabase();

        public poletiekenpartijpagina()
        {
            InitializeComponent();
            LaadDataGrid();
        }

        private void LaadDataGrid()
        {
            var partijen = database.GetPartijen();
            uitkomstveld.ItemsSource = partijen;
        }

        private void zoekbtn_Click(object sender, RoutedEventArgs e)
        {
            var partijen = database.ZoekPartij(PartijTextBox.Text, infotextbox.Text);
            uitkomstveld.ItemsSource = partijen;
        }

        private void verwijderbtn_Click(object sender, RoutedEventArgs e)
        {
            if (uitkomstveld.SelectedItem is Partij geselecteerdePartij)
            {
                database.Verwijder(geselecteerdePartij.PartijId);
                LaadDataGrid();
            }
            else
            {
                MessageBox.Show("Selecteer eerst een partij om te verwijderen.");
            }
        }

        private void toevoegbtn_Click(object sender, RoutedEventArgs e)
        {
            database.VoegPartijToe(PartijTextBox.Text, infotextbox.Text);
            PartijTextBox.Clear();
            infotextbox.Clear();
            LaadDataGrid();
        }

        private void dashbordknop_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void wijzigbtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (uitkomstveld.SelectedItem is Partij geselecteerdePartij)
            {
                wijzigwindow wijzig = new wijzigwindow("poletiekenpartijpagina", geselecteerdePartij.PartijId);
                wijzig.Closed += (s, args) => LaadDataGrid(); // DataGrid herladen na sluiten
                wijzig.Show();
            }
            else
            {
                MessageBox.Show("Selecteer eerst een partij om te wijzigen.");
            }
        }
    }
}
