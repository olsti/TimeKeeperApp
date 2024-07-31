using System.Data;
using Microsoft.Data.Sqlite;

namespace TimeKeeperApp.Data;
public class DatabaseInitializer
{
    public static void InitializeDatabase(IConfiguration configuration)
    {
        var connectionString = configuration?.GetConnectionString("DefaultConnection") ?? "timekeeper.db";
        Console.WriteLine("InitializeDatabase: Connect to DB: " + connectionString);
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS timerecord (
                    id	TEXT PRIMARY NOT NULL,
                    created_at	    TEXT NOT NULL,
                    updated_at	    TEXT,
                    starttime_at    TEXT NOT NULL,
                    endtime_at      TEXT NOT NULL,
                	breaktime	    NUMERIC NOT NULL DEFAULT 0,
                    note	TEXT NOT NULL DEFAULT ""
                )";
        tableCmd.ExecuteNonQuery();
    }
}