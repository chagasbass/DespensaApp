using Despensa.DataContexts;
using Despensa.Models;
using Despensa.Services;
using Despensa.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarProdutoPage : ContentPage
    {
        readonly ProdutoRepository ProdutoRepo;
        readonly CategoriaRepository CategoriaRepo;

        private CadastrarProdutoViewModel ViewModel
        {
            get { return BindingContext as CadastrarProdutoViewModel; }
            set { BindingContext = value; }
        }

        public CadastrarProdutoPage()
        {
            InitializeComponent();

            ProdutoRepo = new ProdutoRepository();
            CategoriaRepo = new CategoriaRepository();

            BindingContext = new CadastrarProdutoViewModel(ProdutoRepo,CategoriaRepo);
            ViewModel.NovoProduto = new Models.Produto();
        }

        //private void Picker_SelectedIndexChangedCategoria(object sender, EventArgs e)
        //{
        //    var categoria = ViewModel.Categorias[PicCategoria.SelectedIndex];
        //    ViewModel.SelecionarCategoriaCommand.Execute(categoria);
        //}

        private void Picker_SelectedIndexChangedStatus(object sender, EventArgs e)
        {
            var status = ViewModel.Status[PicStatus.SelectedIndex];
            ViewModel.SelecionarStatusCommand.Execute(status);
        }

        private void Picker_SelectedIndexChangedMedidas(object sender, EventArgs e)
        {
            var medida = ViewModel.Medidas[PicMedidas.SelectedIndex];
            ViewModel.SelecionarMedidaCommand.Execute(medida);
        }
    }
}