using Android.Runtime;
using Despensa.DataContexts;
using Despensa.Helpers;
using Despensa.Models;
using Despensa.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    [Preserve(AllMembers = true)]
    public class AtualizarProdutoViewModel : BaseViewModel
    {
        public ICommand AtualizarProdutoCommand { get; private set; }

        readonly INavigationService _NavigationService;
        readonly IPopupService _PopupService;
        readonly IMessageService _MessageService;
        readonly ProdutoRepository _ProdutoRepository;
        readonly CategoriaRepository _CategoriaRepository;

        Produto _ProdutoAtualizado;
        string _CategoriaSelecionada;
        public ObservableCollection<Categoria> CategoriasDeItem { get; private set; } = new ObservableCollection<Categoria>();
        List<string> _Status;
        List<string> _Medidas;
        List<string> _Categorias;
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

        public string CategoriaSelecionada
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

        public List<string> Categorias
        {
            get { return _Categorias; }
            set
            {
                SetValue(ref _Categorias, value);
                OnPropertyChanged(nameof(_Categorias));
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

        public AtualizarProdutoViewModel(ProdutoRepository ProdutoRepository, CategoriaRepository CategoriaRepository, Produto produto)
        {
            _NavigationService = DependencyService.Get<INavigationService>();
            _PopupService = DependencyService.Get<IPopupService>();
            _MessageService = DependencyService.Get<IMessageService>();

            _ProdutoRepository = ProdutoRepository;
            _CategoriaRepository = CategoriaRepository;
            ProdutoAtualizado = produto;
            CategoriaSelecionada = produto.Categoria.Nome;
            InicializarListas();

            AtualizarProdutoCommand = new Command(AtualizarContato);
        }

        private async void AtualizarContato()
        {
            var cat = (from c in CategoriasDeItem
                       where c.Nome == CategoriaSelecionada
                       select c).FirstOrDefault();

            ProdutoAtualizado.Categoria = cat;

            ProdutoAtualizado.IdCategoria = ProdutoAtualizado.Categoria.Id;
            ProdutoAtualizado.CriarDetalhes();

            var erros = ProdutoAtualizado.ValidarProduto();

            if (erros.Count > 0)
            {
                foreach (var item in erros)
                {
                    Erros = string.Concat(Erros, "*", item);
                }

                await _MessageService.MostrarDialog("Atenção", Erros);

                Erros = string.Empty;

                return;
            }

            _ProdutoRepository.AtualizarProduto(ProdutoAtualizado);

            _PopupService.MostrarSnackbar("Item atualizado com sucesso");

            VerificarStatusDeItem();

            await _NavigationService.NavegarParaListarProdutos();
        }

        private void VerificarStatusDeItem()
        {
            switch (ProdutoAtualizado.Status)
            {
                case "Acabou":
                    _PopupService.MostrarSnackbar(string.Concat("Verifique a sua Despensa, existe um item que acabou"));
                    _NavigationService.NavegarParaListarProdutos();
                    break;
                case "Acabando":
                    _PopupService.MostrarSnackbar(string.Concat("Existe um item que está acabando na Despensa"));
                    _NavigationService.NavegarParaListarProdutos();
                    break;
                default:
                    break;
            }
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
            Categorias = new List<string>();

            foreach (var cat in categorias)
            {
                CategoriasDeItem.Add(cat);
                Categorias.Add(cat.Nome);
            }
        }
    }
}