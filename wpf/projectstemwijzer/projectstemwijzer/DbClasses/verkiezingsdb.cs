using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static projectstemwijzer.DbClasses.verkiezingsdb;
using static projectstemwijzer.standpuntenpagina;

namespace projectstemwijzer.DbClasses
{
    public class verkiezingsdb
    {
        public class verkiezing
        {
            public int VerkiezingID { get; set; }
            public string Titel { get; set; }
            public string Beschrijving { get; set; }
            public DateOnly Start_datum { get; set; }
            public DateOnly Eind_datum { get; set; }
            public DateTime Aanmaakdatum { get; set; }
        }
        private readonly string connectionString = "Server=localhost;Port=3309;Database=stemwijzer;Uid=root;Pwd=;Allow Zero Datetime=True;Convert Zero Datetime=True;";

        public void UpdateVerkiezing(int id, string titel, string beschrijving, DateOnly start, DateOnly eind)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE verkiezingen SET titel = @titel, beschrijving = @beschrijving, start_datum = @start, eind_datum = @eind WHERE verkiezingID = @id";
            cmd.Parameters.AddWithValue("@titel", titel);
            cmd.Parameters.AddWithValue("@beschrijving", beschrijving);
            cmd.Parameters.AddWithValue("@start", start.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@eind", eind.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // voor het ophalen van verkiezingen
        public List<verkiezing> Getverkiezingen()
        {
            var verkiezingen = new List<verkiezing>();
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return verkiezingen;
            }

            string query = "select verkiezingID,titel,beschrijving,start_datum,eind_datum,aanmaakdatum from verkiezingen";

            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DateTime startDatumDb, eindDatumDb, aanmaakDatumDb;

                try
                {
                    startDatumDb = reader.IsDBNull(reader.GetOrdinal("start_datum"))
                        ? DateTime.MinValue : reader.GetDateTime("start_datum");
                    eindDatumDb = reader.IsDBNull(reader.GetOrdinal("eind_datum"))
                        ? DateTime.MinValue : reader.GetDateTime("eind_datum");
                    aanmaakDatumDb = reader.IsDBNull(reader.GetOrdinal("aanmaakdatum"))
                        ? DateTime.MinValue : reader.GetDateTime("aanmaakdatum");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fout bij record ID " + reader.GetInt32("verkiezingID") + ": " + ex.Message);
                    continue;
                }

                verkiezingen.Add(new verkiezing
                {
                    VerkiezingID = reader.GetInt32("verkiezingID"),
                    Titel = reader.GetString("titel"),
                    Beschrijving = reader.GetString("beschrijving"),
                    Start_datum = DateOnly.FromDateTime(startDatumDb),
                    Eind_datum = DateOnly.FromDateTime(eindDatumDb),
                    Aanmaakdatum = aanmaakDatumDb
                });
            }


            return verkiezingen;
        }
        // voor het toe voegen van een verkiezing 

        public void Voegverkiezingtoe(string titel, string beschrijving, DateOnly startDatum, DateOnly eindDatum)
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

            string query = "INSERT INTO verkiezingen (titel, beschrijving, start_datum, eind_datum, aanmaakdatum) VALUES (@titel, @beschrijving, @start_datum, @eind_datum, @aanmaakdatum)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@titel", titel);
            cmd.Parameters.AddWithValue("@beschrijving", beschrijving);
            cmd.Parameters.AddWithValue("@start_datum", startDatum.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@eind_datum", eindDatum.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@aanmaakdatum", DateTime.Now);

            cmd.ExecuteNonQuery();
        }
        // voor het verwijderen van een verkiezing
        public void Verwijder(int verkiezingid)
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

            string query = "DELETE FROM verkiezingen WHERE verkiezingID = @verkiezingID";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@verkiezingID", verkiezingid);
            cmd.ExecuteNonQuery();
        }
        // voor het zoeken van een verkiezing

        public List<verkiezing> Zoekverkiezing(string titel, string beschrijving, DateOnly? startDatum, DateOnly? eindDatum)
        {
            var verkiezingen = new List<verkiezing>();
            using var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                var voorwaarden = new List<string>();
                var parameters = new List<MySqlParameter>();

                if (!string.IsNullOrWhiteSpace(titel))
                {
                    voorwaarden.Add("titel LIKE @p_titel");
                    parameters.Add(new MySqlParameter("@p_titel", $"%{titel}%"));
                }
                if (!string.IsNullOrWhiteSpace(beschrijving))
                {
                    voorwaarden.Add("beschrijving LIKE @p_beschrijving");
                    parameters.Add(new MySqlParameter("@p_beschrijving", $"%{beschrijving}%"));
                }
                if (startDatum.HasValue)
                {
                    voorwaarden.Add("DATE(start_datum) = @p_startDatum");
                    parameters.Add(new MySqlParameter("@p_startDatum", startDatum.Value.ToDateTime(TimeOnly.MinValue)));
                }
                if (eindDatum.HasValue)
                {
                    voorwaarden.Add("DATE(eind_datum) = @p_eindDatum");
                    parameters.Add(new MySqlParameter("@p_eindDatum", eindDatum.Value.ToDateTime(TimeOnly.MinValue)));
                }

                string whereClause = voorwaarden.Count > 0 ? "WHERE " + string.Join(" AND ", voorwaarden) : "";

                string query = $@"
            SELECT verkiezingID, titel, beschrijving, start_datum, eind_datum, aanmaakdatum
            FROM verkiezingen
            {whereClause}
        ";

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddRange(parameters.ToArray());

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    verkiezingen.Add(new verkiezing
                    {
                        VerkiezingID = reader.GetInt32("verkiezingID"),
                        Titel = reader.GetString("titel"),
                        Beschrijving = reader.GetString("beschrijving"),
                        Start_datum = DateOnly.FromDateTime(reader.GetDateTime("start_datum")),
                        Eind_datum = DateOnly.FromDateTime(reader.GetDateTime("eind_datum")),
                        Aanmaakdatum = reader.GetDateTime("aanmaakdatum")
                    });
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Fout bij het zoeken van verkiezingen: " + ex.Message);
            }

            return verkiezingen;
        }
    }
}
