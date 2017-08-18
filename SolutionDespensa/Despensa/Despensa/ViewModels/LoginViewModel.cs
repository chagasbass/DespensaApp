using Despensa.DataContexts;
using Despensa.Services;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Despensa.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        public ICommand RedirecionarParaEsqueciSenhaCommand { get; private set; }
        public ICommand EfetuarLoginCommand { get; private set; }

        readonly UsuarioRepository _UsuarioRepository;
        readonly INavigationService _Navigation;
        readonly IMessageService _MessageService;

        #region Propriedades 
        string _Login;
        string _Senha;

        public string Login
        {
            get { return _Login; }
            set
            {
                SetValue(ref _Login, value);
                OnPropertyChanged(nameof(_Login));
            }
        }

        public string Senha
        {
            get { return _Senha; }
            set
            {
                SetValue(ref _Senha, value);
                OnPropertyChanged(nameof(_Senha));
            }
        }

        #endregion

        public LoginViewModel(UsuarioRepository UsuarioRepository)
        {
            _UsuarioRepository = UsuarioRepository;
            _Navigation = DependencyService.Get<INavigationService>();
            _MessageService = DependencyService.Get<IMessageService>();

            RedirecionarParaEsqueciSenhaCommand = new Command(IRedirecionarParaTrocaDeSenha);
            EfetuarLoginCommand = new Command(EfetuarLogin);
        }

        private async  void EfetuarLogin()
        {
            var eValido = await ValidarLogin();

            if (!eValido)
                return;

            var user =  _UsuarioRepository.ValidarLogin(Login, Senha);

            if(user == null)
            {
                await _MessageService.MostrarDialog("Atenção", "Usuário ou Senha inválidos");
                return;
            }

            await _Navigation.NavegarParaMenu();
        }

        /// <summary>
        /// Redireciona para a Page de troca de senha
        /// </summary>
        private async void IRedirecionarParaTrocaDeSenha() => await _Navigation.NavegarParaEsqueciSenha();

        public  async Task<bool> ValidarLogin()
        {
            if (string.IsNullOrEmpty(Login))
            {
                await _MessageService.MostrarDialog("Atenção", "O Login é obrigatório");
                return false;
            }
            if (string.IsNullOrEmpty(Senha))
            {
                await _MessageService.MostrarDialog("Atenção", "A Senha é obrigatória");
                return false;
            }

            return true;
        }
    }
}