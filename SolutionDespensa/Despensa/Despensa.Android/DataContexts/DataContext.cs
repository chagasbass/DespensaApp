using Despensa.DataContexts;
using Despensa.Droid.DataContexts.AppSqLite.Droid.DataContexts;
using SQLite;
using SQLite.Net;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataContext))]
namespace Despensa.Droid.DataContexts
{
    namespace AppSqLite.Droid.DataContexts
    {
        /// <summary>
        /// Classe que implementa a interface de acesso à dados
        /// </summary>
        public class DataContext : IDataContext
        {
            public SQLiteConnection GetConnection()
            {
                var nomeDoArquivo = "DespensaDB.db3";

                var caminhoDoDiretorio = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var caminho = Path.Combine(caminhoDoDiretorio, nomeDoArquivo);

                var conexao = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), caminho);

                return conexao;
            }
        }
    }
}