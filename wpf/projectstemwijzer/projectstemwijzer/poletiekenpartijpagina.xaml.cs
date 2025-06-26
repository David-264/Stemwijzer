using System.Windows;
using projectstemwijzer.DbClasses;
using System.IO;
using System.Windows.Media.Imaging;

namespace projectstemwijzer
{
    public partial class poletiekenpartijpagina : Window
    {
        private readonly PartijDatabase database = new PartijDatabase();
        private byte[] geselecteerdeFoto;

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
                var resultaat = MessageBox.Show(
                    $"Weet je zeker dat je de partij '{geselecteerdePartij.Naam}' wilt verwijderen?, als je deze verwijdert gaan de standpunten van deze partij ook weg",
                    "Bevestiging",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (resultaat == MessageBoxResult.Yes)
                {
                    database.Verwijder(geselecteerdePartij.PartijId);
                    LaadDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Selecteer eerst een partij om te verwijderen.");
            }
        }


        private void toevoegbtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PartijTextBox.Text) || string.IsNullOrWhiteSpace(infotextbox.Text))
            {
                MessageBox.Show("voer alle velden in");
                return;
            }

            database.VoegPartijToe(PartijTextBox.Text, infotextbox.Text);

            if (geselecteerdeFoto != null)
            {
                var partijen = database.GetPartijen();
                var laatstToegevoegd = partijen.OrderByDescending(p => p.PartijId).FirstOrDefault();
                if (laatstToegevoegd != null)
                {
                    database.VoegOfWijzigFoto(laatstToegevoegd.PartijId, geselecteerdeFoto);
                }
                geselecteerdeFoto = null;
            }

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
                wijzig.Show();
                wijzig.Closed += (s, args) => LaadDataGrid();
            }
            else
            {
                MessageBox.Show("Selecteer eerst een partij om te wijzigen.");
            }
        }

        private void UploadFoto_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Afbeeldingen|*.jpg;*.jpeg;*.png;*.bmp";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filePath = dlg.FileName;
                geselecteerdeFoto = File.ReadAllBytes(filePath);
            }
        }

    }
}
