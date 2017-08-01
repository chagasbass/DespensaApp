using Despensa.DataContexts;
using Despensa.Models;
using Despensa.Services;
using Plugin.LocalNotifications;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    public class RegistrarUsuarioViewModel : BaseViewModel
    {
        public ICommand CadastrarNovoUsuarioCommand { get; private set; }

        readonly UsuarioRepository _UsuarioRepository;
        readonly INavigation _Navigation;
        readonly IPageService _PageService;

        #region Propriedades

        private Usuario _NovoUsuario;
        private string _Erros;
        private bool _IsLoading;

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

        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                SetValue(ref _IsLoading, value);
                OnPropertyChanged(nameof(_IsLoading));
            }
        }

        #endregion

        public RegistrarUsuarioViewModel(INavigation Navigation,UsuarioRepository UsuarioRepository,IPageService PageService)
        {
            _UsuarioRepository = UsuarioRepository;
            _Navigation = Navigation;
            _PageService = PageService;

            CadastrarNovoUsuarioCommand = new Command(CadastrarNovoUsuario);
            IsLoading = false;
        }

        private async void CadastrarNovoUsuario()
        {
            IsLoading = true;

            var erros = NovoUsuario.ValidarUsuario();

            if (erros.Count > 0)
            {
                foreach (var item in erros)
                {
                    Erros = string.Concat(Erros, "*", item);
                }

                IsLoading = false;
                await _PageService.DisplayAlert("Atenção", Erros, "OK");

                return;
            }

            var userEncontrado = await _UsuarioRepository.RecuperarUsuarioPorEmailAsync(NovoUsuario.Email);

            if (userEncontrado != null)
            {
                IsLoading = false;
                await _PageService.DisplayAlert("Atenção", "Usuário já cadastrado", "OK");
                return;
            }

            _UsuarioRepository.CadastrarUsuarioAsync(NovoUsuario);

            //pq da erro aqui??
            //await _PageService.DisplayAlert("Parabéns", "Sua conta foi criada com sucesso", "OK");

            IsLoading = false;

            CrossLocalNotifications.Current.Show("Despensa", string.Concat("Sua conta foi criada,seja bem vindo ", NovoUsuario.Nome, " ", NovoUsuario.Sobrenome));

            //pa da erro aqui e em algumas páginas não??
            await _Navigation.PopAsync();
        }
    }
}