using Android.Runtime;
using Despensa.Models;
using Xamarin.Forms;

namespace Despensa.DataContexts
{
    /// <summary>
    /// Classe que efetua a inicialização do banco de dados
    /// </summary>
    /// 
    [Preserve(AllMembers = true)]
    public static class DataContextInitializer
    {
        public static void InicializarBancoDeDados()
        {
            var conexao = DependencyService.Get<IDataContext>().GetConnection();

            conexao.CreateTable<Usuario>();
            conexao.CreateTable<Categoria>();
            conexao.CreateTable<Produto>();

            var CategoriaRepo = new CategoriaRepository();
            CategoriaRepo.InicializarCategorias();
        }
    }
}
