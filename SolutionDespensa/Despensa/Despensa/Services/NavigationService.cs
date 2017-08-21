using Despensa.Models;
using Despensa.Views;
using System.Threading.Tasks;

namespace Despensa.Services
{
    public class NavigationService : INavigationService
    {
        #region Produtos

        public async Task NavegarCadastrarProdutos() => await App.NavigateMasterDetail(new CadastrarProdutoPage());
        public async Task NavegarParaAtualizarProdutos(Produto produto) => await App.NavigateMasterDetail(new AtualizarProdutoPage(produto));
        public async Task NavegarParaDetalhesDoProduto(Produto produto) => await App.NavigateMasterDetail(new DetalhesProdutoPage(produto));
        public async Task NavegarParaListarProdutos() => await App.NavigateMasterDetail(new ListagemDeProdutosPage());
        #endregion

        #region Categorias
        public async Task NavegarCadastrarCategorias() => await App.NavigateMasterDetail(new CadastrarCategoriaPage());
        public async Task NavegarParaAtualizarCategorias(Categoria categoria) => await App.NavigateMasterDetail(new AtualizarCategoriaPage(categoria));
        public async Task NavegarParaDetalhesDaCategoria(Categoria categoria) => await App.NavigateMasterDetail(new DetalhesCategoriaPage(categoria));
        public async Task NavegarParaListarCategorias() => await App.NavigateMasterDetail(new ListagemDeCategoriasPage());
        #endregion

        #region ListaDeCompras
        public async Task NavegarParaCadastrarItemDeCompra() => await App.NavigateMasterDetail(new CadastrarItemDeCompraPage());
        public async Task NavegarParaListaDeCompras() => await App.NavigateMasterDetail(new ListaDeComprasPage());
        public async Task NavegarParaDetalhesDeItemDeCompra(Produto produto) => await App.Current.MainPage.Navigation.PushAsync(new DetalhesItemCompraPage(produto));
        #endregion

        #region Usuarios
        public async Task NavegarParaEsqueciSenha() => await App.Current.MainPage.Navigation.PushAsync(new EsqueciSenhaPage());
        public async Task NavegarParaLogin() => await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        public async Task NavegarParaMinhaConta() => await App.Current.MainPage.Navigation.PushAsync(new MinhaContaPage());
        public async Task NavegarRegistrarUsuario() => await App.Current.MainPage.Navigation.PushAsync(new RegistrarUsuarioPage());
        #endregion

        public async Task NavegarParaSobre() => await App.NavigateMasterDetail(new SobrePage());

        public async Task NavegarParaMenu() => await App.Current.MainPage.Navigation.PushAsync(new MenuPage());

        public async Task Voltar() => await App.Current.MainPage.Navigation.PopAsync();

      
    }
}
