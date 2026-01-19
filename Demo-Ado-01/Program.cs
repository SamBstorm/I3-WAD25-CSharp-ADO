using Microsoft.Data.SqlClient;

namespace Demo_Ado_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Demo-DB;Integrated Security=True";

            int nbProduct;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Plus nécessaire, car la connection string est repris dans le constructeur
                //connection.ConnectionString = connectionString;

                using(SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT([ProductId]) FROM [Product]";
                    connection.Open();
                    nbProduct = (int)command.ExecuteScalar();
                    connection.Close();
                }
            }

            Console.WriteLine($"Dans ma DB sont enregistrés {nbProduct} produit(s).");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Name], [Description] FROM [Product]";
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"\t- {reader["Name"]}\n\t\t{reader["Description"]}");
                        }
                    }

                    connection.Close();
                }
            }
        }
    }
}
