using SQLite;

namespace Despensa.DataContexts
{
    public interface IDataContext
    {
        SQLiteAsyncConnection GetConnection();
    }
}