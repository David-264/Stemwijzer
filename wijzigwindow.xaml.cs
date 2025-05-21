using projectstemwijzer.DbClasses;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace projectstemwijzer
{
    public partial class wijzigwindow : Window
    {
        private string windowsoort;
        private int index;
        private readonly PartijDatabase database = new PartijDatabase();
        private readonly Partij huidigePartij;

        private TextBox partijNaamTextBox;
        private TextBox partijInfoTextBox;
        private Button wijzigButton;

        public wijzigwindow(string window, int wijzigindex)
        {
            InitializeComponent();

            index = wijzigindex;
            windowsoort = window;
            huidigePartij = database.GetPartijen().FirstOrDefault(p => p.PartijId == wijzigindex);

            if (huidigePartij == null)
            {
                MessageBox.Show("Kan partij niet vinden.");
                Close();
                return;
            }

            start();

            partijNaamTextBox.Text = huidigePartij.Naam;
            partijInfoTextBox.Text = huidigePartij.Info;
        }

        private void wijzigbtn_Click(object sender, RoutedEventArgs e)
        {
            string nieuweNaam = partijNaamTextBox.Text;
            string nieuweInfo = partijInfoTextBox.Text;

            if (!string.IsNullOrWhiteSpace(nieuweNaam) && !string.IsNullOrWhiteSpace(nieuweInfo))
            {
                database.WijzigPartij(huidigePartij.PartijId, nieuweNaam, nieuweInfo);
                Close();
            }
            else
            {
                MessageBox.Show("Vul zowel naam als info in.");
            }
        }

        private void start()
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
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.8, GridUnitType.Star) });

                var label1 = new Label
                {
                    Content = "partijnaam",
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3"))
                };
                Grid.SetRow(label1, 0);
                Grid.SetColumn(label1, 1);
                grid.Children.Add(label1);

                partijNaamTextBox = new TextBox
                {
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3"))
                };
                Grid.SetRow(partijNaamTextBox, 1);
                Grid.SetColumn(partijNaamTextBox, 1);
                grid.Children.Add(partijNaamTextBox);

                var label2 = new Label
                {
                    Content = "info",
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3"))
                };
                Grid.SetRow(label2, 2);
                Grid.SetColumn(label2, 1);
                grid.Children.Add(label2);

                partijInfoTextBox = new TextBox
                {
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3"))
                };
                Grid.SetRow(partijInfoTextBox, 3);
                Grid.SetColumn(partijInfoTextBox, 1);
                grid.Children.Add(partijInfoTextBox);

                wijzigButton = new Button
                {
                    Content = "wijzig",
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAE6E3"))
                };
                wijzigButton.Click += wijzigbtn_Click;

                Grid.SetRow(wijzigButton, 5);
                Grid.SetColumn(wijzigButton, 1);
                grid.Children.Add(wijzigButton);

                this.Content = grid;
            }
            else
            {
                MessageBox.Show("Er is iets fout gegaan, open het scherm opnieuw.");
            }
        }
    }
}
