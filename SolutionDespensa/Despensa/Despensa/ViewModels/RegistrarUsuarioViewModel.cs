using Android.Runtime;
using Despensa.DataContexts;
using Despensa.Helpers.Despensa.Helpers;
using Despensa.Models;
using Despensa.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    [Preserve(AllMembers = true)]
    public class RegistrarUsuarioViewModel : BaseViewModel
    {
        public ICommand CadastrarNovoUsuarioCommand { get; private set; }

        readonly UsuarioRepository _UsuarioRepository;
        readonly INavigationService _Navigation;
        readonly IMessageService _MessageService;
        readonly IPopupService _PopupService;

        #region Propriedades

        private Usuario _NovoUsuario;
        private string _Erros;

        public Usuario NovoUsuario
        {
            get { return _NovoUsuario; }
            set
            {
                SetValue(ref _NovoUsuario, value);
                OnPropertyChanged(nameof(_NovoUsuario));
            }
        }

        public string Erros
        {
            get { return _Erros; }
            set
            {
                SetValue(ref _Erros, value);
                OnPropertyChanged(nameof(_Erros));
            }
        }

        #endregion

        public RegistrarUsuarioViewModel(UsuarioRepository UsuarioRepository)
        {
            _UsuarioRepository = UsuarioRepository;
            _Navigation = DependencyService.Get<INavigationService>();
            _MessageService = DependencyService.Get<IMessageService>();
            _PopupService = DependencyService.Get<IPopupService>();

            CadastrarNovoUsuarioCommand = new Command(CadastrarNovoUsuario);
        }

        private async void CadastrarNovoUsuario()
        {
            var erros = NovoUsuario.ValidarUsuario();

            if (erros.Count > 0)
            {
                foreach (var item in erros)
                    Erros = string.Concat(Erros, "*", item);

                await _MessageService.MostrarDialog("Atenção", Erros);

                return;
            }

            var userEncontrado =  _UsuarioRepository.RecuperarUsuarioPorEmail(NovoUsuario.Email);

            if (userEncontrado != null)
            {
                _PopupService.MostrarToast("Usuário já cadastrado");
                return;
            }

            NovoUsuario.Nome = char.ToUpper(NovoUsuario.Nome[0]) + NovoUsuario.Nome.Substring(1);
            NovoUsuario.Sobrenome= char.ToUpper(NovoUsuario.Sobrenome[0]) + NovoUsuario.Sobrenome.Substring(1);

            _UsuarioRepository.CadastrarUsuario(NovoUsuario);

            GravarPreferenciasDeUsuario();

            string mensagem  = string.Concat("Sua conta foi criada,seja bem vindo ", NovoUsuario.Nome, " ", NovoUsuario.Sobrenome);

            _PopupService.MostrarSnackbar(mensagem);

            await _Navigation.NavegarParaLogin();
        }

        /// <summary>
        /// Grava as preferencias do usuário
        /// </summary>
        private void GravarPreferenciasDeUsuario()
        {
            PreferenciasHelper.GravarLogin = NovoUsuario.Email;
            PreferenciasHelper.GravarSenha = NovoUsuario.Senha;
            PreferenciasHelper.GravarNomeUsuario = NovoUsuario.Nome;
        }
    }
}