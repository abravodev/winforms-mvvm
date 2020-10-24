using Ardalis.SmartEnum;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

namespace UserManager.DataAccess.Migrations
{
    public class Command : SmartEnum<Command>
    {
        private Command(string name, int value) : base(name, value)
        {
        }

        public static readonly Command MigrateToLatest = new Command("MigrateToLatest", 1);
    }
}
