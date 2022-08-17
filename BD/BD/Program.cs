using System;
using System.Data;
using System.Data.SqlClient;

namespace BD
{
    class Program
    {
        
      

        static void Main(string[] args)
        {
            Console.WriteLine("Введите сервер для базы данных:");
            string server = Console.ReadLine();
            Console.WriteLine("Введите название базы данных:");
            string name = Console.ReadLine();

            CreateBD createBd = new CreateBD(server, name);
              createBd.CreateBd();
               createBd.CreateTableCust();
              createBd.CreateTableOrders();
              createBd.AddCust();
             createBd.AddOrders();
            string connectionString = $"Server={server};Database={name};Trusted_connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            try
            {
                string sqlCommand = "SELECT Name as Customers FROM Customers " +
                    "LEFT JOIN Orders ON Customers.Id= Orders.CustomerId " +
                    "WHERE Orders.CustomerId is null";


                SqlCommand command = new SqlCommand(sqlCommand, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine(reader[i] + "\t");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            } 
        }
    }
}