using MySql.Data.MySqlClient;
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
using static projectstemwijzer.standpuntenpagina;

namespace projectstemwijzer
{
    /// <summary>
    /// Interaction logic for standpuntenpagina.xaml
    /// </summary>
    public partial class standpuntenpagina : Window
    {
        private readonly string connectionString = "Server=localhost;Port=3309;Database=stemwijzer;Uid=root;Pwd=;";
        private readonly Standpuntdatabase db = new Standpuntdatabase();
        public standpuntenpagina()
        {
            InitializeComponent();
            LaadDataGrid();
            LaadPartijen();
        }
        private void LaadPartijen()
        {
            var partijen = GetPartijen();
            partijComboBox.ItemsSource = partijen;
        }

        public List<Partij> GetPartijen()
        {
            var partijen = new List<Partij>();
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return partijen;
            }

            string query = "SELECT partijID, partijnaam FROM partijen";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var partijId = reader.IsDBNull(reader.GetOrdinal("partijID")) ? 0 : reader.GetInt32("partijID");
                var partijNaam = reader.IsDBNull(reader.GetOrdinal("partijnaam")) ? "" : reader.GetString("partijnaam");

                partijen.Add(new Partij
                {
                    PartijID = partijId,
                    Partijnaam = partijNaam
                });
            }

            return partijen;
        }

        public class Partij
        {
            public int PartijID { get; set; }
            public string Partijnaam { get; set; }
        }
        private void LaadDataGrid()
        {
            var standpunten = db.Getstandpunten();
            standpuntenveld.ItemsSource = standpunten;
        }
        private void toevoegbtn_Click(object sender, RoutedEventArgs e)
        {
            if(standpuntbox.Text == "")
            {
                MessageBox.Show("voer een standpunt in");
                return;
            }
            if (partijComboBox.SelectedItem is Partij geselecteerdePartij)
            {
                db.VoegPartijToe(standpuntbox.Text, geselecteerdePartij.PartijID);
                standpuntbox.Clear();
                partijComboBox.SelectedIndex = -1;
                LaadDataGrid();
            }
            else
            {
                MessageBox.Show("Selecteer een partij.");
            }

            standpuntbox.Clear();
            LaadDataGrid();
        }
        private void verwijderbtn_Click(object sender, RoutedEventArgs e)
        {
            if (standpuntenveld.SelectedItem is standpunt geselecteerdestandpunt)
            {
                db.Verwijder(geselecteerdestandpunt.StandpuntID);
                LaadDataGrid();
            }
            else
            {
                MessageBox.Show("Selecteer eerst een partij om te verwijderen.");
            }
        }
        private void zoekbtn_Click(object sender, RoutedEventArgs e)
        {
            var standpunten = db.ZoekStandpunt(standpuntbox.Text, partijComboBox.Text);
            standpuntenveld.ItemsSource = standpunten;
        }


        private void dashbordknop_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void wijzigbtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (standpuntenveld.SelectedItem is standpunt gekozenstandpunt)
            {
                wijzigwindow wijzig = new wijzigwindow("standpuntpagina", gekozenstandpunt.StandpuntID);
                wijzig.Show();
                wijzig.Closed += (s, args) => LaadDataGrid(); // DataGrid herladen na sluiten
            }
        }
    }
}
