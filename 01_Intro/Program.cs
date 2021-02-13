using System;
using System.Data.SqlClient;

namespace _01_Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                                    Initial Catalog=master;
                                    Integrated Security=True;";

            var connection = new SqlConnection(connectionString);
            connection.StateChange += Connection_StateChange;
            //connection.Open();
            //Console.WriteLine("Connection: State: " + connection.State);
            //Console.WriteLine("Connection: Database: " + connection.Database);
            //Console.WriteLine("Connection: Server: " + connection.DataSource);
            //connection.Close();

            using (connection)
            {
                connection.Open();
                #region Create database
                //var commandText = "CREATE DATABASE Demo";
                //var command = new SqlCommand(commandText, connection);
                //var result = command.ExecuteNonQuery(); // CREATE INSERT DELETE UPDATE
                //Console.WriteLine("Result create: " + result);
                #endregion

                connection.ChangeDatabase("Demo");
                #region Create table
                //var commandText = "CREATE TABLE Car(" +
                //    "Id INT IDENTITY PRIMARY KEY," +
                //    "Brand nvarchar(15) NOT NULL," +
                //    "Model nvarchar(10) NOT NULL)";

                //var command = new SqlCommand(commandText, connection);
                //command.ExecuteNonQuery(); 
                #endregion

                #region Insert data
                //var commandText = "INSERT INTO Car VALUES ('Audi', 'R8')," +
                //    "('Mazda', 'CX-5')";

                //var command = new SqlCommand(commandText, connection);
                //var result = command.ExecuteNonQuery();
                //Console.WriteLine("Inserted: " + result);
                #endregion
                //command.ExecuteScalar(); // min, count, max, sum, avg

                #region Select from table
                var commandText = "SELECT * FROM Car; select * from Car;";
                var command = new SqlCommand(commandText, connection);
                var flag = true;
                using (var sqlReader = command.ExecuteReader())
                {

                    do
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
                    while (sqlReader.NextResult());
                }

                #endregion
                //command.ExecuteReader(); // SELECT
            }
        }

        private static void Connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            var obj = sender as SqlConnection;
            Console.WriteLine($"State changed: {obj.State}");
        }
    }
}
