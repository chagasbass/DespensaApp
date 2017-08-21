using Despensa.DataContexts;
using Despensa.Models;
using Despensa.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    public class AtualizarCategoriaViewModel:BaseViewModel
    {
        public ICommand AtualizarCategoriaCommand { get; private set; }

        readonly INavigationService _NavigationService;
        readonly IMessageService _MessageService;
        readonly IPopupService _PopupService;

        readonly CategoriaRepository _CategoriaRepository;

        private Categoria _CategoriaAtualizada;

        public Categoria CategoriaAtualizada
        {
            get { return _CategoriaAtualizada; }
            set { SetValue(ref _CategoriaAtualizada, value); }
        }

        public AtualizarCategoriaViewModel(CategoriaRepository CategoriaRepository)
        {
            _NavigationService = DependencyService.Get<INavigationService>();
            _MessageService = DependencyService.Get<IMessageService>();
            _PopupService = DependencyService.Get<IPopupService>();

            _CategoriaRepository = CategoriaRepository;

            AtualizarCategoriaCommand = new Command(AtualizarContato);
        }

        private async void AtualizarContato()
        {
            if (CategoriaAtualizada.Original == false)
            {
                _CategoriaRepository.AtualizarCategoria(CategoriaAtualizada);
                _PopupService.MostrarSnackbar("Categoria atualizada com sucesso");
            }
            else
            {
                await _MessageService.MostrarDialog("Atenção", "Esta categoria não pode ser atualizada");
            }

           await  _NavigationService.NavegarParaListarCategorias();
        }
    }
}