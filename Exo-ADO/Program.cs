using Microsoft.Data.SqlClient;

namespace Exo_ADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExoWAD-ADO;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Console.WriteLine(connection.State);
                connection.Open();
                Console.WriteLine(connection.State);
                connection.Close();
                Console.WriteLine(connection.State);
            }
        }
    }
}
