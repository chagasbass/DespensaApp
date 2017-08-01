
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage :MasterDetailPage
    {
        public MenuPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.Master = new MenuHamburguerPage();
            this.Detail = new NavigationPage(new ListagemDeProdutosPage());

            App.MasterDetail = this;
        }
    }
}