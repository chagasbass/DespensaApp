using Despensa.Models;
using Despensa.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhesProdutoPage : ContentPage
    {
        private DetalhesProdutoViewModel ViewModel
        {
            get { return BindingContext as DetalhesProdutoViewModel; }
            set { BindingContext = value; }
        }

        public DetalhesProdutoPage(Produto produto)
        {
            InitializeComponent();
            ViewModel = new DetalhesProdutoViewModel();
            ViewModel.ProdutoSelecionado = produto;
            BindingContext = ViewModel;
        }
    }
}