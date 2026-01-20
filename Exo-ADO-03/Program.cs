using Microsoft.Data.SqlClient;

namespace Exo_ADO_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExoWAD-ADO;Integrated Security=True";

            Student sam = new Student()
            {
                FirstName = "Aude",
                LastName = "Beurive",
                BirthDate = new DateTime(1987,1,1),
                YearResult = 18,
                SectionID = 1020,
                Active = true
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    //command.CommandText = $"INSERT INTO [Student] ([FirstName], [LastName], [BirthDate], [YearResult], [SectionID], [Active]) OUTPUT [inserted].[Id] VALUES ('{sam.FirstName}' , '{sam.LastName}' , '{sam.BirthDate.ToString("yyyy-MM-dd")}' , {sam.YearResult} , {sam.SectionID} , {(sam.Active ? 1 : 0)} )";
                    //Sans colonne Active car par defaut à 1
                    command.CommandText = $"INSERT INTO [Student] ([FirstName], [LastName], [BirthDate], [YearResult], [SectionID]) OUTPUT [inserted].[Id] VALUES ('{sam.FirstName}' , '{sam.LastName}' , '{sam.BirthDate.ToString("yyyy-MM-dd")}' , {sam.YearResult} , {sam.SectionID} )";

                    try
                    {
                        connection.Open();
                        sam.Id = (int)command.ExecuteScalar();
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
            }
            Console.WriteLine($"L'étudiant {sam.FirstName} {sam.LastName} a l'identifiant {sam.Id}.");
        }
    }
}
