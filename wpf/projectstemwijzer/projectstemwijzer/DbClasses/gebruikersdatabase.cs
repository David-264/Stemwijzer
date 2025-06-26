using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace projectstemwijzer.DbClasses
{
    public class gebruikersdatabase
    {
        public class Gebruiker
        {
            public int GebruikerID { get; set; }
            public string Email { get; set; }
            public string Gebruikersnaam { get; set; }
            public string Wachtwoord_hash { get; set; }
            public int RolID { get; set; }
            public string RolNaam { get; set; }
        }

        private readonly string connectionString = "Server=localhost;Port=3309;Database=stemwijzer;Uid=root;Pwd=;";

        public List<Gebruiker> ZoekUsers(string email, string gebruikersnaam, string wachtwoord, string rol)
        {
            var gebruikers = new List<Gebruiker>();
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return gebruikers;
            }

            var voorwaarden = new List<string>();
            if (!string.IsNullOrWhiteSpace(email))
                voorwaarden.Add("g.email LIKE @email");
            if (!string.IsNullOrWhiteSpace(gebruikersnaam))
                voorwaarden.Add("g.gebruikersnaam LIKE @gebruikersnaam");
            if (!string.IsNullOrWhiteSpace(wachtwoord))
                voorwaarden.Add("g.wachtwoord_hash LIKE @wachtwoord");
            if (!string.IsNullOrWhiteSpace(rol))
                voorwaarden.Add("r.rolNaam LIKE @rol");

            if (voorwaarden.Count == 0)
            {
                MessageBox.Show("Geen zoekcriteria opgegeven.");
                return gebruikers;
            }

            string whereClause = string.Join(" OR ", voorwaarden);
            string query = $@"
                SELECT g.gebruikerID, g.email, g.gebruikersnaam, g.wachtwoord_hash, g.rolID, r.rolNaam
                FROM gebruikers g
                INNER JOIN rol r ON g.rolID = r.rolID
                WHERE {whereClause}
            ";

            using var cmd = new MySqlCommand(query, connection);
            if (!string.IsNullOrWhiteSpace(email))
                cmd.Parameters.AddWithValue("@email", $"%{email}%");
            if (!string.IsNullOrWhiteSpace(gebruikersnaam))
                cmd.Parameters.AddWithValue("@gebruikersnaam", $"%{gebruikersnaam}%");
            if (!string.IsNullOrWhiteSpace(wachtwoord))
                cmd.Parameters.AddWithValue("@wachtwoord", $"%{wachtwoord}%");
            if (!string.IsNullOrWhiteSpace(rol))
                cmd.Parameters.AddWithValue("@rol", $"%{rol}%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                gebruikers.Add(new Gebruiker
                {
                    GebruikerID = reader.IsDBNull(reader.GetOrdinal("gebruikerID")) ? 0 : reader.GetInt32("gebruikerID"),
                    Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email"),
                    Gebruikersnaam = reader.IsDBNull(reader.GetOrdinal("gebruikersnaam")) ? "" : reader.GetString("gebruikersnaam"),
                    Wachtwoord_hash = reader.IsDBNull(reader.GetOrdinal("wachtwoord_hash")) ? "" : reader.GetString("wachtwoord_hash"),
                    RolID = reader.IsDBNull(reader.GetOrdinal("rolID")) ? 0 : reader.GetInt32("rolID"),
                    RolNaam = reader.IsDBNull(reader.GetOrdinal("rolNaam")) ? "" : reader.GetString("rolNaam")
                });
            }

            return gebruikers;
        }

        public List<Gebruiker> GetAlleGebruikersMetRol()
        {
            var gebruikers = new List<Gebruiker>();
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return gebruikers;
            }

            string query = @"
    SELECT g.gebruikerID, g.email, g.gebruikersnaam, g.wachtwoord_hash, g.rolID, r.rolNaam
    FROM gebruikers g
    INNER JOIN rol r ON g.rolID = r.rolID
    WHERE g.gebruikerID NOT IN (1, 2)
";


            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                gebruikers.Add(new Gebruiker
                {
                    GebruikerID = reader.IsDBNull(reader.GetOrdinal("gebruikerID")) ? 0 : reader.GetInt32("gebruikerID"),
                    Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email"),
                    Gebruikersnaam = reader.IsDBNull(reader.GetOrdinal("gebruikersnaam")) ? "" : reader.GetString("gebruikersnaam"),
                    Wachtwoord_hash = reader.IsDBNull(reader.GetOrdinal("wachtwoord_hash")) ? "" : reader.GetString("wachtwoord_hash"),
                    RolID = reader.IsDBNull(reader.GetOrdinal("rolID")) ? 0 : reader.GetInt32("rolID"),
                    RolNaam = reader.IsDBNull(reader.GetOrdinal("rolNaam")) ? "" : reader.GetString("rolNaam")
                });
            }

            return gebruikers;
        }

        public List<Gebruiker> getgebruikers()
        {
            var gebruikers = new List<Gebruiker>();
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return gebruikers;
            }

            string query = @"
                SELECT g.gebruikerID, g.email, g.gebruikersnaam, g.wachtwoord_hash, g.rolID, r.rolNaam
                FROM gebruikers g
                INNER JOIN rol r ON g.rolID = r.rolID
                WHERE g.rolID = 1
            ";

            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                gebruikers.Add(new Gebruiker
                {
                    GebruikerID = reader.IsDBNull(reader.GetOrdinal("gebruikerID")) ? 0 : reader.GetInt32("gebruikerID"),
                    Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email"),
                    Gebruikersnaam = reader.IsDBNull(reader.GetOrdinal("gebruikersnaam")) ? "" : reader.GetString("gebruikersnaam"),
                    Wachtwoord_hash = reader.IsDBNull(reader.GetOrdinal("wachtwoord_hash")) ? "" : reader.GetString("wachtwoord_hash"),
                    RolID = reader.IsDBNull(reader.GetOrdinal("rolID")) ? 0 : reader.GetInt32("rolID"),
                    RolNaam = reader.IsDBNull(reader.GetOrdinal("rolNaam")) ? "" : reader.GetString("rolNaam")
                });
            }

            return gebruikers;
        }

        public List<Gebruiker> getbeheerders()
        {
            var gebruikers = new List<Gebruiker>();
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return gebruikers;
            }

            string query = @"
                SELECT g.gebruikerID, g.email, g.gebruikersnaam, g.wachtwoord_hash, g.rolID, r.rolNaam
                FROM gebruikers g
                INNER JOIN rol r ON g.rolID = r.rolID
                WHERE g.rolID = 2
            ";

            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                gebruikers.Add(new Gebruiker
                {
                    GebruikerID = reader.IsDBNull(reader.GetOrdinal("gebruikerID")) ? 0 : reader.GetInt32("gebruikerID"),
                    Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email"),
                    Gebruikersnaam = reader.IsDBNull(reader.GetOrdinal("gebruikersnaam")) ? "" : reader.GetString("gebruikersnaam"),
                    Wachtwoord_hash = reader.IsDBNull(reader.GetOrdinal("wachtwoord_hash")) ? "" : reader.GetString("wachtwoord_hash"),
                    RolID = reader.IsDBNull(reader.GetOrdinal("rolID")) ? 0 : reader.GetInt32("rolID"),
                    RolNaam = reader.IsDBNull(reader.GetOrdinal("rolNaam")) ? "" : reader.GetString("rolNaam")
                });
            }

            return gebruikers;
        }
        public void AddAccount(string email, string gebruikersnaam, string wachtwoord_hash, int rolID)
        {
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM rol WHERE rolID = @RolID";
                using var checkCmd = new MySqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@RolID", rolID);
                var count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count == 0)
                {
                    MessageBox.Show("Ongeldige rolID: deze rol bestaat niet.");
                    return;
                }

                string query = @"
            INSERT INTO gebruikers (email, gebruikersnaam, wachtwoord_hash, rolID)
            VALUES (@Email, @Gebruikersnaam, @Wachtwoord_hash, @RolID)
        ";

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
                cmd.Parameters.AddWithValue("@Wachtwoord_hash", wachtwoord_hash);
                cmd.Parameters.AddWithValue("@RolID", rolID);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        public void UpdateAccount(int gebruikerID, string email, string gebruikersnaam, string wachtwoord_hash, int rolID)
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

            string query = @"
                UPDATE gebruikers
                SET email = @Email,
                    gebruikersnaam = @Gebruikersnaam,
                    wachtwoord_hash = @Wachtwoord_hash,
                    rolID = @RolID
                WHERE gebruikerID = @GebruikerID
            ";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
            cmd.Parameters.AddWithValue("@Wachtwoord_hash", wachtwoord_hash);
            cmd.Parameters.AddWithValue("@RolID", rolID);
            cmd.Parameters.AddWithValue("@GebruikerID", gebruikerID);

            cmd.ExecuteNonQuery();
        }
        public void VerwijderAccount(int gebruikerID)
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

            string query = "DELETE FROM gebruikers WHERE gebruikerID = @GebruikerID";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@GebruikerID", gebruikerID);

            cmd.ExecuteNonQuery();
        }

    }
}   
