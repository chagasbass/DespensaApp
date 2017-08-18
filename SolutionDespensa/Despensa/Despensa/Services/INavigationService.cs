using Despensa.Models;
using System.Threading.Tasks;

namespace Despensa.Services
{
    /// <summary>
    /// Interface de navegação so APP
    /// </summary>
    public interface INavigationService
    {
        Task NavegarParaLogin();
        Task NavegarRegistrarUsuario();
        Task NavegarParaListarProdutos();
        Task NavegarCadastrarProdutos();
        Task NavegarParaAtualizarProdutos(Produto produto);
        Task NavegarParaDetalhesDoProduto(Produto produto);
        Task NavegarParaListarCategorias();
        Task NavegarCadastrarCategorias();
        Task NavegarParaAtualizarCategorias(Categoria categoria);
        Task NavegarParaDetalhesDaCategoria(Categoria categoria);
        Task NavegarParaSobre();
        Task NavegarParaMinhaConta();
        Task NavegarParaEsqueciSenha();
        Task NavegarParaListaDeCompras();
        Task NavegarParaCadastrarItemDeCompra();
        Task NavegarParaMenu();
        Task Voltar();
    }
}
