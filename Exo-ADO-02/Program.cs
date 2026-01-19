using Microsoft.Data.SqlClient;
using System.Data;

namespace Exo_ADO_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExoWAD-ADO;Integrated Security=True";

            /*Mode connecté : Liste des étudiants*/

            List<Student> students = new List<Student>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [FirstName], [LastName] FROM [V_Student]";
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student() {
                                Id = (int)reader[nameof(Student.Id)],
                                FirstName = (string)reader[nameof(Student.FirstName)],
                                LastName = (string)reader[nameof(Student.LastName)]
                            });
                        }
                    }
                    connection.Close();
                }
            }

            foreach (var stud in students)
            {
                Console.WriteLine($"{stud.Id}. {stud.LastName} {stud.FirstName}");
            }

            /*Mode déconnecté : Liste des sections*/

            DataTable sectionTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT [Id], [SectionName] FROM [Section]";
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = command;
                        adapter.Fill(sectionTable);
                    }
                }
            }

            foreach (DataRow row in sectionTable.Rows)
            {
                Console.WriteLine($"{row["Id"]} {row["SectionName"]}");
            }

            /* ExecuteScalar */
            double moyenne;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT AVG(CONVERT(Float,YearResult)) FROM Student";
                    connection.Open();
                    moyenne = (double)command.ExecuteScalar();
                    connection.Close();
                }
            }

            Console.WriteLine($"La moyenne annuelle des élèves est de {moyenne} / 20");
        }
    }
}
