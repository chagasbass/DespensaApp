using Despensa.DataContexts;
using Despensa.Services;
using Despensa.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EsqueciSenhaPage : ContentPage
    {
        readonly UsuarioRepository UsuarioRepo;
        readonly IMessageService PageService;

        private EsqueciSenhaViewModel ViewModel
        {
            get { return BindingContext as EsqueciSenhaViewModel; }
            set { BindingContext = value; }
        }

        public EsqueciSenhaPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = new EsqueciSenhaViewModel(UsuarioRepo);
            ViewModel.UsuarioTrocaSenha = new Models.UsuarioTrocaSenha();
        }
    }
}