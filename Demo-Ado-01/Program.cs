using Microsoft.Data.SqlClient;
using System.Data;

namespace Demo_Ado_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Demo-DB;Integrated Security=True";

            /* Mode connecté : ExecuteScalar (une seule valeur)
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
            */
            /* Mode connecté : ExecuteReader (Plusieurs lignes/colonnes)
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [ProductId], [Name], [Description], [CreationDate] FROM [Product]";
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product()
                            {
                                ProductId = (int)reader[nameof(Product.ProductId)],
                                Name = (string)reader[nameof(Product.Name)],
                                Description = (reader[nameof(Product.Description)] is DBNull) ? null : (string?)reader[nameof(Product.Description)],
                                CreationDate = (DateTime)reader[nameof(Product.CreationDate)]
                            });
                        }
                    }

                    connection.Close();
                }
            }

            foreach (var prod in products)
            {
                Console.WriteLine($"\t- {prod.Name}\n\t\t{prod.Description} - {prod.CreationDate.ToShortDateString()}");
            }*/

            /* Mode Déconnecté : SqlDataAdapter (Dans des DataTable)*/

            DataTable productTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [ProductId], [Name], [Description], [CreationDate] FROM [Product]";

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        adapter.Fill(productTable);
                    }
                }
            }

            foreach (DataRow row in productTable.Rows)
            {
                Console.WriteLine($"\t- {row["Name"]}\n\t\t{row["Description"]} - {((DateTime)row["CreationDate"]).ToShortDateString()}");
            }
        }
    }
}
