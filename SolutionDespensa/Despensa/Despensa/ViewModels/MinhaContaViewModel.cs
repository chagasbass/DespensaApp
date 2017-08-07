using Despensa.DataContexts;
using Despensa.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    public class MinhaContaViewModel:BaseViewModel
    {
        public ICommand RecuperarUsuarioCommand { get; private set; }

        readonly UsuarioRepository _UsuarioRepository;
        readonly INavigation _Navigation;

        Usuario _UsuarioEncontrado;

        public Usuario UsuarioEncontrado
        {
            get { return _UsuarioEncontrado; }
            set
            {
                SetValue(ref _UsuarioEncontrado, value);
                OnPropertyChanged(nameof(_UsuarioEncontrado));
            }
        }

        public MinhaContaViewModel(INavigation Navigation, UsuarioRepository UsuarioRepository)
        {
            _Navigation = Navigation;
            _UsuarioRepository = UsuarioRepository;

            RecuperarUsuarioCommand = new Command(RecuperarUsuarioAsync);
        }

        private async void RecuperarUsuarioAsync()
        {
            var email = Application.Current.Properties["Login"].ToString();

            UsuarioEncontrado = await _UsuarioRepository.RecuperarUsuarioPorEmailAsync(email);
        }
    }
}
