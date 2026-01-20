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

            /* Mode Déconnecté : SqlDataAdapter (Dans des DataTable)

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
            }*/

            /* Ordre DML : ExecuteNonQuery
            int nbLigneInseree = 0;

            using (SqlConnection connection = new SqlConnection(connectionString) )
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [Product] ([Name], [Description]) VALUES ('Casque audio', 'Casque audio haute définition, avec micro intégré')";
                    try
                    {
                        connection.Open();
                        nbLigneInseree = command.ExecuteNonQuery();
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

            if (nbLigneInseree > 0) Console.WriteLine("Insertion réussie !");
            else Console.WriteLine("Échec de l'insertion..."); */
            /* Ordre DML avec OUTPUT : ExecuteScalar/ExecuteReader 
            int? productId = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [Product] ([Name], [Description]) OUTPUT [inserted].[ProductId] VALUES ('Casque audio Gaming', 'Casque audio haute définition, avec micro intégré, RGB')";
                    try
                    {
                        connection.Open();
                        productId = (int)command.ExecuteScalar();
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

            if (productId is not null) Console.WriteLine($"Insertion réussie ! L'identifiant du produit est {productId}.");
            else Console.WriteLine("Échec de l'insertion...");
            */

            /* Injection SQL : Requête paramètrée (SqlParameter) */

            Product prod = new Product()
            {
                Name = "Barre de son Gaming",
                Description = "Barre de son 5.1 avec Subwoofer intégré, RGB"
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [Product] ([Name], [Description]) OUTPUT [inserted].[ProductId], [inserted].[CreationDate] VALUES (@prodName, @prodDesc)";

                    //Ajout d'un paramètre classique (pour tous les types de base de données)
                    SqlParameter pName = new SqlParameter() {
                        ParameterName = "prodName",
                        Value = prod.Name
                    };

                    command.Parameters.Add(pName);

                    //Ajout d'un paramètre SQL Server
                    command.Parameters.AddWithValue("prodDesc", (object)prod.Description ?? DBNull.Value);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                prod.ProductId = (int)reader[nameof(Product.ProductId)];
                                prod.CreationDate = (DateTime)reader[nameof(Product.CreationDate)];
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
            }
            Console.WriteLine($"{prod.Name} ({prod.ProductId})\n{prod.Description}\n{prod.CreationDate}");
        }
    }
}
