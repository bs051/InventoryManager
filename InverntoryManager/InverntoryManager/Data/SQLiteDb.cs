using SQLite;

namespace InverntoryManager.Data
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}

