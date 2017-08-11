using Despensa.DataContexts;
using Despensa.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using Despensa.Views;
using System.Linq;

namespace Despensa.ViewModels
{
    public class CategoriaViewModel:BaseViewModel
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
        readonly Page _Page;

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

        public CategoriaViewModel(Page Page, CategoriaRepository CategoriaRepository)
        {
            _Page = Page;
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

            await _Page.Navigation.PushAsync(new DetalhesCategoriaPage(categoria));
        }

        private async Task AtualizarCategoria(Categoria categoria)
        {
            if (categoria == null)
                return;

           await _Page.Navigation.PushAsync(new AtualizarCategoriaPage(categoria));
        }

        private async void ExcluirCategoria()
        {
            var response = await _Page.DisplayAlert("Atenção", "Deseja excluir o Item?", "SIM", "NÃO");

            if (!response)
                return;

            if (CategoriaSelecionada.Original == false)
            {
                _CategoriaRepository.ExcluirCategoria(CategoriaSelecionada);
                await _Page.DisplayAlert("Atenção", "Item excluído com sucesso", "OK");
            }
            else
            { 
                await _Page.DisplayAlert("Atenção", "Esta categoria não pode ser excluída", "OK");
            }
        }

        private async void NavegarParaNovoCategoria()
        {
            await _Page.Navigation.PushAsync(new CadastrarCategoriaPage());
        }

        private async void ListarCategorias()
        {
            Categorias.Clear();

            var categorias =  _CategoriaRepository.RecuperarCategorias();

            if (categorias == null)
                return;

            foreach (var cat in categorias)
                Categorias.Add(cat);
            
            if (Categorias.Count == 0)
                await _Page.DisplayAlert("Despensa", "Cadastre as suas categorias!!,","OK");
        }
    }
}