using Despensa.DataContexts;
using Despensa.Services;
using Despensa.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrarUsuarioPage : ContentPage
	{
        readonly UsuarioRepository UsuarioRepo;
        readonly IMessageService PageService;

        private RegistrarUsuarioViewModel ViewModel
        {
            get { return BindingContext as RegistrarUsuarioViewModel; }
            set { BindingContext = value; }
        }

        public RegistrarUsuarioPage ()
		{
            try
            {
                InitializeComponent();
                NavigationPage.SetHasNavigationBar(this, false);
                UsuarioRepo = new UsuarioRepository();

                BindingContext = new RegistrarUsuarioViewModel(UsuarioRepo);
                ViewModel.NovoUsuario = new Models.Usuario();
            }
            catch (System.Exception ex)
            {
                var e = ex.Message;
            }
		}
	}
}