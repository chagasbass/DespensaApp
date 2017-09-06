using Despensa.DataContexts;
using Despensa.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using System.Linq;
using Despensa.Services;
using Android.Runtime;

namespace Despensa.ViewModels
{
    [Preserve(AllMembers = true)]
    public class CategoriaViewModel : BaseViewModel
    {
        public ICommand SelecionarCategoriaCommand { get; private set; }
        public ICommand NavegarParaNovoCategoriaCommand { get; private set; }
        public ICommand NavegarParaCategoriaProdutoCommand { get; private set; }
        public ICommand ExcluirCategoriaCommand { get; private set; }
        public ICommand ListarCategoriasCommand { get; private set; }
        public ICommand PesquisarCategoriaCommand { get; set; }
        public ICommand AtualizarCategoriaCommand { get; set; }

        public ObservableCollection<Categoria> Categorias { get; private set; } = new ObservableCollection<Categoria>();

        readonly CategoriaRepository _CategoriaRepository;
        readonly INavigationService _NavigationService;
        readonly IMessageService _MessageService;
        readonly IPopupService _PopupService;

        private Categoria _CategoriaSelecionada;
        private string _Pesquisa;

        public Categoria CategoriaSelecionada
        {
            get { return _CategoriaSelecionada; }
            set { SetValue(ref _CategoriaSelecionada, value); }
        }

        public string Pesquisa
        {
            get { return _Pesquisa; }
            set { SetValue(ref _Pesquisa, value); }
        }

        public CategoriaViewModel(CategoriaRepository CategoriaRepository)
        {
            _NavigationService = DependencyService.Get<INavigationService>();
            _MessageService = DependencyService.Get<IMessageService>();
            _PopupService = DependencyService.Get<IPopupService>();

            _CategoriaRepository = CategoriaRepository;

            SelecionarCategoriaCommand = new Command<Categoria>(async vm => await SelecionarCategoria(vm));
            NavegarParaNovoCategoriaCommand = new Command(NavegarParaNovoCategoria);
            NavegarParaCategoriaProdutoCommand = new Command<Categoria>(async vm => await AtualizarCategoria(vm));
            ListarCategoriasCommand = new Command(ListarCategorias);
            ExcluirCategoriaCommand = new Command(ExcluirCategoria);
            PesquisarCategoriaCommand = new Command(PesquisarCategoria);
        }

        private async void PesquisarCategoria()
        {
            if (String.IsNullOrEmpty(Pesquisa))
            {
                ListarCategorias();
                return;
            }

            var pesquisa = Categorias.Where(x => x.Nome.ToUpper().Contains(Pesquisa.ToUpper())).ToList();

            Categorias.Clear();

            foreach (var item in pesquisa)
            {
                Categorias.Add(item);
            }
        }

        private async Task SelecionarCategoria(Categoria categoria)
        {
            if (categoria == null)
                return;

            await _NavigationService.NavegarParaDetalhesDaCategoria(categoria);
        }

        private async Task AtualizarCategoria(Categoria categoria)
        {
            if (categoria == null)
                return;

            await _NavigationService.NavegarParaAtualizarCategorias(categoria);
        }

        private async void ExcluirCategoria()
        {
            var response = await _MessageService.MostrarDialogComBotoes("Atenção", "Deseja excluir o Item?");

            if (!response)
                return;

            if (CategoriaSelecionada.Original == false)
            {
                _CategoriaRepository.ExcluirCategoria(CategoriaSelecionada);
                await _MessageService.MostrarDialog("Atenção", "Item excluído com sucesso");
            }
            else
            {
                _PopupService.MostrarSnackbar("Esta categoria não pode ser excluída");
            }
        }

        private async void NavegarParaNovoCategoria() => await _NavigationService.NavegarCadastrarCategorias();

        private async void ListarCategorias()
        {
            Categorias.Clear();

            var categorias = _CategoriaRepository.RecuperarCategorias();

            if (categorias == null)
                return;

            foreach (var cat in categorias)
                Categorias.Add(cat);

            if (Categorias.Count == 0)
                await _MessageService.MostrarDialog("Despensa", "Cadastre as suas categorias!!,");
        }
    }
}