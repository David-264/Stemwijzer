using MySql.Data.MySqlClient;
using projectstemwijzer.DbClasses;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static projectstemwijzer.DbClasses.gebruikersdatabase;
using static projectstemwijzer.DbClasses.nieuwsdb;
using static projectstemwijzer.DbClasses.verkiezingsdb;

namespace projectstemwijzer
{
    public partial class wijzigwindow : Window
    {
        private string windowsoort;
        private int index;
        private readonly PartijDatabase database = new PartijDatabase();
        private readonly Standpuntdatabase standpuntDb = new Standpuntdatabase();
        private readonly nieuwsdb nieuwsDb = new nieuwsdb();
        private readonly verkiezingsdb verkiezingsDb = new verkiezingsdb();

        private readonly Partij huidigePartij;
        private readonly standpunt huidigStandpunt;
        private readonly nieuws huidigNieuws;
        private readonly verkiezing huidigeVerkiezing;

        private TextBox partijNaamTextBox;
        private TextBox partijInfoTextBox;
        private TextBox standpuntTextBox;
        private TextBox nieuwsTitelTextBox;
        private TextBox nieuwsInhoudTextBox;

        private TextBox titelTextBox;
        private TextBox beschrijvingTextBox;
        private DatePicker startDatumPicker;
        private DatePicker eindDatumPicker;

        private Button wijzigButton;
        private Button uploadFotoButton;

        private byte[] nieuweFotoBytes = null;

        public wijzigwindow(string window, int wijzigindex, Gebruiker gekozenGebruiker = null)
        {
            InitializeComponent();

            index = wijzigindex;
            windowsoort = window;

            if (windowsoort == "poletiekenpartijpagina")
            {
                huidigePartij = database.GetPartijen().FirstOrDefault(p => p.PartijId == wijzigindex);
                if (huidigePartij == null)
                {
                    MessageBox.Show("Kan partij niet vinden.");
                    Close();
                    return;
                }
            }
            else if (windowsoort == "standpuntpagina")
            {
                huidigStandpunt = standpuntDb.Getstandpunten().FirstOrDefault(s => s.StandpuntID == wijzigindex);
                if (huidigStandpunt == null)
                {
                    MessageBox.Show("Kan standpunt niet vinden.");
                    Close();
                    return;
                }
            }
            else if (windowsoort == "nieuwswindow")
            {
                huidigNieuws = nieuwsDb.Getnieuws().FirstOrDefault(n => n.nieuwsberichtID == wijzigindex);
                if (huidigNieuws == null)
                {
                    MessageBox.Show("Kan nieuwsbericht niet vinden.");
                    Close();
                    return;
                }
            }
            else if (windowsoort == "verkiezingspagina")
            {
                huidigeVerkiezing = verkiezingsDb.Getverkiezingen().FirstOrDefault(v => v.VerkiezingID == wijzigindex);
                if (huidigeVerkiezing == null)
                {
                    MessageBox.Show("Kan verkiezing niet vinden.");
                    Close();
                    return;
                }
            }

            start(gekozenGebruiker);

            if (windowsoort == "poletiekenpartijpagina")
            {
                partijNaamTextBox.Text = huidigePartij.Naam;
                partijInfoTextBox.Text = huidigePartij.Info;
            }
            else if (windowsoort == "standpuntpagina")
            {
                standpuntTextBox.Text = huidigStandpunt.Standpunt;
            }
            else if (windowsoort == "nieuwswindow")
            {
                nieuwsTitelTextBox.Text = huidigNieuws.titel;
                nieuwsInhoudTextBox.Text = huidigNieuws.inhoud;
            }
            else if (windowsoort == "verkiezingspagina")
            {
                titelTextBox.Text = huidigeVerkiezing.Titel;
                beschrijvingTextBox.Text = huidigeVerkiezing.Beschrijving;
                startDatumPicker.SelectedDate = DateTime.ParseExact(huidigeVerkiezing.Start_datum.ToString("yyyy-MM-dd"), "yyyy-MM-dd", null);
                eindDatumPicker.SelectedDate = DateTime.ParseExact(huidigeVerkiezing.Eind_datum.ToString("yyyy-MM-dd"), "yyyy-MM-dd", null);
            }
        }

        private void UploadFotoButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Afbeeldingen|*.jpg;*.jpeg;*.png;*.bmp";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filePath = dlg.FileName;
                nieuweFotoBytes = File.ReadAllBytes(filePath);
                MessageBox.Show("Foto is klaar om toegevoegd te worden bij wijzigen.");
            }
        }

        private void wijzigbtn_Click(object sender, RoutedEventArgs e)
        {
            if (windowsoort == "poletiekenpartijpagina")
            {
                string nieuweNaam = partijNaamTextBox.Text;
                string nieuweInfo = partijInfoTextBox.Text;

                if (!string.IsNullOrWhiteSpace(nieuweNaam) && !string.IsNullOrWhiteSpace(nieuweInfo))
                {
                    database.WijzigPartij(huidigePartij.PartijId, nieuweNaam, nieuweInfo, nieuweFotoBytes);
                    Close();
                }
                else
                {
                    MessageBox.Show("Vul zowel naam als info in.");
                }
            }
            else if (windowsoort == "nieuwswindow")
            {
                string nieuweTitel = nieuwsTitelTextBox.Text;
                string nieuweInhoud = nieuwsInhoudTextBox.Text;

                if (!string.IsNullOrWhiteSpace(nieuweTitel) && !string.IsNullOrWhiteSpace(nieuweInhoud))
                {
                    nieuwsDb.UpdateNieuws(huidigNieuws.nieuwsberichtID, nieuweTitel, nieuweInhoud);
                    Close();
                }
                else
                {
                    MessageBox.Show("Vul zowel titel als inhoud in.");
                }
            }
            else if (windowsoort == "verkiezingspagina")
            {
                if (startDatumPicker.SelectedDate.Value > eindDatumPicker.SelectedDate.Value)
                {
                    MessageBox.Show("De startdatum mag niet later zijn dan de einddatum.");
                    return;
                }
                string nieuweTitel = titelTextBox.Text;
                string nieuweBeschrijving = beschrijvingTextBox.Text;
                DateOnly? nieuweStart = startDatumPicker.SelectedDate.HasValue ? DateOnly.FromDateTime(startDatumPicker.SelectedDate.Value) : (DateOnly?)null;
                DateOnly? nieuweEind = eindDatumPicker.SelectedDate.HasValue ? DateOnly.FromDateTime(eindDatumPicker.SelectedDate.Value) : (DateOnly?)null;

                if (!string.IsNullOrWhiteSpace(nieuweTitel) && !string.IsNullOrWhiteSpace(nieuweBeschrijving)
                    && nieuweStart.HasValue && nieuweEind.HasValue)
                {
                    verkiezingsDb.UpdateVerkiezing(huidigeVerkiezing.VerkiezingID, nieuweTitel, nieuweBeschrijving, nieuweStart.Value, nieuweEind.Value);
                    Close();
                }
                else
                {
                    MessageBox.Show("Vul alle velden in.");
                }
            }
            else if (windowsoort == "standpuntpagina")
            {
                string nieuwStandpunt = standpuntTextBox.Text;

                if (!string.IsNullOrWhiteSpace(nieuwStandpunt))
                {
                    standpuntDb.UpdateStandpunt(huidigStandpunt.StandpuntID, nieuwStandpunt);
                    Close();
                }
                else
                {
                    MessageBox.Show("Vul het standpunt in.");
                }
            }

        }


        private void start(Gebruiker gekozenGebruiker = null)
        {
            if (windowsoort == "poletiekenpartijpagina")
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.8, GridUnitType.Star) });

                var label1 = new Label { Content = "partijnaam", Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(label1, 0);
                Grid.SetColumn(label1, 1);
                grid.Children.Add(label1);

                partijNaamTextBox = new TextBox { Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(partijNaamTextBox, 1);
                Grid.SetColumn(partijNaamTextBox, 1);
                grid.Children.Add(partijNaamTextBox);

                var label2 = new Label { Content = "info", Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(label2, 2);
                Grid.SetColumn(label2, 1);
                grid.Children.Add(label2);

                partijInfoTextBox = new TextBox { Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(partijInfoTextBox, 3);
                Grid.SetColumn(partijInfoTextBox, 1);
                grid.Children.Add(partijInfoTextBox);

                var label3 = new Label { Content = "foto uploaden", Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(label3, 4);
                Grid.SetColumn(label3, 1);
                grid.Children.Add(label3);

                uploadFotoButton = new Button { Content = "Upload Foto", Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")), Margin = new Thickness(0, 5, 0, 5) };
                uploadFotoButton.Click += UploadFotoButton_Click;
                Grid.SetRow(uploadFotoButton, 5);
                Grid.SetColumn(uploadFotoButton, 1);
                grid.Children.Add(uploadFotoButton);

                wijzigButton = new Button { Content = "wijzig", Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                wijzigButton.Click += wijzigbtn_Click;
                Grid.SetRow(wijzigButton, 6);
                Grid.SetColumn(wijzigButton, 1);
                grid.Children.Add(wijzigButton);

                this.Content = grid;
            }
            else if (windowsoort == "standpuntpagina")
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });

                var label = new Label { Content = "standpunt", Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(label, 0);
                Grid.SetColumn(label, 1);
                grid.Children.Add(label);

                standpuntTextBox = new TextBox { Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(standpuntTextBox, 1);
                Grid.SetColumn(standpuntTextBox, 1);
                grid.Children.Add(standpuntTextBox);

                wijzigButton = new Button { Content = "wijzig", Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                wijzigButton.Click += wijzigbtn_Click;
                Grid.SetRow(wijzigButton, 2);
                Grid.SetColumn(wijzigButton, 1);
                grid.Children.Add(wijzigButton);

                this.Content = grid;
            }
            else if (windowsoort == "nieuwswindow")
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.8, GridUnitType.Star) });

                var labelTitel = new Label { Content = "Titel", Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(labelTitel, 0);
                Grid.SetColumn(labelTitel, 1);
                grid.Children.Add(labelTitel);

                nieuwsTitelTextBox = new TextBox { Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(nieuwsTitelTextBox, 1);
                Grid.SetColumn(nieuwsTitelTextBox, 1);
                grid.Children.Add(nieuwsTitelTextBox);

                var labelInhoud = new Label { Content = "Inhoud", Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(labelInhoud, 2);
                Grid.SetColumn(labelInhoud, 1);
                grid.Children.Add(labelInhoud);

                nieuwsInhoudTextBox = new TextBox { Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")), AcceptsReturn = true, TextWrapping = TextWrapping.Wrap, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
                Grid.SetRow(nieuwsInhoudTextBox, 3);
                Grid.SetColumn(nieuwsInhoudTextBox, 1);
                Grid.SetRowSpan(nieuwsInhoudTextBox, 2);
                grid.Children.Add(nieuwsInhoudTextBox);

                wijzigButton = new Button { Content = "wijzig", Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                wijzigButton.Click += wijzigbtn_Click;
                Grid.SetRow(wijzigButton, 5);
                Grid.SetColumn(wijzigButton, 1);
                grid.Children.Add(wijzigButton);

                this.Content = grid;
            }
            else if (windowsoort == "verkiezingspagina")
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.8, GridUnitType.Star) });

                var labelTitel = new Label { Content = "Titel", Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(labelTitel, 0);
                Grid.SetColumn(labelTitel, 1);
                grid.Children.Add(labelTitel);

                titelTextBox = new TextBox { Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(titelTextBox, 1);
                Grid.SetColumn(titelTextBox, 1);
                grid.Children.Add(titelTextBox);

                var labelBeschrijving = new Label { Content = "Beschrijving", Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(labelBeschrijving, 2);
                Grid.SetColumn(labelBeschrijving, 1);
                grid.Children.Add(labelBeschrijving);

                beschrijvingTextBox = new TextBox { Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(beschrijvingTextBox, 3);
                Grid.SetColumn(beschrijvingTextBox, 1);
                grid.Children.Add(beschrijvingTextBox);

                var labelStartDatum = new Label { Content = "Startdatum", Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(labelStartDatum, 4);
                Grid.SetColumn(labelStartDatum, 1);
                grid.Children.Add(labelStartDatum);

                startDatumPicker = new DatePicker();
                Grid.SetRow(startDatumPicker, 5);
                Grid.SetColumn(startDatumPicker, 1);
                grid.Children.Add(startDatumPicker);

                var labelEindDatum = new Label { Content = "Einddatum", Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                Grid.SetRow(labelEindDatum, 6);
                Grid.SetColumn(labelEindDatum, 1);
                grid.Children.Add(labelEindDatum);

                eindDatumPicker = new DatePicker();
                Grid.SetRow(eindDatumPicker, 7);
                Grid.SetColumn(eindDatumPicker, 1);
                grid.Children.Add(eindDatumPicker);

                wijzigButton = new Button { Content = "wijzig", Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3")) };
                wijzigButton.Click += wijzigbtn_Click;
                Grid.SetRow(wijzigButton, 8);
                Grid.SetColumn(wijzigButton, 1);
                grid.Children.Add(wijzigButton);

                this.Content = grid;
            }
        }
    }
}
