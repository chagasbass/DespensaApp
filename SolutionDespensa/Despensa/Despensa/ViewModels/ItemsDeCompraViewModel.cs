using Despensa.DataContexts;
using Despensa.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using System.Linq;
using Despensa.Helpers;
using Despensa.Services;
using Android.Runtime;

namespace Despensa.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ItemsDeCompraViewModel : BaseViewModel
    {
        public ICommand ListarComprasCommand { get; private set; }
        public ICommand PesquisarItemCommand { get; private set; }
        public ICommand ExcluirItemCommand { get; private set; }
        public ICommand FinalizarCompraCommand { get; private set; }
        public ICommand CancelarSelecaoDeItem { get; private set; }
        public ICommand RedirecionarParaNovoItemCommand { get; private set; }

        INavigationService _NavigationService;
        IMessageService _MessageService;
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

        public ItemsDeCompraViewModel(ProdutoRepository ProdutoRepository)
        {
            _NavigationService = DependencyService.Get<INavigationService>();
            _MessageService = DependencyService.Get<IMessageService>();
            _ProdutoRepository = ProdutoRepository;
            _CategoriaRepository = new CategoriaRepository();

            ListarComprasCommand = new Command(ListarCompras);
            PesquisarItemCommand = new Command(PesquisarItem);
            ExcluirItemCommand = new Command(ExcluirItem);
            FinalizarCompraCommand = new Command(FinalizarCompras);
            CancelarSelecaoDeItem = new Command(SelecionarItem);
            RedirecionarParaNovoItemCommand = new Command(RedirecionarParaNovoItem);
        }

        private async void RedirecionarParaNovoItem() => await _NavigationService.NavegarParaCadastrarItemDeCompra();

        private async void SelecionarItem() => await _NavigationService.NavegarParaDetalhesDeItemDeCompra(ItemSelecionado);

        private async void FinalizarCompras()
        {
            foreach (var item in ItemsDeCompra)
            {
                item.Comprado = true;
                item.Status = QuantidadeDeItemHelper.Bastante;
            }

            _ProdutoRepository.AtualizarProdutosEmLote(ItemsDeCompra.ToList());

            await _MessageService.MostrarDialog("Despensa", "Sua Compra foi finalizada!");

            await _NavigationService.NavegarParaListarProdutos();
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

                    var categoria = _CategoriaRepository.RecuperarCategoriaPorId(item.IdCategoria);
                    item.Categoria = categoria;
                    item.CriarDetalhes();

                    ItemsDeCompra.Add(item);
                }
              
                if (ItemsDeCompra.Count == 0)
                {
                    await _MessageService.MostrarDialog("Despensa", "Não é necessário fazer compras mo momento");
                    await _NavigationService.NavegarParaListarProdutos();
                }
            }
            catch (Exception ex)
            {
                var e = ex.Message;
            }
        }
    }
}