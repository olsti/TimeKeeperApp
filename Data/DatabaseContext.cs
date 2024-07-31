using System;
using Microsoft.Data.Sqlite;
using NPoco;

namespace TimeKeeperApp.Data
{
    public class DatabaseContext : Database
    {
        public DatabaseContext()
            : base(new SqliteConnection("Data Source=timekeeper.db"), DatabaseType.SQLite)
        {
        }
    }
}