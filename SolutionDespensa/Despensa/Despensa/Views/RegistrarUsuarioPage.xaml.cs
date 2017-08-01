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
        readonly IPageService PageService;

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

                UsuarioRepo = new UsuarioRepository();
                PageService = new PageService();

                BindingContext = new RegistrarUsuarioViewModel(this.Navigation, UsuarioRepo, PageService);
                ViewModel.NovoUsuario = new Models.Usuario();
            }
            catch (System.Exception ex)
            {
                var e = ex.Message;
            }
		}
	}
}