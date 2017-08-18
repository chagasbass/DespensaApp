using Despensa.DataContexts;
using Despensa.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarItemDeCompraPage : ContentPage
    {
        readonly ProdutoRepository ProdutoRepo;
        readonly CategoriaRepository CategoriaRepo;

        private CadastrarItemViewModel ViewModel
        {
            get { return BindingContext as CadastrarItemViewModel; }
            set { BindingContext = value; }
        }

        public CadastrarItemDeCompraPage()
        {
            InitializeComponent();

            ProdutoRepo = new ProdutoRepository();
            CategoriaRepo = new CategoriaRepository();

            BindingContext = new CadastrarItemViewModel(ProdutoRepo, CategoriaRepo);
            ViewModel.NovoProduto = new Models.Produto();
        }
    }
}