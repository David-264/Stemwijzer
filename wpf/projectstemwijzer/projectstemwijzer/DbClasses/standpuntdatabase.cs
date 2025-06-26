using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace projectstemwijzer.DbClasses
{
    public class Standpuntdatabase
    {
        private readonly string connectionString = "Server=localhost;Port=3309;Database=stemwijzer;Uid=root;Pwd=;";

        public List<standpunt> Getstandpunten()
        {
            var standpunten = new List<standpunt>();
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return standpunten;
            }

            string query = @"SELECT s.standpuntID, s.standpunt, s.partijID, p.partijnaam 
                             FROM standpunten s 
                             JOIN partijen p ON s.partijID = p.partijID";

            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                standpunten.Add(new standpunt
                {
                    StandpuntID = reader.GetInt32("standpuntID"),
                    Standpunt = reader.GetString("standpunt"),
                    PartijId = reader.GetInt32("partijID"),
                    Partijnaam = reader.GetString("partijnaam")
                });
            }

            return standpunten;
        }

        public List<standpunt> ZoekStandpunt(string standpunttekst, string partijnaam)
        {
            var standpunten = new List<standpunt>();
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return standpunten;
            }

            var voorwaarden = new List<string>();
            var parameters = new List<MySqlParameter>();

            if (!string.IsNullOrWhiteSpace(standpunttekst))
            {
                voorwaarden.Add("s.standpunt LIKE @standpunt");
                parameters.Add(new MySqlParameter("@standpunt", $"%{standpunttekst}%"));
            }

            if (!string.IsNullOrWhiteSpace(partijnaam))
            {
                voorwaarden.Add("p.partijnaam LIKE @partij");
                parameters.Add(new MySqlParameter("@partij", $"%{partijnaam}%"));
            }

            string whereClause = voorwaarden.Count > 0 ? "WHERE " + string.Join(" AND ", voorwaarden) : "";

            string query = $@"
        SELECT s.standpuntID, s.standpunt, s.partijID, p.partijnaam 
        FROM standpunten s
        JOIN partijen p ON s.partijID = p.partijID
        {whereClause}";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters.ToArray());

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                standpunten.Add(new standpunt
                {
                    StandpuntID = reader.GetInt32("standpuntID"),
                    Standpunt = reader.GetString("standpunt"),
                    PartijId = reader.GetInt32("partijID"),
                    Partijnaam = reader.GetString("partijnaam")
                });
            }

            return standpunten;
        }




        public void Verwijder(int standpuntID)
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

            string query = "DELETE FROM standpunten WHERE standpuntID = @standpuntid";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@standpuntid", standpuntID);
            cmd.ExecuteNonQuery();
        }

        public void VoegPartijToe(string standpunt, int partijID)
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

            string query = "INSERT INTO standpunten (standpunt, partijID) VALUES (@standpunt, @partijID)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@standpunt", standpunt);
            cmd.Parameters.AddWithValue("@partijID", partijID);
            cmd.ExecuteNonQuery();
        }
        public void UpdateStandpunt(int standpuntID, string nieuweTekst)
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

            string query = "UPDATE standpunten SET standpunt = @standpunt WHERE standpuntID = @standpuntID";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@standpunt", nieuweTekst);
            cmd.Parameters.AddWithValue("@standpuntID", standpuntID);
            cmd.ExecuteNonQuery();
        }

    }

    public class standpunt
    {
        public int PartijId { get; set; }
        public string Partijnaam { get; set; }
        public string Standpunt { get; set; }
        public int StandpuntID { get; set; }
    }
}
