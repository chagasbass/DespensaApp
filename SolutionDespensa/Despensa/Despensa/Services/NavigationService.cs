using Despensa.Models;
using Despensa.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Despensa.Services
{
    /// <summary>
    /// Servico
    /// </summary>
    public class NavigationService : INavigationService
    {
        #region Produtos

        public async Task NavegarCadastrarProdutos()
        {
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(new CadastrarProdutoPage());
            await App.NavigateMasterDetail(new CadastrarProdutoPage());
        }

        public async Task NavegarParaAtualizarProdutos(Produto produto)
        {
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(new AtualizarProdutoPage(produto));
            await App.NavigateMasterDetail(new AtualizarProdutoPage(produto));
        }
        public async Task NavegarParaDetalhesDoProduto(Produto produto)
        {
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(new DetalhesProdutoPage(produto));
            await App.NavigateMasterDetail(new DetalhesProdutoPage(produto));
        }
        public async Task NavegarParaListarProdutos()
        {
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(new ListagemDeProdutosPage());
            await App.NavigateMasterDetail(new ListagemDeProdutosPage());
        }
        #endregion

        #region Categorias
        public async Task NavegarCadastrarCategorias()
        {
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(new ListagemDeCategoriasPage());
            await App.NavigateMasterDetail(new CadastrarCategoriaPage());
        }
        public async Task NavegarParaAtualizarCategorias(Categoria categoria)
        {
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(new AtualizarCategoriaPage(categoria));
            await App.NavigateMasterDetail(new AtualizarCategoriaPage(categoria));
        }

        public async Task NavegarParaDetalhesDaCategoria(Categoria categoria)
        {
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(new DetalhesCategoriaPage(categoria));
            await App.NavigateMasterDetail(new DetalhesCategoriaPage(categoria));
        }

        public async Task NavegarParaListarCategorias()
        {
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(new ListagemDeCategoriasPage());
            await App.NavigateMasterDetail(new ListagemDeCategoriasPage());
        }
        #endregion

        #region ListaDeCompras
        public async Task NavegarParaCadastrarItemDeCompra()
        {
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(new CadastrarItemDeCompraPage());
            await App.NavigateMasterDetail(new CadastrarItemDeCompraPage());
        }

        public async Task NavegarParaListaDeCompras()
        {
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(new ListaDeComprasPage());
            await App.NavigateMasterDetail(new ListaDeComprasPage());
        }
        public async Task NavegarParaDetalhesDeItemDeCompra(Produto produto)
        {
            GuardarUltimaPagina(App.PaginaAtual);
            GuardarPaginaAtual(new DetalhesItemCompraPage(produto));
            await App.NavigateMasterDetail(new DetalhesItemCompraPage(produto));
        }
        #endregion

        #region Usuarios
        public async Task NavegarParaEsqueciSenha() => await App.Current.MainPage.Navigation.PushAsync(new EsqueciSenhaPage());
        public async Task NavegarParaLogin() => await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        public async Task NavegarParaMinhaConta()
        {
            GuardarPaginaAtual(new MinhaContaPage());
            GuardarUltimaPagina(App.PaginaAtual);
            await App.NavigateMasterDetail(new MinhaContaPage());
        }
        public async Task NavegarRegistrarUsuario() => await App.Current.MainPage.Navigation.PushAsync(new RegistrarUsuarioPage());
        #endregion

        public async Task NavegarParaSobre()
        {
            GuardarPaginaAtual(new SobrePage());
            GuardarUltimaPagina(App.PaginaAtual);
            await App.NavigateMasterDetail(new SobrePage());
        }

        public async Task NavegarParaMenu() => await App.Current.MainPage.Navigation.PushAsync(new MenuPage());

        public async Task Voltar()
        {
            var pagina = App.ListaDePaginas.Pop();

            if(pagina == null)
                await App.NavigateMasterDetail(new LoginPage());

            if (pagina == RetornarPaginaAtual())
                await App.NavigateMasterDetail(RetornarUltimaPagina());
            else
                await App.NavigateMasterDetail(App.ListaDePaginas.Pop());
        }

        public void GuardarUltimaPagina(Page page)
        {
            App.UltimaPagina = page;
            App.ListaDePaginas.Push(page);
        }

        public void GuardarPaginaAtual(Page page) => App.PaginaAtual = page;

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
