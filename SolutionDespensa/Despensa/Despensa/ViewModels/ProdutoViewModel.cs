using Despensa.DataContexts;
using Despensa.Models;
using Despensa.Services;
using Despensa.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    public class ProdutoViewModel : BaseViewModel
    {
        public ICommand SelecionarProdutoCommand { get; private set; }
        public ICommand NavegarParaNovoProdutoCommand { get; private set; }
        public ICommand NavegarParaAtualizarProdutoCommand { get; private set; }
        public ICommand ExcluirProdutoCommand { get; private set; }
        public ICommand ListarProdutosCommand { get; private set; }
        public ICommand PesquisarProdutoCommand { get; set; }

        public ObservableCollection<Produto> Produtos { get; private set; } = new ObservableCollection<Produto>();
        //public ObservableCollection<Agrupamento<char, Produto>> Produtos { get; private set; } = new ObservableCollection<Agrupamento<char, Produto>>();

        readonly ProdutoRepository _ProdutoRepository;
        readonly CategoriaRepository _CategoriaRepository;
        readonly INavigationService _Navigation;
        readonly IMessageService _MessageService;

        string _Pesquisa;
        bool _TemItems;

        Produto _ProdutoSelecionado;

        public Produto ProdutoSelecionado
        {
            get { return _ProdutoSelecionado; }
            set { SetValue(ref _ProdutoSelecionado, value); }
        }

        public string Pesquisa
        {
            get { return _Pesquisa; }
            set { SetValue(ref _Pesquisa, value); }
        }

        public bool TemItems
        {
            get { return _TemItems; }
            set { SetValue(ref _TemItems, value); }
        }

        public ProdutoViewModel(ProdutoRepository ProdutoRepository)
        {
            _ProdutoRepository = ProdutoRepository;
            _CategoriaRepository = new CategoriaRepository();
            _Navigation = DependencyService.Get<INavigationService>();
            _MessageService = DependencyService.Get<IMessageService>();

            SelecionarProdutoCommand = new Command<Produto>(async vm => await SelecionarProduto(vm));
            NavegarParaNovoProdutoCommand = new Command(NavegarParaNovoProduto);
            NavegarParaAtualizarProdutoCommand = new Command<Produto>(async vm => await AtualizarProduto(vm));
            ListarProdutosCommand = new Command(ListarProdutos);
            ExcluirProdutoCommand = new Command(ExcluirProduto);
            PesquisarProdutoCommand = new Command(PesquisarProduto);

            TemItems = true;
            //_Navigation.GuardarPaginaAtual(new ListagemDeProdutosPage());
        }

        private async void PesquisarProduto()
        {
            if (string.IsNullOrEmpty(Pesquisa))
            {
                ListarProdutos();
                return;
            }

            var pesquisa = Produtos.Where(x => x.Nome.ToUpper().Contains(Pesquisa.ToUpper())).ToList();

            Produtos.Clear();

            foreach (var item in pesquisa)
            {
                Produtos.Add(item);
            }
        }

        private async Task SelecionarProduto(Produto produto)
        {
            if (produto == null)
                return;
            
            await _Navigation.NavegarParaDetalhesDoProduto(produto);
        }

        private async Task AtualizarProduto(Produto produto)
        {
            if (produto == null)
                return;
            
            await _Navigation.NavegarParaAtualizarProdutos(produto);
        }

        private void ExcluirProduto()
        {
            _ProdutoRepository.ExcluirProdutoAsync(ProdutoSelecionado);
        }

        private async void NavegarParaNovoProduto()
        {
            await _Navigation.NavegarCadastrarProdutos();
        }

        private async void ListarProdutos()
        {
            Produtos.Clear();

            var produtos = _ProdutoRepository.RecuperarProdutos();

            #region recuperando as categorias de cada produto
            foreach (var produto in produtos)
            {
                produto.CriarDetalhes();
                produto.Categoria = _CategoriaRepository.RecuperarCategoriaPorId(produto.IdCategoria);
                Produtos.Add(produto);
            }
            #endregion

            if (Produtos.Count == 0)
            {
                TemItems = false;
                await _MessageService.MostrarDialog("Despensa", "Não existem produtos cadastrados");
                return;
            }
        }

        //private IEnumerable<Agrupamento<char, Produto>> ListarAgrupados(IEnumerable<Produto> Produtos)
        //{
        //    var lista =  from produto in Produtos
        //           orderby produto.Nome
        //           group produto
        //           by produto.Categoria.Nome[0]
        //           into grupos
        //           select new Agrupamento<char, Produto>(grupos.Key, grupos);

        //    return lista;
        //}
    }
}