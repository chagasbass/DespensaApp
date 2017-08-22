
using Despensa.DataContexts;
using Despensa.Models;
using Despensa.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListagemDeProdutosPage : ContentPage
    {
        ProdutoRepository _Repo;

        private ProdutoViewModel ViewModel
        {
            get { return BindingContext as ProdutoViewModel; }
            set { BindingContext = value; }
        }
       
        public ListagemDeProdutosPage()
        {
            InitializeComponent();
            _Repo = new ProdutoRepository();
            BindingContext = new ProdutoViewModel(_Repo);
        }

        protected override async void OnAppearing()
        {
            App.PaginaAtual = this;
            ViewModel.ListarProdutosCommand.Execute(null);
        }

        private async void ToolbarItem_Activated(object sender, EventArgs e)=> ViewModel.NavegarParaNovoProdutoCommand.Execute(null);

        private async void listViewProdutos_Refreshing(object sender, EventArgs e)
        {
            ViewModel.ListarProdutosCommand.Execute(null);
            listViewProdutos.EndRefresh();
        }

        private async void MenuItem_EditarProdutos(object sender, EventArgs e)
        {
            var escolha = (MenuItem)sender;
            var produto = (Produto)escolha.CommandParameter;

            ViewModel.NavegarParaAtualizarProdutoCommand.Execute(produto);
        }

        private async void MenuItem_ExcluirProdutos(object sender, EventArgs e)
        {
            var escolha = (MenuItem)sender;
            var produto = (Produto)escolha.CommandParameter;

            ViewModel.ProdutoSelecionado = produto;

            var response = await DisplayAlert("Atenção", "Deseja excluir o Item?", "SIM", "NÃO");

            if (!response)
                return;

            ViewModel.ExcluirProdutoCommand.Execute(null);

            await DisplayAlert("Atenção", "Item excluído com sucesso", "OK");

            ViewModel.ListarProdutosCommand.Execute(null);
        }

        private async void listViewProdutos_ItemSelected(object sender, SelectedItemChangedEventArgs e) => ViewModel.SelecionarProdutoCommand.Execute(e.SelectedItem);

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.PesquisarProdutoCommand.Execute(null);
        }
    }
}