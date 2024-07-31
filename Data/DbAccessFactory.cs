using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NPoco;

namespace TimeKeeperApp.Data;

public class DbAcccessFactory
{

    private readonly DatabaseFactory Factory;

    public DbAcccessFactory(IConfiguration configuration)
    {
        var connectionString = configuration?.GetConnectionString("DefaultConnection") ?? "timekeeper.db";
        Console.WriteLine("Connect to DB: " + connectionString);
        Factory = DatabaseFactory.Config(c =>
        {
            c.UsingDatabase(() => new NPoco.Database(connectionString, DatabaseType.SQLite, SqliteFactory.Instance));
            c.WithMapper(new CustomMapper());

            c.WithColumnSerializer(new NewtonsoftJsonColumnSerializer());
        });
    }

    public DbAcccess GetDbAcccess() => new DbAcccess(Factory.GetDatabase(), null);

    class NewtonsoftJsonColumnSerializer : IColumnSerializer
    {
        public object Deserialize(string value, System.Type targetType) => JsonConvert.DeserializeObject(value, targetType) ?? "";
        public string Serialize(object value) => JsonConvert.SerializeObject(value);
    }


}