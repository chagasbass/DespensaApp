using Despensa.DataContexts;
using Despensa.Models;
using Despensa.Views;
using Plugin.LocalNotifications;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    public class ProdutoViewModel:BaseViewModel
    {
        public ICommand SelecionarProdutoCommand { get; private set; }
        public ICommand NavegarParaNovoProdutoCommand { get; private set; }
        public ICommand NavegarParaAtualizarProdutoCommand { get; private set; }
        public ICommand ExcluirProdutoCommand { get; private set; }
        public ICommand ListarProdutosCommand { get; private set; }
       
        public ObservableCollection<Produto> Produtos { get; private set; } = new ObservableCollection<Produto>();

        readonly ProdutoRepository _ProdutoRepository;
        readonly Page _Page;

         Produto _ProdutoSelecionado;
         
        public Produto ProdutoSelecionado
        {
            get { return _ProdutoSelecionado; }
            set { SetValue(ref _ProdutoSelecionado, value); }
        }

        public ProdutoViewModel(Page Page, ProdutoRepository ProdutoRepository)
        {
            _Page = Page;
            _ProdutoRepository = ProdutoRepository;

            SelecionarProdutoCommand = new Command<Produto>(async vm => await SelecionarProduto(vm));
            NavegarParaNovoProdutoCommand = new Command(NavegarParaNovoProduto);
            NavegarParaAtualizarProdutoCommand = new Command<Produto>(async vm => await AtualizarProduto(vm));
            ListarProdutosCommand = new Command(ListarProdutos);
            ExcluirProdutoCommand = new Command(ExcluirProduto);
        }

        private async Task SelecionarProduto(Produto produto)
        {
            if (produto == null)
                return;
            
            await _Page.Navigation.PushAsync(new DetalhesProdutoPage(produto));
        }

        private async Task AtualizarProduto(Produto produto)
        {
            if (produto == null)
                return;

            await _Page.DisplayAlert("Atenção", "Atualizando produto", "OK");
            //await _Page.Navigation.PushAsync(new AtualizarContatoPage(produto));
        }

        private async void ExcluirProduto()
        {
            await _Page.DisplayAlert("Atenção", "Excluindo produto", "OK");
            //_ProdutoRepository.ExcluirContatoAsync(ProdutoSelecionado);
        }

        private async void NavegarParaNovoProduto()
        {
            await _Page.Navigation.PushAsync(new CadastrarProdutoPage());
        }

        private async void ListarProdutos()
        {
            try
            {
                Produtos.Clear();

                var produtos = await _ProdutoRepository.RecuperarProdutosAsync();

                if (produtos == null)
                    return;

                //var lista = from p in produtos
                //            orderby p.Nome
                //            group p by p.Categoria.Nome into produtosAgrupados
                //            select new Agrupamento<string, Produto>(produtosAgrupados.Key, produtosAgrupados);

                foreach (var produto in produtos)
                {
                    produto.CriarDetalhes();
                    Produtos.Add(produto);
                }

                if (Produtos.Count == 0)
                    CrossLocalNotifications.Current.Show("Atenção", "A despensa está vazia!!, cadastre os seus items");
            }
            catch (System.Exception ex)
            {
                var e = ex.Message;
            }
        }
    }
}