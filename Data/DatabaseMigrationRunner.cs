using FluentMigrator.Runner;

namespace TimeKeeperApp.Data;
public static class DatabaseMigrationRunner
{
    public static IServiceCollection AddDatabaseMigrationRunner(this IServiceCollection serviceCollection, string connectionString)
    {
        Console.WriteLine("DatabaseMigrationRunner: Connect to DB: " + connectionString);
        serviceCollection.AddFluentMigratorCore();
        serviceCollection.ConfigureRunner(c => c
        .AddSQLite()
        .WithGlobalConnectionString(connectionString)
        .WithMigrationsIn(typeof(Program).Assembly)
        .WithVersionTable(new VersionTableMetaData()));
        serviceCollection.AddLogging(lb => lb.AddFluentMigratorConsole());
        return serviceCollection;
    }

    public static void Run(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}