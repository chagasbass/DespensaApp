using Android.Runtime;
using Despensa.DataContexts;
using Despensa.Helpers.Despensa.Helpers;
using Despensa.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    [Preserve(AllMembers = true)]
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

            RecuperarUsuarioCommand = new Command(RecuperarUsuario);
        }

        private  void RecuperarUsuario()
        {
            var email = PreferenciasHelper.GravarLogin;

            UsuarioEncontrado =  _UsuarioRepository.RecuperarUsuarioPorEmail(email);
        }
    }
}
