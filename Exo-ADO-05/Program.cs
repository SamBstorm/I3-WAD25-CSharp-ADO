using Microsoft.Data.SqlClient;
using System.Data;

namespace Exo_ADO_05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExoWAD-ADO;Integrated Security=True";

            Student moi = new Student() { 
                FirstName = "Samuel",
                LastName = "Legrain"
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [BirthDate], [YearResult], [SectionID], [Active] FROM [Student] WHERE [FirstName] = @fn AND [LastName] = @ln";
                    command.Parameters.AddWithValue("fn", moi.FirstName);
                    command.Parameters.AddWithValue("ln", moi.LastName);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                moi.Id = (int)reader[nameof(Student.Id)];
                                moi.BirthDate = (DateTime)reader[nameof(Student.BirthDate)];
                                moi.SectionID = (int)reader[nameof(Student.SectionID)];
                                moi.YearResult = (int)reader[nameof(Student.YearResult)];
                                moi.Active = (bool)reader[nameof(Student.Active)];
                            }
                        }
                    }
                    catch(SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                moi.SectionID = 1020;

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UpdateStudent";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("id", moi.Id);
                    command.Parameters.AddWithValue("sectionid", moi.SectionID);
                    command.Parameters.AddWithValue("yearresult", moi.YearResult);

                    try
                    {
                        connection.Open();
                        bool hasChanged = command.ExecuteNonQuery() > 0;
                        if (hasChanged) Console.WriteLine("Mise à jour effectuée");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                int idVoisin = 1004;

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DeleteStudent";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("id", idVoisin);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }
    }
}
