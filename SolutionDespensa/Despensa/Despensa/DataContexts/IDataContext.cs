using Android.Runtime;
using SQLite.Net;

namespace Despensa.DataContexts
{
    [Preserve(AllMembers = true)]
    public interface IDataContext
    {
        SQLiteConnection GetConnection();
    }
}