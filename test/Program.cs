using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;


namespace test;

class Program
{
    static void Main(string[] args)
    {
        using(var serviceProvider = CreateService())
        using( var scope  = serviceProvider.CreateScope())
        {
            UpdateDatabase(scope.ServiceProvider);
        }
    }

    private static ServiceProvider CreateService()
    {
        return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(r => r
                .AddMySql8()
                .WithGlobalConnectionString("Server=localhost;Database=PersonDb;Uid=root;Pwd=root;")
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