using Despensa.DataContexts;
using Despensa.Services;
using System.Windows.Input;
using Xamarin.Forms;

using Despensa.Views;
using System.Threading.Tasks;

namespace Despensa.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        public ICommand RedirecionarParaEsqueciSenhaCommand { get; private set; }
        public ICommand EfetuarLoginCommand { get; private set; }

        readonly UsuarioRepository _UsuarioRepository;
        readonly INavigation _Navigation;
        readonly IPageService _PageService;


        #region Propriedades 
        bool _IsLoading;
        string _Login;
        string _Senha;

        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                SetValue(ref _IsLoading, value);
                OnPropertyChanged(nameof(_IsLoading));
            }
        }

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

        public LoginViewModel(INavigation Navigation, UsuarioRepository UsuarioRepository, IPageService PageService)
        {
            _UsuarioRepository = UsuarioRepository;
            _Navigation = Navigation;
            _PageService = PageService;

            RedirecionarParaEsqueciSenhaCommand = new Command(IRedirecionarParaTrocaDeSenha);
            EfetuarLoginCommand = new Command(EfetuarLogin);
            IsLoading = false;
        }

        private async  void EfetuarLogin(object obj)
        {
            IsLoading = true;

            Login = "teste@teste.com";
            Senha = "teste";

            var eValido = await ValidarLogin();

            if (!eValido)
                return;

            var user = await _UsuarioRepository.ValidarLoginAsync(Login, Senha);

            if(user == null)
            {
                await _PageService.DisplayAlert("Atenção", "Usuário ou Senha inválidos", "OK");
                return;
            }

            //redireciona para a tela inicial da aplicação

            await _Navigation.PushAsync(new MenuPage());
        }

        /// <summary>
        /// Redireciona para a Page de troca de senha
        /// </summary>
        private async void IRedirecionarParaTrocaDeSenha() => await _Navigation.PushAsync(new EsqueciSenhaPage());


        public  async Task<bool> ValidarLogin()
        {
            if (string.IsNullOrEmpty(Login))
            {
                await _PageService.DisplayAlert("Atenção", "O Login é obrigatório", "OK");
                return false;
            }
            if (string.IsNullOrEmpty(Senha))
            {
                await _PageService.DisplayAlert("Atenção", "A Senha é obrigatória", "OK");
                return false;
            }

            return true;
        }
    }
}