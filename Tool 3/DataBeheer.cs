using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Tool_3
{
    class DataBeheer
    {


        private string connectionString;
        public DataBeheer(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        public void GetStraatnaamByGemeente(string gemeentenaam)
        {
            Console.WriteLine("Alle straaten van het gemeente " + gemeentenaam);
            string query = "SELECT s.StraatNaam, g.GemeenteNaam " +
                "FROM Straat s, Gemeente g, Gemeente_Straat z " +
                "where g.GemeenteNaam = '" + gemeentenaam + "' AND g.IDGemeente = z.IDGemeente AND s.IDStraat = z.IDStraat " +
                "ORDER BY s.StraatNaam ASC";

            SqlConnection connection = getConnection();
            using (getConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine(reader[0].ToString().Trim() + " - " + reader[1]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        public void GetStraatEnGemeenteByID(int id)
        {
            string query = "SELECT s.StraatNaam, g.GemeenteNaam " +
                "FROM Straat s, Gemeente g, Gemeente_Straat z " +
                "where s.IDStraat = '" + id + "' AND g.IDGemeente = z.IDGemeente AND s.IDStraat = z.IDStraat";

            SqlConnection connection = getConnection();
            using (getConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"De straat met id {id} behoort tot straat: {reader[0].ToString().Trim()} in gemeente: {reader[1]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        public void GetStraatByID(int id)
        {
            string query = "SELECT StraatNaam" +
                " FROM Straat " +
                "where IDStraat = '"+id+"'";

            SqlConnection connection = getConnection();
            using (getConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("De ID: "+ id + " behoort tot straat: " + reader[0]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        public void GetGemeenteNaamID(string gemeentenaam)
        {
            Console.WriteLine("Alle straat id's van het gemeente " + gemeentenaam);
            string query = "SELECT s.IDStraat, g.GemeenteNaam " +
                "FROM Straat s, Gemeente g, Gemeente_Straat z " +
                "where g.GemeenteNaam = '" + gemeentenaam + "' AND g.IDGemeente = z.IDGemeente AND s.IDStraat = z.IDStraat";

            SqlConnection connection = getConnection();
            using (getConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine(reader[0] + " - " +reader[1]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
                
            }
        }
        public void GetAlleProvs()
        {
            string query = "SELECT * FROM Provincie";

            SqlConnection connection = getConnection();
            using (getConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader[0] + " - " + reader[1]);
                }
            }
        }
    }
}
