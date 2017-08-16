﻿using Xamarin.Forms;
using Despensa.DataContexts;
using Despensa.Services;
using Despensa.Models;
using System.Collections.Generic;
using System.Windows.Input;
using System.Threading.Tasks;
using Despensa.Helpers;
using System.Collections.ObjectModel;
using Despensa.Views;

namespace Despensa.ViewModels
{
    public class CadastrarProdutoViewModel : BaseViewModel
    {
        public ICommand CadastrarNovoProdutoCommand { get; private set; }
        public ICommand SelecionarCategoriaCommand { get; private set; }
        public ICommand SelecionarMedidaCommand { get; private set; }
        public ICommand SelecionarStatusCommand { get; private set; }

        readonly ProdutoRepository _ProdutoRepository;
        readonly CategoriaRepository _CategoriaRepository;
        readonly INavigation _Navigation;
        readonly IPageService _PageService;

        Produto _NovoProduto;
        public ObservableCollection<Categoria> Categorias { get; private set; } = new ObservableCollection<Categoria>();
        List<string> _Status;
        List<string> _Medidas;
        Categoria _CategoriaSelecionada;
        string _Erros;

        #region Propriedades
        public Produto NovoProduto
        {
            get { return _NovoProduto; }
            set
            {
                SetValue(ref _NovoProduto, value);
                OnPropertyChanged(nameof(_NovoProduto));
            }
        }

        public List<string> Status
        {
            get { return _Status; }
            set
            {
                SetValue(ref _Status, value);
                OnPropertyChanged(nameof(_Status));
            }
        }

        public List<string> Medidas
        {
            get { return _Medidas; }
            set
            {
                SetValue(ref _Medidas, value);
                OnPropertyChanged(nameof(_Medidas));
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

        public Categoria CategoriaSelecionada
        {
            get { return _CategoriaSelecionada; }
            set
            {
                SetValue(ref _CategoriaSelecionada, value);
                OnPropertyChanged(nameof(_CategoriaSelecionada));
            }
        }

        #endregion

        public CadastrarProdutoViewModel(INavigation Navigation, ProdutoRepository ProdutoRepository, CategoriaRepository CategoriaRepository, IPageService PageService)
        {
            _Navigation = Navigation;
            _ProdutoRepository = ProdutoRepository;
            _CategoriaRepository = CategoriaRepository;
            _PageService = PageService;

            CadastrarNovoProdutoCommand = new Command(CadastrarProduto);
            SelecionarMedidaCommand = new Command<string>(async vm => await SelecionarMedida(vm));
            SelecionarStatusCommand = new Command<string>(async vm => await SelecionarStatus(vm));

            InicializarListas();
        }

        private void InicializarListas()
        {
            ListarCategorias();
            Medidas = MedidaHelper.RetornarMedidas();
            Status = StatusHelper.RecuperarStatus();
        }

        private void ListarCategorias()
        {
            var categorias = _CategoriaRepository.RecuperarCategorias();

            foreach (var cat in categorias)
                Categorias.Add(cat);
        }

        private async void CadastrarProduto()
        {
            NovoProduto.CriarDetalhes();
            NovoProduto.Categoria = CategoriaSelecionada;
            NovoProduto.IdCategoria = CategoriaSelecionada.Id;

            var erros = NovoProduto.ValidarProduto();

            if (erros.Count > 0)
            {
                foreach (var item in erros)
                {
                    Erros = string.Concat(Erros, "*", item);
                }

                await _PageService.DisplayAlert("Atenção", Erros, "OK");

                Erros = string.Empty;

                return;
            }

            NovoProduto.FormatarCamposDeItem();

            var produtoEncontrado = _ProdutoRepository.RecuperarProdutoPorNomeEMarca(NovoProduto.Nome, NovoProduto.Marca);

            if (produtoEncontrado != null)
            {
                await _PageService.DisplayAlert("Atenção", "Item já cadastrado", "OK");
                return;
            }

            _ProdutoRepository.CadastrarProduto(NovoProduto);

            await _PageService.DisplayAlert("Despensa", "Item criado com sucesso", "OK");

            if (NovoProduto.Status.Equals("Acabou") || NovoProduto.Status.Equals("Acabando"))
                await _Navigation.PushAsync(new ListaDeComprasPage());

            else
                await _Navigation.PopAsync();
        }

        private async Task SelecionarMedida(string medida)=> NovoProduto.Medida = medida;

        private async Task SelecionarStatus(string status)=> NovoProduto.Status = status;
    }
}