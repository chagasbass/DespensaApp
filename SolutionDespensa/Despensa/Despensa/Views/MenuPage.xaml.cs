
using Despensa.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage :MasterDetailPage
    {
        readonly INavigationService _service = DependencyService.Get<INavigationService>();

        public MenuPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            var page = new ListagemDeProdutosPage();
            this.Master = new MenuHamburguerPage();
            this.Detail = new NavigationPage(page);
            _service.GuardarUltimaPagina(page);
            _service.GuardarPaginaAtual(page);

            App.MasterDetail = this;
        }

        protected override  bool OnBackButtonPressed()
        {
                if (Device.OS == TargetPlatform.Android)
                {
                    DependencyService.Get<IAndroidMethods>();
                    _service.Voltar();
                    return true;
                }

                return false;
        }
    }
}