/*
Author: Lauren Holmgren
Date: 11/16/2025
Assignment: PA6-2
*/

using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace FactoryDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nFactory Model Example\n");

            //hard codes the provider and connection string
            string provider = "SqlServer";
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Integrated Security=true;Initial Catalog=AutoLot2023";

            Console.WriteLine($"Provider: {provider}");
            Console.WriteLine($"Connection string: {connectionString}");

            //creates a SQL Server provider factory
            DbProviderFactory factory = GetDbProviderFactory(provider);

            //begins a connection to the database based on the provider factory
            using (DbConnection connection = factory.CreateConnection())
            {
                Console.WriteLine($"\nYour connection object is: {connection.GetType().Name}");

                //completes the connection by adding the connection string
                connection.ConnectionString = connectionString;

                //opens the connection
                connection.Open();

                //creates a command object based on the provider factory
                DbCommand command = factory.CreateCommand();

                Console.WriteLine($"Your command object is: {command.GetType().Name}");

                //connects the command to the database
                command.Connection = connection;

                //adds the SQL query that gets the data to the command
                command.CommandText = "SELECT i.Id, m.Name FROM Inventory i INNER JOIN Makes m ON m.Id = i.MakeId ";

                //creates a data reader object
                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    Console.WriteLine($"Your data reader object is: {dataReader.GetType().Name}");
                    Console.WriteLine("\nCurrent inventory:");

                    //reads the data returned by the query
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"-> Car #{dataReader["Id"]} is a {dataReader["Name"]}.");
                    }
                }
            }
        }

        //creates the provider factory based on the provider name
        static DbProviderFactory GetDbProviderFactory(string provider)
        {
            if (provider == "SqlServer")
            {
                return SqlClientFactory.Instance;
            }
            else
            {
                return null;
            }
        }
    }
}
