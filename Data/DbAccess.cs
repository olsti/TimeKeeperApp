
namespace TimeKeeperApp.Data;

public class DbAcccess(NPoco.Database db, Action disposeNotification) : IDisposable
{
    private readonly Action DisposeNotification = disposeNotification;

    public NPoco.Database DB { get; } = db;

    public void Dispose()
    {
        DB.Dispose();
        DisposeNotification?.Invoke();
    }
}