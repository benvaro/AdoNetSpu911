using System;
using System.Configuration;
using System.Data.SqlClient;

namespace _02_ConnectedMode
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["local"].ConnectionString;

            #region Connection string builder
            //var connectionStringBuilder = new SqlConnectionStringBuilder();
            //connectionStringBuilder.UserID = "test";
            //connectionStringBuilder.Password = "1";
            //connectionStringBuilder.DataSource = "194.44.93.225"; 
            #endregion

            //var connection = new SqlConnection(connectionStringBuilder.ConnectionString);
            var connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();
                ReadData(connection);
                Console.WriteLine("Enter id: ");
                var id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter brand: ");
                var brand = Console.ReadLine();

                var brandParameter = new SqlParameter("@p1", System.Data.SqlDbType.NVarChar, 15);
                brandParameter.Value = brand;

                var commandText = $"Update Car set Brand=@p1 where Id=@p2";
                var command = new SqlCommand(commandText, connection);
                command.Parameters.Add(brandParameter);
                command.Parameters.AddWithValue("@p2", id);
                command.ExecuteNonQuery();
                Console.WriteLine();
                ReadData(connection);
            }
        }

        private static void ReadData(SqlConnection connection)
        {
            var commandText = $"SELECT * FROM Car";
            var command = new SqlCommand(commandText, connection);
            var flag = true;
            using (var sqlReader = command.ExecuteReader())
            {
                for (int i = 0; i < sqlReader.FieldCount; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{sqlReader.GetName(i),-15}");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                while (sqlReader.Read())
                {
                    if (flag)
                    {

                        flag = false;
                    }

                    for (int i = 0; i < sqlReader.FieldCount; i++)
                    {
                        Console.Write($"{sqlReader.GetValue(i),-15}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
