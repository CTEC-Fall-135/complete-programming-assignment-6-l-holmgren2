/*
Author: Lauren Holmgren
Date: 11/16/2025
Assignment: PA6-2
*/

using Microsoft.Extensions.Configuration;

namespace ConfigDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var providerInfo = GetProviderFromConfiguration();
            Console.WriteLine("\nProvider info\nProvider: {0}\nConnection string: {1}\n", providerInfo.Item1, providerInfo.Item2);
        }

        static (string Provider, string ConnectionString) GetProviderFromConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", true, true)
                            .Build();
            return (config["ProviderName"], config["SqlServer:ConnectionString"]);
        }
    }
}
