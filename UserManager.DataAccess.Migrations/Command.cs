using Ardalis.SmartEnum;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace UserManager.DataAccess.Migrations
{
    public abstract class Command : SmartEnum<Command>
    {
        private Command(string name, int value) : base(name, value) { }

        public static readonly Command MigrateToLatest = new MigrateToLatestCommand();
        public static readonly Command Rollback = new RollbackCommand();

        public static (Command Command, string[] arguments) FromString(string text)
        {
            var commandParameters = text.Split(' ').ToList();
            var command = commandParameters[0];
            commandParameters.RemoveAt(0);
            return (FromName(command), commandParameters.ToArray());
        }

        public string Arguments { get; }

        public abstract void Execute(IServiceProvider serviceProvider, string[] arguments);

        private sealed class MigrateToLatestCommand : Command
        {
            public MigrateToLatestCommand() : base("MigrateToLatest", 1) { }

            public override void Execute(IServiceProvider serviceProvider, string[] arguments)
            {
                // Instantiate the runner
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                runner.MigrateUp();
            }
        }

        private class RollbackCommand : Command
        {
            public RollbackCommand()  : base("Rollback", 2)  { }

            public override void Execute(IServiceProvider serviceProvider, string[] arguments)
            {
                // Instantiate the runner
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                // Migrate down to version
                var version = long.Parse(arguments[0]);
                runner.MigrateDown(version);
            }
        }
    }
}
