using Despensa.DataContexts;
using Despensa.Models;
using Despensa.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    public class CadastrarCategoriaViewModel : BaseViewModel
    {
        public ICommand CadastrarNovaCategoriaCommand { get; private set; }

        readonly CategoriaRepository _CategoriaRepository;
        readonly INavigationService _Navigation;
        readonly IMessageService _MessageService;
        readonly IPopupService _PopupService;

        Categoria _NovaCategoria;
        string _Erros;

        public Categoria NovaCategoria
        {
            get { return _NovaCategoria; }
            set
            {
                SetValue(ref _NovaCategoria, value);
                OnPropertyChanged(nameof(_NovaCategoria));
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

        public CadastrarCategoriaViewModel(CategoriaRepository CategoriaRepository)
        {
            _CategoriaRepository = CategoriaRepository;
            _Navigation = DependencyService.Get<INavigationService>();
            _MessageService = DependencyService.Get<IMessageService>();
            _PopupService = DependencyService.Get<IPopupService>();

            CadastrarNovaCategoriaCommand = new Command(CadastrarCategoria);
        }

        private async void CadastrarCategoria()
        {
            var erros = NovaCategoria.ValidarCategoria();

            if (erros.Count > 0)
            {
                foreach (var item in erros)
                {
                    Erros = string.Concat(Erros, "*", item);
                }
                
                await _MessageService.MostrarDialog("Atenção", Erros);

                return;
            }
            else
            {
                var categoriaEncontrada = _CategoriaRepository.RecuperarCategoriaPorNome(NovaCategoria.Nome);

                if (categoriaEncontrada != null)
                {
                    await _MessageService.MostrarDialog("Atenção", "Categoria já cadastrada");
                    return;
                }

                NovaCategoria.FormatarCamposDeItem();

                _CategoriaRepository.CadastrarCategoria(NovaCategoria);

                _PopupService.MostrarSnackbar("Categoria criada com sucesso");

                await _Navigation.NavegarParaListarCategorias();
            }
        }
    }
}