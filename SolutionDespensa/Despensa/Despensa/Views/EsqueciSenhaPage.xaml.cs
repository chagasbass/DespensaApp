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
        readonly IPageService PageService;

        private EsqueciSenhaViewModel ViewModel
        {
            get { return BindingContext as EsqueciSenhaViewModel; }
            set { BindingContext = value; }
        }

        public EsqueciSenhaPage()
        {
            InitializeComponent();

            UsuarioRepo = new UsuarioRepository();
            PageService = new PageService();

            BindingContext = new EsqueciSenhaViewModel(this.Navigation, UsuarioRepo, PageService);
            ViewModel.UsuarioTrocaSenha = new Models.UsuarioTrocaSenha();
        }
    }
}