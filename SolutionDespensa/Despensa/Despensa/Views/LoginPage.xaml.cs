using Despensa.DataContexts;
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

        const string Login = "Login";
        const string Senha = "Senha";

        public string _Login
        {
            get
            {
                if (Application.Current.Properties.ContainsKey(Login))
                    return Application.Current.Properties[Login].ToString();

                return string.Empty;
            }
            set
            {
                Application.Current.Properties[Login] = value;
            }
        }

        public string _Senha
        {
            get
            {
                if (Application.Current.Properties.ContainsKey(Senha))
                    return Application.Current.Properties[Senha].ToString();

                return string.Empty;
            }
            set
            {
                Application.Current.Properties[Senha] = value;
            }
        }

        protected override void OnAppearing()
        {
            if (!string.IsNullOrEmpty(_Login))
            {
                ViewModel.Login = _Login;
                ViewModel.Senha = _Senha;
            }
        }

        protected override void OnDisappearing()
        {
            _Login = ViewModel.Login;
            _Senha = ViewModel.Senha;

            Application.Current.SavePropertiesAsync();
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