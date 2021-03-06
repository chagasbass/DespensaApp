﻿using Despensa.DataContexts;
using Despensa.Models;
using Despensa.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaDeComprasPage : ContentPage
    {
        private ItemsDeCompraViewModel ViewModel
        {
            get { return BindingContext as ItemsDeCompraViewModel; }
            set { BindingContext = value; }
        }

        public ListaDeComprasPage()
        {
            InitializeComponent();
            BindingContext = new ItemsDeCompraViewModel(new ProdutoRepository());
        }

        protected override void OnAppearing() => ViewModel.ListarComprasCommand.Execute(null);

        private void ToolbarItem_FinalizarCompra(object sender, EventArgs e) => ViewModel.FinalizarCompraCommand.Execute(null);

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e) => ViewModel.PesquisarItemCommand.Execute(null);

        private void listViewItems_Refreshing(object sender, EventArgs e) => listViewItems.EndRefresh();

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var escolha = (MenuItem)sender;
            var item = (Produto)escolha.CommandParameter;

            ViewModel.ItemSelecionado = item;

            ViewModel.ExcluirItemCommand.Execute(null);
        }

        private void listViewItems_ItemSelected(object sender, SelectedItemChangedEventArgs e) => ViewModel.CancelarSelecaoDeItem.Execute(null);

        private void ToolbarItem_NovoItem(object sender, EventArgs e) => ViewModel.RedirecionarParaNovoItemCommand.Execute(null);
    }
}