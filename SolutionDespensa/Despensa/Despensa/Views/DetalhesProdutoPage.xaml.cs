using Despensa.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhesProdutoPage : ContentPage
    {
        public DetalhesProdutoPage(Produto produto)
        {
            InitializeComponent();
            BindingContext = produto;
        }
    }
}