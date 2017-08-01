using Despensa.DataContexts;
using Despensa.Droid.DataContexts.AppSqLite.Droid.DataContexts;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataContext))]
namespace Despensa.Droid.DataContexts
{
    namespace AppSqLite.Droid.DataContexts
    {
        public class DataContext : IDataContext
        {
            public SQLiteAsyncConnection GetConnection()
            {
                var fileName = "DespensaDB.db3";

                var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var path = Path.Combine(documentsPath, fileName);

                return new SQLiteAsyncConnection(path);
            }
        }
    }
}