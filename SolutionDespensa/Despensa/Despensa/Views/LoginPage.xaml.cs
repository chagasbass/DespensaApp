using Despensa.DataContexts;
using Despensa.Services;
using Despensa.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        readonly UsuarioRepository UsuarioRepo;
        readonly IPageService PageService;

        private LoginViewModel ViewModel
        {
            get { return BindingContext as LoginViewModel; }
            set { BindingContext = value; }
        }
        public LoginPage ()
		{
			InitializeComponent ();

            UsuarioRepo = new UsuarioRepository();
            PageService = new PageService();

            BindingContext = new LoginViewModel(this.Navigation, UsuarioRepo, PageService);
        }
	}
}