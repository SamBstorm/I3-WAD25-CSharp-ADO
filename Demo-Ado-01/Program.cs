using Microsoft.Data.SqlClient;

namespace Demo_Ado_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Demo-DB;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;

                Console.WriteLine(connection.State);

                connection.Open();

                Console.WriteLine(connection.State);

                connection.Close();

                Console.WriteLine(connection.State);
            }
        }
    }
}
