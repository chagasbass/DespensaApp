using SQLite.Net;

namespace Despensa.DataContexts
{
    public interface IDataContext
    {
        SQLiteConnection GetConnection();
    }
}