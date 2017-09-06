using Android.Runtime;
using Despensa.Models;
using Despensa.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Despensa.Services
{
    /// <summary>
    /// Servico de navegação do aplicativo
    /// </summary>
    /// 
    [Preserve(AllMembers = true)]
    public class NavigationService : INavigationService
    {
        #region Produtos

        public async Task NavegarCadastrarProdutos()
        {
            var page = new CadastrarProdutoPage();
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(page);

            await App.NavigateMasterDetail(page);
        }

        public async Task NavegarParaAtualizarProdutos(Produto produto)
        {
            var page = new AtualizarProdutoPage(produto);
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(page);

            await App.NavigateMasterDetail(page);
        }
        public async Task NavegarParaDetalhesDoProduto(Produto produto)
        {
            var page = new DetalhesProdutoPage(produto);
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(page);

            await App.NavigateMasterDetail(page);
        }
        public async Task NavegarParaListarProdutos()
        {
            var page = new ListagemDeProdutosPage();
            
            await App.NavigateMasterDetail(page);
        }
        #endregion

        #region Categorias
        public async Task NavegarCadastrarCategorias()
        {
            var page = new CadastrarCategoriaPage();
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(page);

            await App.NavigateMasterDetail(page);
        }
        public async Task NavegarParaAtualizarCategorias(Categoria categoria)
        {
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(new AtualizarCategoriaPage(categoria));
            await App.NavigateMasterDetail(new AtualizarCategoriaPage(categoria));
        }

        public async Task NavegarParaDetalhesDaCategoria(Categoria categoria)
        {
            var page = new DetalhesCategoriaPage(categoria);
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(page);

            await App.NavigateMasterDetail(page);
        }

        public async Task NavegarParaListarCategorias()
        {
            var page = new ListagemDeCategoriasPage();
            
            await App.NavigateMasterDetail(page);
        }
        #endregion

        #region ListaDeCompras
        public async Task NavegarParaCadastrarItemDeCompra()
        {
            var page = new CadastrarItemDeCompraPage();
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(page);

            await App.NavigateMasterDetail(page);
        }

        public async Task NavegarParaListaDeCompras()
        {
            var page = new ListaDeComprasPage();
            
            await App.NavigateMasterDetail(page);
        }
        public async Task NavegarParaDetalhesDeItemDeCompra(Produto produto)
        {
            var page = new DetalhesItemCompraPage(produto);
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(page);
            await App.NavigateMasterDetail(page);
        }
        #endregion

        #region Usuarios
        public async Task NavegarParaEsqueciSenha() => await App.Current.MainPage.Navigation.PushAsync(new EsqueciSenhaPage());
        public async Task NavegarParaLogin() => await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        public async Task NavegarParaMinhaConta()
        {
            var page = new MinhaContaPage();
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(page);
            
            await App.NavigateMasterDetail(page);
        }
        public async Task NavegarRegistrarUsuario() => await App.Current.MainPage.Navigation.PushAsync(new RegistrarUsuarioPage());
        #endregion

        public async Task NavegarParaSobre()
        {
            var page = new SobrePage();
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(page);
            await App.NavigateMasterDetail(page);
        }

        public async Task NavegarParaMenu() => await App.Current.MainPage.Navigation.PushAsync(new MenuPage());

        public async Task Voltar()
        {
            var pagina = App.ListaDePaginas.Pop();

            if(App.ListaDePaginas.Count == 0)
            {
                await App.NavigateMasterDetail(new LoginPage());
                return;
            }
                
            if (pagina == RetornarPaginaAtual())
            {
                await App.NavigateMasterDetail(App.ListaDePaginas.Pop());
                return;
            }

            await App.NavigateMasterDetail(pagina);
        }

        public void GuardarUltimaPagina(Page page) => App.ListaDePaginas.Push(page);


        public void GuardarPaginaAtual(Page page)=> App.PaginaAtual = page;

        public Page RetornarPaginaAtual()
        {
            return App.PaginaAtual;
        }

        public Page RetornarUltimaPagina()
        {
            return App.UltimaPagina;
        }
    }
}
