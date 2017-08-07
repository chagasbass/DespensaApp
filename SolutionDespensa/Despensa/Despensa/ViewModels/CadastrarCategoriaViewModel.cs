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
        readonly INavigation _Navigation;
        readonly IPageService _PageService;

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

        public CadastrarCategoriaViewModel(CategoriaRepository CategoriaRepository, INavigation Navigation, IPageService PageService)
        {
            _CategoriaRepository = CategoriaRepository;
            _Navigation = Navigation;
            _PageService = PageService;

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
                
                await _PageService.DisplayAlert("Atenção", Erros, "OK");

                return;
            }

            var categoriaEncontrada = await _CategoriaRepository.RecuperarCategoriaPorNomeAsync(NovaCategoria.Nome);

            if (categoriaEncontrada != null)
            {
                await _PageService.DisplayAlert("Atenção", "Categoria já cadastrada", "OK");
                return;
            }

            NovaCategoria.FormatarCamposDeItem();

            _CategoriaRepository.CadastrarCategoriaAsync(NovaCategoria);

            await _PageService.DisplayAlert("Despensa", "Categoria criada com sucesso", "OK");
            
            await _Navigation.PopAsync();
        }
    }
}