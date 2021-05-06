using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace UserManager.DataAccess.Migrations
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UsersDatabase"].ConnectionString;
            var services = CreateServices(connectionString);
            var command = args.Any()
                ? args[0]
                : GetCommandFromUser();

            Loop(services, connectionString, command);
            Console.ReadKey();
        }

        private static void Loop(IServiceProvider services, string connectionString, string command)
        {
            try
            {
                CreateDatabase(connectionString);
                ExecuteCommand(services, command);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Next: ");

            var newCommand = GetCommandFromUser();
            if (string.IsNullOrEmpty(newCommand))
            {
                return;
            }
            Loop(services, connectionString, newCommand);
        }

        private static string GetCommandFromUser()
        {
            var availableCommands = string.Join(",", Command.List.Select(x => x.Name));
            Console.WriteLine($"Available commands: {availableCommands}");
            return Console.ReadLine();
        }

        private static void ExecuteCommand(IServiceProvider services, string command)
        {
            var (commandName, commandArguments) = Command.FromString(command);
            commandName.Execute(services, commandArguments);
        }

        private static void CreateDatabase(string connectionString)
        {
            var connectionStringInfo = new SqlConnectionStringBuilder(connectionString);
            var databaseToCreate = connectionStringInfo.InitialCatalog;
            connectionStringInfo.InitialCatalog = "master";
            using (var connection = new SqlConnection(connectionStringInfo.ToString()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{databaseToCreate}') BEGIN CREATE DATABASE [{databaseToCreate}] END";
                var affectedRows = command.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    Console.WriteLine($"Database [{databaseToCreate}] created");
                }
            }
        }

        private static IServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQL Server support to FluentMigrator
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString(connectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Program).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }
    }
}
