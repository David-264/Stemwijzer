using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace projectstemwijzer
{
    public class Nieuwsbericht
    {
        public string Titel { get; set; }
        public string Inhoud { get; set; }
        public DateTime Publicatiedatum { get; set; }
        public override string ToString() => Titel;
    }

    public partial class MainWindow : Window
    {
        public string InfoTextPartijen { get; set; }
        public string InfoTextGebruikers { get; set; }
        public List<Nieuwsbericht> Nieuwsberichten { get; set; }

        private readonly string connectionString = "Server=localhost;Port=3309;Database=stemwijzer;Uid=root;Pwd=;";

        public MainWindow()
        {
            InitializeComponent();

            Nieuwsberichten = new List<Nieuwsbericht>();

            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();

                string queryPartijen = "SELECT COUNT(partijID) FROM partijen";
                using var cmdPartijen = new MySqlCommand(queryPartijen, connection);
                object resultPartijen = cmdPartijen.ExecuteScalar();
                int aantalPartijen = resultPartijen != null ? Convert.ToInt32(resultPartijen) : 0;
                InfoTextPartijen = $"Er zijn momenteel: {aantalPartijen} partijen";

                string queryGebruikers = "SELECT COUNT(gebruikerID) FROM gebruikers";
                using var cmdGebruikers = new MySqlCommand(queryGebruikers, connection);
                object resultGebruikers = cmdGebruikers.ExecuteScalar();
                int aantalGebruikers = resultGebruikers != null ? Convert.ToInt32(resultGebruikers) : 0;
                InfoTextGebruikers = $"Er zijn momenteel: {aantalGebruikers} gebruikers";

                string queryNieuws = "SELECT titel, inhoud, publicatiedatum FROM nieuwsberichten";
                using var cmdNieuws = new MySqlCommand(queryNieuws, connection);
                using var reader = cmdNieuws.ExecuteReader();
                while (reader.Read())
                {
                    Nieuwsbericht bericht = new Nieuwsbericht
                    {
                        Titel = reader.GetString("titel"),
                        Inhoud = reader.GetString("inhoud"),
                        Publicatiedatum = reader.GetDateTime("publicatiedatum")
                    };
                    Nieuwsberichten.Add(bericht);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij ophalen van data: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                InfoTextPartijen = "Er is een fout opgetreden bij partijen.";
                InfoTextGebruikers = "Er is een fout opgetreden bij gebruikers.";
            }

            DataContext = this;
        }

        private void NieuwsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NieuwsList.SelectedItem is Nieuwsbericht geselecteerd)
            {
                TitelText.Text = geselecteerd.Titel;
                InhoudText.Text = geselecteerd.Inhoud;
                DatumText.Text = geselecteerd.Publicatiedatum.ToString("dd-MM-yyyy");
            }
            else
            {
                TitelText.Text = "";
                InhoudText.Text = "";
                DatumText.Text = "";
            }
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

        private void acountmaakknoo_Click(object sender, RoutedEventArgs e)
        {
            acountpagina acountpagina = new acountpagina();
            acountpagina.Show();
            this.Close();
        }

        private void nieuwsbeheer_Click(object sender, RoutedEventArgs e)
        {
            nieuwsbeheer nieuwsbeheer = new nieuwsbeheer();
            nieuwsbeheer.Show();
            this.Close();
        }

        private void verkiezingen_Click(object sender, RoutedEventArgs e)
        {
            verkiezingspagina verkiezingspagina = new verkiezingspagina();
            verkiezingspagina.Show();
            this.Close();
        }
    }
}
