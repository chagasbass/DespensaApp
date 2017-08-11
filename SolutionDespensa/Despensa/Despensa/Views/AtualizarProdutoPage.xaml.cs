using Despensa.DataContexts;
using Despensa.Models;
using Despensa.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AtualizarProdutoPage : ContentPage
    {
        private AtualizarProdutoViewModel ViewModel
        {
            get { return BindingContext as AtualizarProdutoViewModel; }
            set { BindingContext = value; }
        }

        public AtualizarProdutoPage(Produto produto)
        {
            InitializeComponent();

            var ProdutoRepo = new ProdutoRepository();
            var CategoriaRepo = new CategoriaRepository();

            ViewModel = new AtualizarProdutoViewModel(this,ProdutoRepo, CategoriaRepo,produto);

            BindingContext = ViewModel;
        }

        private void PicStatus_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }
    }
}