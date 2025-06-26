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
using static projectstemwijzer.DbClasses.verkiezingsdb;

namespace projectstemwijzer
{
    /// <summary>
    /// Interaction logic for verkiezingspagina.xaml
    /// </summary>
    public partial class verkiezingspagina : Window
    {
        private readonly verkiezingsdb database = new verkiezingsdb();
        public verkiezingspagina()
        {
            InitializeComponent();
            laadgrid();
        }
        public void laadgrid()
        {
            var verkiezingen = database.Getverkiezingen();
            verkiezingsveld.ItemsSource = verkiezingen;
            startdatum.SelectedDate = null;
            einddatum.SelectedDate = null;
            titelbox.Clear();
            beschrijvingbox.Clear();
        }

        private void dashbordknop_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();

        }

        private void toevoegbtn_Click(object sender, RoutedEventArgs e)
        {
            if(startdatum.SelectedDate.HasValue && einddatum.SelectedDate.HasValue &&
                startdatum.SelectedDate.Value > einddatum.SelectedDate.Value)
            {
                MessageBox.Show("De startdatum mag niet later zijn dan de einddatum.");
                return;
            }

            if (string.IsNullOrWhiteSpace(titelbox.Text) || string.IsNullOrWhiteSpace(beschrijvingbox.Text) ||
                startdatum.SelectedDate == null || einddatum.SelectedDate == null)
            {
                MessageBox.Show("Vul alle velden in.");
                return;
            }
            database.Voegverkiezingtoe(titelbox.Text, beschrijvingbox.Text,
                DateOnly.FromDateTime(startdatum.SelectedDate.Value), DateOnly.FromDateTime(einddatum.SelectedDate.Value));
            laadgrid();
        }

        private void wijzigbtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (verkiezingsveld.SelectedItem is verkiezing geselecteerdeVerkiezing)
            {
                var wijzigWindow = new wijzigwindow("verkiezingspagina", geselecteerdeVerkiezing.VerkiezingID);
                wijzigWindow.ShowDialog();
                laadgrid();
            }
            else
            {
                MessageBox.Show("Selecteer eerst een verkiezing om te wijzigen.");
            }
        }

        private void verwijderbtn_Click(object sender, RoutedEventArgs e)
        {
            if(verkiezingsveld.SelectedItem is verkiezing geslecteerdeverkiezing)
            {
                database.Verwijder(geslecteerdeverkiezing.VerkiezingID);
                laadgrid();
            }
        }

        private void zoekbtn_Click(object sender, RoutedEventArgs e)
        {
            string titel = titelbox.Text;
            string beschrijving = beschrijvingbox.Text;

            DateOnly? start = startdatum.SelectedDate.HasValue
                ? DateOnly.FromDateTime(startdatum.SelectedDate.Value)
                : null;

            DateOnly? eind = einddatum.SelectedDate.HasValue
                ? DateOnly.FromDateTime(einddatum.SelectedDate.Value)
                : null;

            var resultaten = database.Zoekverkiezing(titel, beschrijving, start, eind);
            verkiezingsveld.ItemsSource = resultaten;
        }

    }
}
