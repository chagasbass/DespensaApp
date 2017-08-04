using Despensa.DataContexts;
using Despensa.Helpers;
using Despensa.Models;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    public class AtualizarProdutoViewModel:BaseViewModel
    {
        public ICommand AtualizarProdutoCommand { get; private set; }

        readonly Page _Page;
        readonly ProdutoRepository _ProdutoRepository;
        readonly CategoriaRepository _CategoriaRepository;

        Produto _ProdutoAtualizado;
        Categoria _CategoriaSelecionada;
        public ObservableCollection<Categoria> CategoriasDeItem { get; private set; } = new ObservableCollection<Categoria>();
        List<string> _Status;
        List<string> _Medidas;
        string _StatusSelecionado;
        string _MedidaSelecionada;
        string _Erros;
        int _Posicao;

        #region Proprieadades

        public Produto ProdutoAtualizado
        {
            get { return _ProdutoAtualizado; }
            set { SetValue(ref _ProdutoAtualizado, value); }
        }

        public Categoria CategoriaSelecionada
        {
            get { return _CategoriaSelecionada; }
            set { SetValue(ref _CategoriaSelecionada, value); }
        }

        public string StatusSelecionado
        {
            get { return _StatusSelecionado; }
            set { SetValue(ref _StatusSelecionado, value); }
        }

        public string MedidaSelecionada
        {
            get { return _MedidaSelecionada; }
            set { SetValue(ref _MedidaSelecionada, value); }
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

        public int Posicao
        {
            get { return _Posicao; }
            set
            {
                SetValue(ref _Posicao, value);
                OnPropertyChanged(nameof(_Posicao));
            }
        }

        #endregion

        public AtualizarProdutoViewModel(Page Page, ProdutoRepository ProdutoRepository,CategoriaRepository CategoriaRepository)
        {
            _Page = Page;
            _ProdutoRepository = ProdutoRepository;
            _CategoriaRepository = CategoriaRepository;

            AtualizarProdutoCommand = new Command(AtualizarContato);

            InicializarListas();
        }

        private async void AtualizarContato()
        {

            ProdutoAtualizado.Categoria = CategoriaSelecionada;
            ProdutoAtualizado.IdCategoria = CategoriaSelecionada.Id;
            ProdutoAtualizado.Medida = MedidaSelecionada;
            ProdutoAtualizado.Status = StatusSelecionado;

            ProdutoAtualizado.CriarDetalhes();

            var erros = ProdutoAtualizado.ValidarProduto();

            if (erros.Count > 0)
            {
                foreach (var item in erros)
                {
                    Erros = string.Concat(Erros, "*", item);
                }

                await _Page.DisplayAlert("Atenção", Erros, "OK");

                Erros = string.Empty;

                return;
            }

            await _ProdutoRepository.AtualizarProdutoAsync(ProdutoAtualizado);

            await _Page.DisplayAlert("Atenção", "Item atualizado com sucesso", "OK");

            VerificarStatusDeItem();

            await _Page.Navigation.PopAsync();
        }

        private void VerificarStatusDeItem()
        {
            switch (ProdutoAtualizado.Status)
            {
                case "Acabou":
                    CrossLocalNotifications.Current.Show("Despensa", string.Concat("Verifique a sua Despensa, existe um item que acabou"), 101, DateTime.Now.AddSeconds(2));
                    //CrossLocalNotifications.Current.Cancel(101);
                    break;
                case "Acabando":
                    CrossLocalNotifications.Current.Show("Despensa", string.Concat("Existe um item que está acabando na Despensa"), 101, DateTime.Now);
                    //CrossLocalNotifications.Current.Cancel(101);
                    break;
                default:
                    break;
            }
        }

        private async void InicializarListas()
        {
            ListarCategorias();
            Medidas = MedidaHelper.RetornarMedidas();
            Status = StatusHelper.RecuperarStatus();
        }

        private async void ListarCategorias()
        {
            var categorias = await _CategoriaRepository.RecuperarCategoriasAsync();

            foreach (var cat in categorias)
                CategoriasDeItem.Add(cat);

            Posicao = CategoriasDeItem.IndexOf(CategoriaSelecionada);
        }
    }
}