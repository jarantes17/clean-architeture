using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BPDotNet.Scripts
{
    public class Program
    {
        private static IConfigurationRoot _configuration;

        static void Main(string[] args)
        {
            _configuration = ConfigurationFactory.Create();

            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices()
        {
            string targetConnectionString = _configuration.GetConnectionString("Default");

            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddMySql5()
                    .WithGlobalConnectionString(targetConnectionString)
                    .ScanIn(typeof(Program).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}