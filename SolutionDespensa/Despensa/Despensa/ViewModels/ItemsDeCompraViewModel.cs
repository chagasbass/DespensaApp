using Despensa.DataContexts;
using Despensa.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using System.Linq;
using Despensa.Views;
using Despensa.Helpers;

namespace Despensa.ViewModels
{
    public class ItemsDeCompraViewModel : BaseViewModel
    {
        public ICommand ListarComprasCommand { get; private set; }
        public ICommand PesquisarItemCommand { get; private set; }
        public ICommand ExcluirItemCommand { get; private set; }
        public ICommand FinalizarCompraCommand { get; private set; }
        public ICommand CancelarSelecaoDeItem { get; private set; }
        public ICommand RedirecionarParaNovoItemCommand { get; private set; }

        Page _Page;
        ProdutoRepository _ProdutoRepository;
        CategoriaRepository _CategoriaRepository;

        Produto _ItemSelecionado;
        string _Pesquisa;

        #region Propriedades
        public Produto ItemSelecionado
        {
            get { return _ItemSelecionado; }
            set { SetValue(ref _ItemSelecionado, value); }
        }

        public string Pesquisa
        {
            get { return _Pesquisa; }
            set { SetValue(ref _Pesquisa, value); }
        }

       

        #endregion

        public ObservableCollection<Produto> ItemsDeCompra { get; private set; } = new ObservableCollection<Produto>();

        public ItemsDeCompraViewModel(Page Page, ProdutoRepository ProdutoRepository)
        {
            _Page = Page;
            _ProdutoRepository = ProdutoRepository;
            _CategoriaRepository = new CategoriaRepository();

            ListarComprasCommand = new Command(ListarCompras);
            PesquisarItemCommand = new Command(PesquisarItem);
            ExcluirItemCommand = new Command(ExcluirItem);
            FinalizarCompraCommand = new Command(FinalizarCompras);
            CancelarSelecaoDeItem = new Command(CancelarSelecao);
            RedirecionarParaNovoItemCommand = new Command(RedirecionarParaNovoItem);
        }

        private async void RedirecionarParaNovoItem() => await _Page.Navigation.PushAsync(new CadastrarProdutoPage());

        private void CancelarSelecao(object obj) => ItemSelecionado = null;

        private async void FinalizarCompras()
        {
            foreach (var item in ItemsDeCompra)
            {
                item.Comprado = true;
                item.Status = QuantidadeDeItemHelper.Bastante;
            }

            _ProdutoRepository.AtualizarProdutosEmLote(ItemsDeCompra.ToList());

            await _Page.DisplayAlert("Despensa", "Sua Compra foi finalizada!", "OK");

            await _Page.Navigation.PushAsync(new ListagemDeProdutosPage());
        }

        private void ExcluirItem()=> ItemsDeCompra.Remove(ItemSelecionado);

        private void PesquisarItem()
        {
            if (String.IsNullOrEmpty(Pesquisa))
            {
                ListarCompras();
                return;
            }

            var pesquisa = ItemsDeCompra.Where(x => x.Nome.ToUpper().Contains(Pesquisa.ToUpper())).ToList();

            ItemsDeCompra.Clear();

            foreach (var item in pesquisa)
            {
                ItemsDeCompra.Add(item);
            }
        }

        private async void ListarCompras()
        {
            try
            {
                ItemsDeCompra.Clear();

                var items = _ProdutoRepository.RecuperarProdutosParaCompra();

                if (items == null)
                    return;

                foreach (var item in items)
                {
                    item.Comprado = false;

                    if (item.Status.Equals("Acabando"))
                        item.CorStatus = "#caa052";
                    else
                        item.CorStatus = "Red";

                    ItemsDeCompra.Add(item);
                }

                //foreach (var item in items)
                //{
                //    var categoria = _CategoriaRepository.RecuperarCategoriaPorId(item.IdCategoria);
                //    item.Categoria = categoria;
                //    ItemsDeCompra.Add(item);
                //}

                if (ItemsDeCompra.Count == 0)
                {
                    await _Page.DisplayAlert("Despensa", "Não é necessário fazer compras", "OK");
                    await _Page.Navigation.PushAsync(new ListagemDeProdutosPage());
                }
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }
        }
    }
}