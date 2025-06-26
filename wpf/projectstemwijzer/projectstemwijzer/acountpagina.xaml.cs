using projectstemwijzer.DbClasses;
using System;
using System.Collections.Generic;
using System.IO;
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
using static projectstemwijzer.DbClasses.gebruikersdatabase;

namespace projectstemwijzer
{
    /// <summary>
    /// Interaction logic for acountpagina.xaml
    /// </summary>
    public partial class acountpagina : Window
    {
        private readonly gebruikersdatabase database = new gebruikersdatabase();
        public acountpagina()
        {
            InitializeComponent();
            LaadDataGrid();
        }
        
        private void LaadDataGrid()
        {
            rolbox.ItemsSource = database.GetAlleGebruikersMetRol().Select(g => g.RolNaam).Distinct().ToList();
            var acounts = database.GetAlleGebruikersMetRol();
            uitkomstveld.ItemsSource = acounts;
        }

        private void dashbordknop_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void toevoegbtn_Click(object sender, RoutedEventArgs e)
        {
            if (emailbox.Text == "" || gebruikersnaambox.Text == "" || wachtwoordbox.Password == "" || rolbox.SelectedIndex == -1)
            {
                MessageBox.Show("voer alle velden in");
                return;
            }
            database.AddAccount(emailbox.Text, gebruikersnaambox.Text, wachtwoordbox.Password, rolbox.SelectedIndex);
            emailbox.Text = "";
            gebruikersnaambox.Text = "";
            wachtwoordbox.Password = "";
            rolbox.SelectedIndex = -1;
            LaadDataGrid();
        }

        private void wijzigbtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (uitkomstveld.SelectedItem is Gebruiker gekozenGebruiker)
            {
                var windowNaam = "gebruikerspagina"; // of iets wat je bedoelt met string window
                int index = uitkomstveld.SelectedIndex;

                var wijzigWindow = new wijzigwindow(windowNaam, index, gekozenGebruiker);
                wijzigWindow.ShowDialog();

                // eventueel je DataGrid verversen
                LaadDataGrid();
            }
            else
            {
                MessageBox.Show("Selecteer eerst een gebruiker.");
            }
        }



        private void verwijderbtn_Click(object sender, RoutedEventArgs e)
        {
            if (uitkomstveld.SelectedItem is Gebruiker gekozenGebruiker)
            {
                database.VerwijderAccount(gekozenGebruiker.GebruikerID);
                LaadDataGrid();
            }
            else
            {
                MessageBox.Show("Selecteer een nieuwsbericht om te verwijderen.");
            }
        }

        private void zoekbtn_Click(object sender, RoutedEventArgs e)
        {
            var email = emailbox.Text ?? "";
            var gebruikersnaam = gebruikersnaambox.Text ?? "";
            var wachtwoord = wachtwoordbox.Password ?? "";
            var rol = rolbox.SelectedItem != null ? rolbox.SelectedItem.ToString() : "";

            var gebruikers = database.ZoekUsers(email, gebruikersnaam, wachtwoord, rol);
            uitkomstveld.ItemsSource = gebruikers;
        }

        private void gebruikersbutton_Click(object sender, RoutedEventArgs e)
        {
            var acounts = database.getgebruikers();
            uitkomstveld.ItemsSource = acounts;
        }

        private void beheerbutton_Click(object sender, RoutedEventArgs e)
        {
            var acounts = database.getbeheerders();
            uitkomstveld.ItemsSource = acounts;
        }

        private void resetfiltersbtn_Click(object sender, RoutedEventArgs e)
        {
            var acounts = database.GetAlleGebruikersMetRol();
            uitkomstveld.ItemsSource = acounts;
        }
    }
}
