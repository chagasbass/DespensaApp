using Despensa.DataContexts;
using Despensa.Models;
using Despensa.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListagemDeCategoriasPage : ContentPage
    {
        CategoriaRepository _Repo;

        private CategoriaViewModel ViewModel
        {
            get { return BindingContext as CategoriaViewModel; }
            set { BindingContext = value; }
        }

        public ListagemDeCategoriasPage()
        {
            InitializeComponent();
            _Repo = new CategoriaRepository();
            BindingContext = new CategoriaViewModel(_Repo);
        }

        private async void ToolbarItem_Activated(object sender, EventArgs e)=> ViewModel.NavegarParaNovoCategoriaCommand.Execute(null);

        protected override async void OnAppearing() => ViewModel.ListarCategoriasCommand.Execute(null);

        private async void listViewCategorias_Refreshing(object sender, EventArgs e)
        {
            ViewModel.ListarCategoriasCommand.Execute(null);
            listViewCategorias.EndRefresh();
        }

        private async void MenuItem_EditarCategorias(object sender, EventArgs e)
        {
            var escolha = (MenuItem)sender;
            var categoria = (Categoria)escolha.CommandParameter;

            ViewModel.NavegarParaCategoriaProdutoCommand.Execute(categoria);
        }

        private async void MenuItem_ExcluirCategorias(object sender, EventArgs e)
        {
            var escolha = (MenuItem)sender;
            var categoria = (Categoria)escolha.CommandParameter;

            ViewModel.CategoriaSelecionada = categoria;

            ViewModel.ExcluirCategoriaCommand.Execute(categoria);

            ViewModel.ListarCategoriasCommand.Execute(null);
        }

        private async void listViewCategorias_ItemSelected(object sender, SelectedItemChangedEventArgs e) => ViewModel.SelecionarCategoriaCommand.Execute(e.SelectedItem);

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e) => ViewModel.PesquisarCategoriaCommand.Execute(null);

    }
}