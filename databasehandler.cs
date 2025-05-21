using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace projectstemwijzer
{
    class databasehandler
    {
        string connectionString = "Server=localhost;Port=3309;Database=stemwijzer;Uid=root;Pwd=;";
        private MySqlConnection connection;

        public databasehandler()
        {
            connection = new MySqlConnection(connectionString);
        }
        public void OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}
