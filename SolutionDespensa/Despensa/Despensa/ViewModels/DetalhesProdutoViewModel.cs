using Despensa.Models;
using Despensa.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    public class DetalhesProdutoViewModel:BaseViewModel
    {
        public ICommand NavegarParaAtualizarProdutoCommand { get; private set; }

        readonly INavigationService _Navigation;

        Produto _ProdutoSelecionado;

        public Produto ProdutoSelecionado
        {
            get { return _ProdutoSelecionado; }
            set { SetValue(ref _ProdutoSelecionado, value); }
        }

        public DetalhesProdutoViewModel()
        {
            NavegarParaAtualizarProdutoCommand = new Command<Produto>(async vm => await AtualizarProduto(vm));
            _Navigation = DependencyService.Get<INavigationService>();
        }

        private async Task AtualizarProduto(Produto produto)
        {
            if (produto == null)
                return;

            await _Navigation.NavegarParaAtualizarProdutos(produto);
        }
    }
}