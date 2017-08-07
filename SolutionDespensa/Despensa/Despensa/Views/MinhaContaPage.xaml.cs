using Despensa.DataContexts;
using Despensa.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MinhaContaPage : ContentPage
    {
        private MinhaContaViewModel ViewModel
        {
            get { return BindingContext as MinhaContaViewModel; }
            set { BindingContext = value; }
        }

        public MinhaContaPage()
        {
            InitializeComponent();
            ViewModel = new MinhaContaViewModel(this.Navigation, new UsuarioRepository());

            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            ViewModel.RecuperarUsuarioCommand.Execute(null);
        }
    }
}