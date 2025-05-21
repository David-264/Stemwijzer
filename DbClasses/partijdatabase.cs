using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;

namespace projectstemwijzer.DbClasses
{
    public class PartijDatabase
    {
        private readonly string connectionString = "Server=localhost;Port=3309;Database=stemwijzer;Uid=root;Pwd=;";

        // voor het zoeken van een partij 
        public List<Partij> ZoekPartij(string naam, string info)
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

            var voorwaarden = new List<string>();
            if (!string.IsNullOrWhiteSpace(naam))
                voorwaarden.Add("partijnaam LIKE @naam");
            if (!string.IsNullOrWhiteSpace(info))
                voorwaarden.Add("partijinfo LIKE @info");

            if (voorwaarden.Count == 0)
            {
                MessageBox.Show("Geen resultaten omdat er niks ingevuld is");
                return partijen;
            }

            string whereClause = string.Join(" OR ", voorwaarden);
            string query = $"SELECT partijID, partijnaam, partijinfo FROM partijen WHERE {whereClause}";

            using var cmd = new MySqlCommand(query, connection);

            if (!string.IsNullOrWhiteSpace(naam))
                cmd.Parameters.AddWithValue("@naam", $"%{naam}%");
            if (!string.IsNullOrWhiteSpace(info))
                cmd.Parameters.AddWithValue("@info", $"%{info}%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                partijen.Add(new Partij
                {
                    PartijId = reader.GetInt32("partijID"),
                    Naam = reader.GetString("partijnaam"),
                    Info = reader.GetString("partijinfo")
                });
            }

            return partijen;
        }
        // voor het toe voegen van een partij
        public void VoegPartijToe(string naam, string info)
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

            string query = "INSERT INTO partijen (partijnaam, partijinfo) VALUES (@naam, @info)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@naam", naam);
            cmd.Parameters.AddWithValue("@info", info);
            cmd.ExecuteNonQuery();
        }
        // voor het verwijderen van een partij
        public void Verwijder(int partijId)
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

            string query = "DELETE FROM partijen WHERE partijID = @partijId";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@partijId", partijId);
            cmd.ExecuteNonQuery();
        }
        // voor het wijzigen
        public void WijzigPartij(int partijId, string nieuweNaam, string nieuweInfo)
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

            string query = "UPDATE partijen SET partijnaam = @naam, partijinfo = @info WHERE partijID = @partijId";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@naam", nieuweNaam);
            cmd.Parameters.AddWithValue("@info", nieuweInfo);
            cmd.Parameters.AddWithValue("@partijId", partijId);
            cmd.ExecuteNonQuery();
        }


        // voor als het window geopent word dat alle dingen die in de database zitten opgehaald worden
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

            string query = "SELECT partijID, partijnaam, partijinfo FROM partijen";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                partijen.Add(new Partij
                {
                    PartijId = reader.GetInt32("partijID"),
                    Naam = reader.GetString("partijnaam"),
                    Info = reader.GetString("partijinfo")
                });
            }

            return partijen;
        }
    }


    public class Partij
    {
        public int PartijId { get; set; }
        public string Naam { get; set; }
        public string Info { get; set; }
    }
}
