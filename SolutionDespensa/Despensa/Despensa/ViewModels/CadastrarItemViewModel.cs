using Android.Runtime;
using Despensa.DataContexts;
using Despensa.Helpers;
using Despensa.Models;
using Despensa.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    [Preserve(AllMembers = true)]
    public class CadastrarItemViewModel:BaseViewModel
    {
        public ICommand CadastrarNovoProdutoCommand { get; private set; }
        public ICommand SelecionarCategoriaCommand { get; private set; }

        readonly ProdutoRepository _ProdutoRepository;
        readonly CategoriaRepository _CategoriaRepository;
        readonly INavigationService _Navigation;
        readonly IMessageService _MessageService;

        Produto _NovoProduto;
        public ObservableCollection<Categoria> Categorias { get; private set; } = new ObservableCollection<Categoria>();
        List<string> _Status;
        List<string> _Medidas;
        Categoria _CategoriaSelecionada;
        string _Erros;
        string _StatusSelecionado;
        string _MedidaSelecionada;

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

        public string StatusSelecionado
        {
            get { return _StatusSelecionado; }
            set
            {
                SetValue(ref _StatusSelecionado, value);
                OnPropertyChanged(nameof(_StatusSelecionado));
            }
        }

        public string MedidaSelecionada
        {
            get { return _MedidaSelecionada; }
            set
            {
                SetValue(ref _MedidaSelecionada, value);
                OnPropertyChanged(nameof(_MedidaSelecionada));
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

        public CadastrarItemViewModel(ProdutoRepository ProdutoRepository, CategoriaRepository CategoriaRepository)
        {
            _Navigation = DependencyService.Get<INavigationService>();
            _MessageService = DependencyService.Get<IMessageService>();
            _ProdutoRepository = ProdutoRepository;
            _CategoriaRepository = CategoriaRepository;

            CadastrarNovoProdutoCommand = new Command(CadastrarProduto);
            //SelecionarMedidaCommand = new Command<string>(async vm => await SelecionarMedida(vm));
            //SelecionarStatusCommand = new Command<string>(async vm => await SelecionarStatus(vm));

            InicializarListas();
        }

        private void InicializarListas()
        {
            ListarCategorias();
            Medidas = MedidaHelper.RetornarMedidas();
            Status = StatusHelper.RecuperarStatus();
            StatusSelecionado = Status[0];
            MedidaSelecionada = Medidas[0];
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

                await _MessageService.MostrarDialog("Atenção", Erros);

                Erros = string.Empty;

                return;
            }

            NovoProduto.FormatarCamposDeItem();

            var produtoEncontrado = _ProdutoRepository.RecuperarProdutoPorNomeEMarca(NovoProduto.Nome, NovoProduto.Marca);

            if (produtoEncontrado != null)
            {
                await _MessageService.MostrarDialog("Atenção", "Item já cadastrado");
                return;
            }

            _ProdutoRepository.CadastrarProduto(NovoProduto);

            await _MessageService.MostrarDialog("Despensa", "Item criado com sucesso");
        }

        //private async Task SelecionarMedida(string medida) => NovoProduto.Medida = medida;

        //private async Task SelecionarStatus(string status) => NovoProduto.Status = status;
    }
}
