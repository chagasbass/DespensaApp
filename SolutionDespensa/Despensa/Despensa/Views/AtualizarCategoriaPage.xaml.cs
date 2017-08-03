using Despensa.DataContexts;
using Despensa.Models;
using Despensa.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AtualizarCategoriaPage : ContentPage
    {
        

        private AtualizarCategoriaViewModel ViewModel
        {
            get { return BindingContext as AtualizarCategoriaViewModel; }
            set { BindingContext = value; }
        }

        public AtualizarCategoriaPage(Categoria categoria)
        {
            InitializeComponent();
            ViewModel = new AtualizarCategoriaViewModel(this, new CategoriaRepository());
            ViewModel.CategoriaAtualizada = categoria;

            BindingContext = ViewModel;
        }
    }
}