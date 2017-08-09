using Despensa.DataContexts;
using Despensa.Helpers.Despensa.Helpers;
using Despensa.Services;
using Despensa.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        readonly UsuarioRepository UsuarioRepo;
        readonly IPageService PageService;


        protected override void OnAppearing()
        {
            ViewModel.Login = PreferenciasHelper.GravarLogin;
            ViewModel.Senha = PreferenciasHelper.GravarSenha;
        }

        private LoginViewModel ViewModel
        {
            get { return BindingContext as LoginViewModel; }
            set { BindingContext = value; }
        }
        public LoginPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            UsuarioRepo = new UsuarioRepository();
            PageService = new PageService();

            BindingContext = new LoginViewModel(this.Navigation, UsuarioRepo, PageService);

        }
	}
}