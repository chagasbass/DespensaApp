using Android.Runtime;
using Despensa.DataContexts;
using Despensa.Models;
using Despensa.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    [Preserve(AllMembers = true)]
    public class EsqueciSenhaViewModel : BaseViewModel
    {
        public ICommand GerarCodigoDeUsuarioCommand { get; private set; }

        readonly UsuarioRepository _UsuarioRepository;
        readonly INavigationService _Navigation;
        readonly IMessageService _MessageService;
        readonly IPopupService _PopupService;

        UsuarioTrocaSenha _UsuarioTrocaSenha;
        bool _DesabilitarEmail;
        bool _HabilitarSenha;
        string _CodigoGerado;
        string _TextoBotao;

        public bool DesabilitarEmail
        {
            get { return _DesabilitarEmail; }
            set
            {
                SetValue(ref _DesabilitarEmail, value);
                OnPropertyChanged(nameof(_DesabilitarEmail));
            }
        }

        public bool HabilitarSenha
        {
            get { return _HabilitarSenha; }
            set
            {
                SetValue(ref _HabilitarSenha, value);
                OnPropertyChanged(nameof(_HabilitarSenha));
            }
        }

        public UsuarioTrocaSenha UsuarioTrocaSenha
        {
            get { return _UsuarioTrocaSenha; }
            set
            {
                SetValue(ref _UsuarioTrocaSenha, value);
                OnPropertyChanged(nameof(_UsuarioTrocaSenha));
            }
        }

        public string CodigoGerado
        {
            get { return _CodigoGerado; }
            set
            {
                SetValue(ref _CodigoGerado, value);
                OnPropertyChanged(nameof(_CodigoGerado));
            }
        }

        public string TextoBotao
        {
            get { return _TextoBotao; }
            set
            {
                SetValue(ref _TextoBotao, value);
                OnPropertyChanged(nameof(_TextoBotao));
            }
        }

        public EsqueciSenhaViewModel(UsuarioRepository UsuarioRepository)
        {
            _UsuarioRepository = UsuarioRepository;
            _Navigation = DependencyService.Get<INavigationService>();
            _MessageService = DependencyService.Get<IMessageService>();
            _PopupService = DependencyService.Get<IPopupService>();

            GerarCodigoDeUsuarioCommand = new Command(GerarCodigoDeUsuario);
            HabilitarSenha = false;
            DesabilitarEmail = true;
            _TextoBotao = "Solicitar Código";
        }

        /// <summary>
        /// Evento do click do botão quando o usuário inserir o email para recuperar a senha
        /// </summary>
        private async void GerarCodigoDeUsuario()
        {
            if (DesabilitarEmail)
            {
                await GerarCodigo();
            }
            else
            {
                await TrocarSenha();
            }
        }

        private async System.Threading.Tasks.Task GerarCodigo()
        {
            if (string.IsNullOrEmpty(UsuarioTrocaSenha.Email))
            {
                await _MessageService.MostrarDialog("Atenção", "Email não preenchido");
                return;
            }

            if (!string.IsNullOrEmpty(CodigoGerado))
            {
                string mensagemErro = string.Concat("Use o código ", CodigoGerado, " para alterar a senha");
                _PopupService.MostrarSnackbar(mensagemErro);
                return;
            }

            #region  recuperando o user
            var user = _UsuarioRepository.RecuperarUsuarioPorEmail(UsuarioTrocaSenha.Email);

            if (user == null)
            {
                await _MessageService.MostrarDialog("Atenção", "Usuário não encontrado");
                return;
            }

            #endregion

            UsuarioTrocaSenha.GerarCodigo();

            CodigoGerado = UsuarioTrocaSenha.Codigo;

            HabilitarSenha = true;
            DesabilitarEmail = false;
            TextoBotao = "Alterar Senha";

            string mensagem = string.Concat("Use o código ", CodigoGerado, " para alterar a senha");
            _PopupService.MostrarSnackbar(mensagem);
        }

        /// <summary>
        /// Evento do botao quando o usuário já tem o código para trocar a senha
        /// </summary>
        private async System.Threading.Tasks.Task TrocarSenha()
        {
            //testa se senhas sao iguais
            if (UsuarioTrocaSenha.NovaSenha != UsuarioTrocaSenha.ConfirmacaoDeSenha)
            {
                await _MessageService.MostrarDialog("Atenção", "As senhas devem ser iguais");
                return;
            }

            if (string.IsNullOrEmpty(UsuarioTrocaSenha.NovaSenha) || string.IsNullOrEmpty(UsuarioTrocaSenha.ConfirmacaoDeSenha))
            {
                await _MessageService.MostrarDialog("Atenção", "A senha e confirmação de senha devem ser preenchidas");
                return;
            }

            //atualiza o user

            var user = _UsuarioRepository.RecuperarUsuarioPorEmail(UsuarioTrocaSenha.Email);

            user.Senha = UsuarioTrocaSenha.NovaSenha;

            var retorno = _UsuarioRepository.AtualizarUsuario(user);

            if (retorno == null)
            {
                await _MessageService.MostrarDialog("Atenção", "A troca de senha não foi efetuada, tente novamente");
                return;
            }

            await _MessageService.MostrarDialog("Atenção", "A troca de senha foi efetuada");

            string mensagem = "A troca de senha foi efetuada, efetue o login";
            _PopupService.MostrarSnackbar(mensagem);

            await _Navigation.NavegarParaLogin();
        }
    }
}