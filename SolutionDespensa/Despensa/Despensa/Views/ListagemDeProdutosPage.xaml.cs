
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
            BindingContext = new ProdutoViewModel(this, _Repo);
            _Repo.CriarTabelas();
        }

        protected override async void OnAppearing()
        {
            ViewModel.ListarProdutosCommand.Execute(null);
        }

        private async void ToolbarItem_Activated(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastrarProdutoPage());
        }

        private async void listViewProdutos_Refreshing(object sender, EventArgs e)
        {
            ViewModel.ListarProdutosCommand.Execute(null);
            listViewProdutos.EndRefresh();
        }

        private async void MenuItem_EditarProdutos(object sender, EventArgs e)
        {
            var escolha = (MenuItem)sender;
            var produto = (Produto)escolha.CommandParameter;

            //ViewModel.NavegarParaAtualizarProdutoCommand(produto);
        }

        private async void MenuItem_ExcluirProdutos(object sender, EventArgs e)
        {
            var escolha = (MenuItem)sender;
            var produto = (Produto)escolha.CommandParameter;

            ViewModel.ProdutoSelecionado = produto;

            var response = await DisplayAlert("Atenção", "Deseja excluir o produto?", "SIM", "NÃO");

            if (!response)
                return;

           //ViewModel.ExcluirContatoCommand.Execute(produto);

            await DisplayAlert("Atenção", "Produto excluído com sucesso", "OK");

            ViewModel.ListarProdutosCommand.Execute(null);
        }

        private async void listViewProdutos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelecionarProdutoCommand.Execute(e.SelectedItem);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}