using Despensa.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhesCategoriaPage : ContentPage
    {
        public DetalhesCategoriaPage(Categoria categoria)
        {
            InitializeComponent();
            BindingContext = categoria;
        }
    }
}