using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Tool_2
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
        public void RelatieStraatSegment()
        {
            VerwijderGegevensTabel("Straat_Segment");
            Console.WriteLine("Relatie tussen alle straten en segmenten aanmaken...");
            SqlConnection connection = getConnection();
            string query = "INSERT INTO dbo.Straat_Segment (IDSegment,IDStraat) VALUES (@IDSegment, @IDStraat)";
            foreach (var segment in Verwerking.eigenCSV)
            {
                if (Verwerking.alleSegmenten.ContainsKey(segment.Key) && Verwerking.alleStraten.ContainsKey(segment.Value)){
                    //hier komt alles ayaaaaaaaa
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        try
                        {
                            command.Parameters.AddWithValue("@IDSegment", SqlDbType.Int).Value = segment.Key;
                            command.Parameters.AddWithValue("@IDStraat", SqlDbType.Int).Value = segment.Value;
                            command.CommandText = query;
                            command.ExecuteNonQuery();
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
            }
                        
                    
                
                

            
            Console.WriteLine("Relatie tussen alle gemeentes en straten aanmaken...OK");
        }
        public void RelatieGemeenteStraat()
        {
            VerwijderGegevensTabel("Gemeente_Straat");
            Console.WriteLine("Relatie tussen alle gemeentes en straten aanmaken...");
            SqlConnection connection = getConnection();
            string query = "INSERT INTO dbo.Gemeente_Straat (IDStraat,IDGemeente) VALUES (@IDStraat, @IDGemeente)";
            foreach (var element in Verwerking.gemeenteEnStraatGegevens)
            {
                if (Verwerking.alleGemeentes.ContainsKey(element.Value) && Verwerking.alleStraten.ContainsKey(element.Key))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        try
                        {
                            command.Parameters.AddWithValue("@IDStraat", SqlDbType.Int).Value = element.Key;
                            command.Parameters.AddWithValue("@IDGemeente", SqlDbType.Int).Value = element.Value;
                            command.CommandText = query;
                            command.ExecuteNonQuery();
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
            }
            Console.WriteLine("Relatie tussen alle gemeentes en straten aanmaken...OK");
        }
        public void RelatieProvincieGemeente()
        {
            VerwijderGegevensTabel("Provincie_Gemeente");
            Console.WriteLine("Relatie tussen alle provincies en gemeentes aanmaken...");
            SqlConnection connection = getConnection();
            string query = "INSERT INTO dbo.Provincie_Gemeente (IDGemeente,IDProvincie) VALUES (@IDGemeente, @IDProvincie)";
            foreach (var element in Verwerking.provincieInfo)
            {
                if (Verwerking.alleGemeentes.ContainsKey(element.Key) && Verwerking.alleProvincies.ContainsKey(int.Parse(element.Value[0])))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        try
                        {
                            command.Parameters.AddWithValue("@IDGemeente", SqlDbType.Int).Value = element.Key;
                            command.Parameters.AddWithValue("@IDProvincie", SqlDbType.Int).Value = int.Parse(element.Value[0]);
                            command.CommandText = query;
                            command.ExecuteNonQuery();
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
            }
            Console.WriteLine("Relatie tussen alle provincies en gemeentes aanmaken...OK");
        }
        public void VoegSegmentenToe()
        {
            VerwijderGegevensTabel("Segment");
            Console.WriteLine("Alle segmenten in databanks steken...");
            SqlConnection connection = getConnection();
            string query = "INSERT INTO dbo.Segment (IDSegment,BeginKnoop,EindKnoop) VALUES (@IDSegment, @BeginKnoop, @EindKnoop)";
            foreach (var element in Verwerking.alleSegmenten)
            {
                string alleBeginKnopen = "";
                string alleEindKnopen = "";
                foreach (var knoop in element.Value)
                {
                    alleBeginKnopen += knoop.BeginKnoop.Punt.X.ToString() + " " + knoop.BeginKnoop.Punt.Y.ToString() + ";";
                    alleEindKnopen += knoop.EindKnoop.Punt.X.ToString() + " " + knoop.EindKnoop.Punt.Y.ToString() + ";";
                }
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    try
                    {
                        command.Parameters.AddWithValue("@IDSegment", SqlDbType.Int).Value = element.Key;
                        command.Parameters.AddWithValue("@BeginKnoop", SqlDbType.NVarChar).Value = alleBeginKnopen;
                        command.Parameters.AddWithValue("@EindKnoop", SqlDbType.NVarChar).Value = alleEindKnopen;
                        command.CommandText = query;
                        command.ExecuteNonQuery();
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
            Console.WriteLine("Alle segmenten in databanks steken...OK");
        }
    
        public void VoegStratenToe()
        {
            VerwijderGegevensTabel("Straat");
            Console.WriteLine("Alle straten in databanks steken...");
            SqlConnection connection = getConnection();
            string query = "INSERT INTO dbo.Straat (IDStraat,StraatNaam) VALUES (@IDStraat, @StraatNaam)";
            foreach (var element in Verwerking.alleStraten)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    try
                    {
                        command.Parameters.AddWithValue("@IDStraat", SqlDbType.Int).Value = element.Key;
                        command.Parameters.AddWithValue("@StraatNaam", SqlDbType.NChar).Value = element.Value.StraatNaam;
                        command.CommandText = query;
                        command.ExecuteNonQuery();
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
            Console.WriteLine("Alle straten in databanks steken...OK");
        }
        
        public void VoegGemeentesToe()
        {
            VerwijderGegevensTabel("Gemeente");
            Console.WriteLine("Alle gemeentes in databanks steken...");
            SqlConnection connection = getConnection();
            string query = "INSERT INTO dbo.Gemeente (IDGemeente,GemeenteNaam) VALUES (@IDGemeente, @GemeenteNaam)";
            foreach (var element in Verwerking.alleGemeentes)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    try
                    {
                        command.Parameters.AddWithValue("@IDGemeente", SqlDbType.Int).Value = element.Key;
                        command.Parameters.AddWithValue("@GemeenteNaam", SqlDbType.NChar).Value = element.Value.GemeenteNaam;
                        command.CommandText = query;
                        command.ExecuteNonQuery();
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
            Console.WriteLine("Alle gemeentes in databanks steken...OK");
        }
        public void VoegProvincieToe()
        {
            VerwijderGegevensTabel("Provincie");
            Console.WriteLine("Alle provincies in databanks steken...");
            SqlConnection connection = getConnection();
            string query = "INSERT INTO dbo.Provincie (IDProvincie,ProvincieNaam) VALUES (@IDProvincie, @ProvincieNaam)";
            foreach (var element in Verwerking.alleProvincies)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    try
                    {
                        command.Parameters.AddWithValue("@IDProvincie", SqlDbType.Int).Value = element.Key;
                        command.Parameters.AddWithValue("@ProvincieNaam", SqlDbType.NChar).Value = element.Value.ProvincieNaam;
                        command.CommandText = query;
                        command.ExecuteNonQuery();
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
            Console.WriteLine("Alle provincies in databanks steken...OK");
        }
        public void VerwijderGegevensTabel(string tabelnaam)
        {
            Console.WriteLine("Verwijderen van gegevens uit " + tabelnaam + "...");
            SqlConnection connection = getConnection();
            string query = "DELETE FROM dbo." + tabelnaam;
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
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
            Console.WriteLine("Verwijderen van gegevens uit " + tabelnaam + "...OK");

        }
    }
}
