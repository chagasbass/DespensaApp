using Android.Runtime;
using Despensa.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    [Preserve(AllMembers = true)]
    public class MainViewModel : BaseViewModel
    {
        public ICommand RedirecionarParaLoginCommand { get; private set; }
        public ICommand RedirecionarParaRegistroCommand { get; private set; }

        readonly INavigation _Navigation;

        public MainViewModel(INavigation Navigation)
        {
            _Navigation = Navigation;

            RedirecionarParaLoginCommand = new Command(RedirecionarParaLogin);
            RedirecionarParaRegistroCommand = new Command(RedirecionarParaRegistro);
        }

        public async void RedirecionarParaLogin()
        {
            await _Navigation.PushAsync(new LoginPage());
        }

        public async void RedirecionarParaRegistro()
        {
            await _Navigation.PushAsync(new RegistrarUsuarioPage());
        }
    }
}
