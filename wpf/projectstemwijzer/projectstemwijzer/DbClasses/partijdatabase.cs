using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;

namespace projectstemwijzer.DbClasses
{
    public class Partij
    {
        public int PartijId { get; set; }
        public string Naam { get; set; }
        public string Info { get; set; }
        public byte[] Foto { get; set; }
    }

    public class PartijDatabase
    {
        private readonly string connectionString = "Server=localhost;Port=3309;Database=stemwijzer;Uid=root;Pwd=;";

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
            string query = $"SELECT partijID, partijnaam, partijinfo, foto FROM partijen WHERE {whereClause}";

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
                    Info = reader.GetString("partijinfo"),
                    Foto = reader.IsDBNull(reader.GetOrdinal("foto")) ? null : (byte[])reader["foto"]
                });
            }

            return partijen;
        }

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

        public void Verwijder(int partijId)
        {
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                string deleteStandpunten = "DELETE FROM standpunten WHERE partijID = @partijId";
                using var cmd1 = new MySqlCommand(deleteStandpunten, connection);
                cmd1.Parameters.AddWithValue("@partijId", partijId);
                cmd1.ExecuteNonQuery();

                string deletePartij = "DELETE FROM partijen WHERE partijID = @partijId";
                using var cmd2 = new MySqlCommand(deletePartij, connection);
                cmd2.Parameters.AddWithValue("@partijId", partijId);
                cmd2.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        public void WijzigPartij(int partijId, string nieuweNaam, string nieuweInfo, byte[] nieuweFoto = null)
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

            string query;
            if (nieuweFoto != null)
            {
                query = "UPDATE partijen SET partijnaam = @naam, partijinfo = @info, foto = @foto WHERE partijID = @partijId";
            }
            else
            {
                query = "UPDATE partijen SET partijnaam = @naam, partijinfo = @info WHERE partijID = @partijId";
            }

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@naam", nieuweNaam);
            cmd.Parameters.AddWithValue("@info", nieuweInfo);
            cmd.Parameters.AddWithValue("@partijId", partijId);
            if (nieuweFoto != null)
            {
                cmd.Parameters.AddWithValue("@foto", nieuweFoto);
            }
            cmd.ExecuteNonQuery();
        }

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

            string query = "SELECT partijID, partijnaam, partijinfo, foto FROM partijen";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                partijen.Add(new Partij
                {
                    PartijId = reader.GetInt32("partijID"),
                    Naam = reader.GetString("partijnaam"),
                    Info = reader.GetString("partijinfo"),
                    Foto = reader.IsDBNull(reader.GetOrdinal("foto")) ? null : (byte[])reader["foto"]
                });
            }

            return partijen;
        }

        public void VoegOfWijzigFoto(int partijId, byte[] foto)
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

            string query = "UPDATE partijen SET foto = @foto WHERE partijID = @partijId";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@foto", foto);
            cmd.Parameters.AddWithValue("@partijId", partijId);
            cmd.ExecuteNonQuery();
        }
    }
}
