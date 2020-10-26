using Ardalis.SmartEnum;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace UserManager.DataAccess.Migrations
{
    public abstract class Command : SmartEnum<Command>
    {
        private Command(string name, int value) : base(name, value)
        {
        }

        public static readonly Command MigrateToLatest = new MigrateToLatestCommand();

        public abstract void Execute(IServiceProvider serviceProvider);

        private sealed class MigrateToLatestCommand : Command
        {
            public MigrateToLatestCommand() :base("MigrateToLatest", 1) { }

            public override void Execute(IServiceProvider serviceProvider)
            {
                // Instantiate the runner
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                runner.MigrateUp();
            }
        }
    }
}
