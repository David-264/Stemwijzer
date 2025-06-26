using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projectstemwijzer.DbClasses
{
    
    public class nieuwsdb
    {
        public class nieuws
        {
            public int nieuwsberichtID { get; set; }
            public string titel { get; set; }
            public string inhoud { get; set; }
            public DateTime publicatiedatum { get; set; }
        }
        private readonly string connectionString = "Server=localhost;Port=3309;Database=stemwijzer;Uid=root;Pwd=;";
        public List<nieuws> Getnieuws()
        {
            var nieuwsdingen = new List<nieuws>();
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return nieuwsdingen;
            }

            string query = "select nieuwsberichtID, titel, inhoud, publicatiedatum from nieuwsberichten";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var nieuwsberichtID = reader.IsDBNull(reader.GetOrdinal("nieuwsberichtID")) ? 0 : reader.GetInt32("nieuwsberichtID");
                var titel = reader.IsDBNull(reader.GetOrdinal("titel")) ? "" : reader.GetString("titel");
                var inhoud = reader.IsDBNull(reader.GetOrdinal("inhoud")) ? "" : reader.GetString("inhoud");
                var publicatiedatum = reader.IsDBNull(reader.GetOrdinal("publicatiedatum")) ? DateTime.MinValue : reader.GetDateTime("publicatiedatum");

                nieuwsdingen.Add(new nieuws
                {
                    nieuwsberichtID = nieuwsberichtID,
                    titel = titel,
                    inhoud = inhoud,
                    publicatiedatum = publicatiedatum
                });
            }
            return nieuwsdingen;
        }
        public void VoegNieuwsToe(string titel, string inhoud)
        {
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }

            string query = "INSERT INTO nieuwsberichten (titel, inhoud, publicatiedatum) VALUES (@titel, @inhoud, @publicatiedatum)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@titel", titel);
            cmd.Parameters.AddWithValue("@inhoud", inhoud);
            cmd.Parameters.AddWithValue("@publicatiedatum", DateTime.Now);
            cmd.ExecuteNonQuery();
        }
        public void VerwijderNieuws(int nieuwsberichtID)
        {
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }

            string query = "DELETE FROM nieuwsberichten WHERE nieuwsberichtID = @nieuwsberichtID";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@nieuwsberichtID", nieuwsberichtID);
            cmd.ExecuteNonQuery();
        }

        // voor het zoeken van nieuws
        public List<nieuws> ZoekNieuws(string titel, string inhoud)
        {
            var resultaten = new List<nieuws>();
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return resultaten;
            }

            var voorwaarden = new List<string>();
            if (!string.IsNullOrWhiteSpace(titel))
                voorwaarden.Add("titel LIKE @titel");
            if (!string.IsNullOrWhiteSpace(inhoud))
                voorwaarden.Add("inhoud LIKE @inhoud");

            if (voorwaarden.Count == 0)
            {
                MessageBox.Show("Geen zoekcriteria opgegeven.");
                return resultaten;
            }

            string whereClause = string.Join(" OR ", voorwaarden);
            string query = $"SELECT nieuwsberichtID, titel, inhoud, publicatiedatum FROM nieuwsberichten WHERE {whereClause}";

            using var cmd = new MySqlCommand(query, connection);
            if (!string.IsNullOrWhiteSpace(titel))
                cmd.Parameters.AddWithValue("@titel", $"%{titel}%");
            if (!string.IsNullOrWhiteSpace(inhoud))
                cmd.Parameters.AddWithValue("@inhoud", $"%{inhoud}%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                resultaten.Add(new nieuws
                {
                    nieuwsberichtID = reader.GetInt32("nieuwsberichtID"),
                    titel = reader.GetString("titel"),
                    inhoud = reader.GetString("inhoud"),
                    publicatiedatum = reader.GetDateTime("publicatiedatum")
                });
            }

            return resultaten;
        }
        public void UpdateNieuws(int id, string titel, string inhoud)
        {
            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE nieuwsberichten SET titel = @titel, inhoud = @inhoud WHERE nieuwsberichtID = @id";
                cmd.Parameters.AddWithValue("@titel", titel);
                cmd.Parameters.AddWithValue("@inhoud", inhoud);
                cmd.Parameters.AddWithValue("@id", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("Update faalde: geen rijen aangepast. Controleer of het ID correct is.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Databasefout: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout: {ex.Message}");
                throw;
            }
        }

    }
}
