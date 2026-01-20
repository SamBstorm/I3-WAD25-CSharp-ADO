using Microsoft.Data.SqlClient;

namespace Exo_ADO_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExoWAD-ADO;Integrated Security=True";

            Student voisin = new Student()
            {
                FirstName = "Alexandre",
                LastName = "Claes",
                BirthDate = new DateTime(1987,1,1),
                SectionID = 1010,
                YearResult = 16
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [Student] ([FirstName], [LastName], [BirthDate], [YearResult], [SectionID]) OUTPUT [inserted].[Id], [inserted].[Active] VALUES (@fn, @ln, @bd, @yr, @sid)";

                    SqlParameter pFN = new SqlParameter() { 
                        ParameterName = "fn",
                        Value = voisin.FirstName
                    };

                    command.Parameters.Add(pFN);

                    command.Parameters.AddWithValue("ln", voisin.LastName);
                    command.Parameters.AddWithValue("bd", voisin.BirthDate);
                    command.Parameters.AddWithValue("yr", voisin.YearResult);
                    command.Parameters.AddWithValue("sid", voisin.SectionID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                voisin.Id = (int)reader[nameof(Student.Id)];
                                voisin.Active = (bool)reader[nameof(Student.Active)];
                            }
                        }
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
            Console.WriteLine($"L'étudiant {voisin.FirstName} {voisin.LastName} a pour identifiant {voisin.Id}");
        }
    }
}
