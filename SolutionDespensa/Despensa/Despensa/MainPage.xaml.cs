using Despensa.DataContexts;
using Despensa.Services;
using Despensa.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            InicializarRepositorios();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new MainViewModel(this.Navigation);
        }

        protected override bool OnBackButtonPressed()
        {
            if (Device.OS == TargetPlatform.Android)
                DependencyService.Get<IAndroidMethods>();

            return base.OnBackButtonPressed();
        }

        private void InicializarRepositorios()
        {

            var UsuarioRepo = new UsuarioRepository();
            var CategoriaRepo = new CategoriaRepository();
            var ProdutoRepo = new ProdutoRepository();

            UsuarioRepo.CriarTabelas();
            CategoriaRepo.CriarTabelas();
            ProdutoRepo.CriarTabelas();
        }
    }
}
