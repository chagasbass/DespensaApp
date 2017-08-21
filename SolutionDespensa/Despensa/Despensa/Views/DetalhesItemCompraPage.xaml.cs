using Despensa.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhesItemCompraPage : ContentPage
    {
        public DetalhesItemCompraPage(Produto produto)
        {
            InitializeComponent();
            BindingContext = produto;
        }
    }
}