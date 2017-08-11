using Despensa.DataContexts;
using Despensa.Models;
using Despensa.Services;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.LocalNotifications;
using Despensa.Views;

namespace Despensa.ViewModels
{
    public class EsqueciSenhaViewModel : BaseViewModel
    {
        public ICommand GerarCodigoDeUsuarioCommand { get; private set; }

        readonly UsuarioRepository _UsuarioRepository;
        readonly INavigation _Navigation;
        readonly IPageService _PageService;

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

        public EsqueciSenhaViewModel(INavigation Navigation, UsuarioRepository UsuarioRepository, IPageService PageService)
        {
            _UsuarioRepository = UsuarioRepository;
            _Navigation = Navigation;
            _PageService = PageService;
            
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
                await _PageService.DisplayAlert("Atenção", "Email não preenchido", "OK");
                return;
            }

            if (!string.IsNullOrEmpty(CodigoGerado))
            {
                CrossLocalNotifications.Current.Show("Atenção", string.Concat("Use o código ", CodigoGerado, " para alterar a senha"));
                return;
            }

            #region  recuperando o user
            var user = _UsuarioRepository.RecuperarUsuarioPorEmail(UsuarioTrocaSenha.Email);

            if (user == null)
            {
                await _PageService.DisplayAlert("Atenção", "Usuário não encontrado", "OK");
                return;
            }

            #endregion

            UsuarioTrocaSenha.GerarCodigo();

            CodigoGerado = UsuarioTrocaSenha.Codigo;

            HabilitarSenha = true;
            DesabilitarEmail = false;
            TextoBotao = "Alterar Senha";

            CrossLocalNotifications.Current.Show("Despensa", string.Concat("Use o código ", UsuarioTrocaSenha.Codigo, " para alterar a senha"));
        }

        /// <summary>
        /// Evento do botao quando o usuário já tem o código para trocar a senha
        /// </summary>
        private async System.Threading.Tasks.Task TrocarSenha()
        {
            //testa se senhas sao iguais
            if (UsuarioTrocaSenha.NovaSenha != UsuarioTrocaSenha.ConfirmacaoDeSenha)
            {
                await _PageService.DisplayAlert("Atenção", "As senhas devem ser iguais", "OK");
                return;
            }

            //atualiza o user

            var user = _UsuarioRepository.RecuperarUsuarioPorEmail(UsuarioTrocaSenha.Email);

            user.Senha = UsuarioTrocaSenha.NovaSenha;

            var retorno = _UsuarioRepository.AtualizarUsuario(user);

            if (retorno == null)
            {
                await _PageService.DisplayAlert("Atenção", "A troca de senha não foi efetuada, tente novamente", "OK");
                return;
            }

            await _PageService.DisplayAlert("Atenção", "A troca de senha foi efetuada", "OK");

            CrossLocalNotifications.Current.Show("Despensa", "A troca de senha foi efetuada, efetue o login");

            await _Navigation.PushAsync(new LoginPage());
        }
    }
}