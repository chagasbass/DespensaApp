using Despensa.Helpers.Despensa.Helpers;
using Despensa.Services;
using Despensa.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    /// <summary>
    /// ViewModel responsável pelo Menu
    /// </summary>
    public class MenuItemViewModel : BaseViewModel
    {
        public ICommand SelecionarItemCommand { get; private set; }
        public ICommand ListarItemsCommand { get; private set; }

        public ObservableCollection<Models.ItemMenu> ListaMenu { get; private set; } = new ObservableCollection<Models.ItemMenu>();

        Models.ItemMenu _ItemSelecionado;
        string _MensagemUsuario;
        INavigationService _NavigationService;

        public Models.ItemMenu ItemSelecionado
        {
            get { return _ItemSelecionado; }
            set { SetValue(ref _ItemSelecionado, value); }
        }

        public string MensagemUsuario
        {
            get { return _MensagemUsuario; }
            set { SetValue(ref _MensagemUsuario, value); }
        }

        public MenuItemViewModel()
        {
            _NavigationService = DependencyService.Get<INavigationService>();
            //SelecionarItemCommand = new Command<Models.MenuItem>(async vm => await SelecionarItem(vm));
            SelecionarItemCommand = new Command(SelecionarItem);
            CriarListaDeMenu();
            MensagemUsuario = string.Concat("Seja bem vindo ", PreferenciasHelper.GravarNomeUsuario);
        }

        private async void SelecionarItem()
        {
            _NavigationService.GuardarUltimaPagina(_NavigationService.RetornarPaginaAtual());
            _NavigationService.GuardarPaginaAtual(ItemSelecionado.Page);

            await App.NavigateMasterDetail(ItemSelecionado.Page);
        }

        private void CriarListaDeMenu()
        {
            ListaMenu.Add(new Models.ItemMenu() { Texto = "Produtos", Icone = "ic_restaurant_white_24dp", Page = new ListagemDeProdutosPage() });
            ListaMenu.Add(new Models.ItemMenu() { Texto = "Categorias", Icone = "ic_class_white_24dp", Page = new ListagemDeCategoriasPage() });
            ListaMenu.Add(new Models.ItemMenu() { Texto = "Lista de Compras", Icone = "ic_shopping_cart_white_24dp", Page = new ListaDeComprasPage() });
            ListaMenu.Add(new Models.ItemMenu() { Texto = "Minha Conta", Icone = "ic_account_box_white_24dp.png", Page = new MinhaContaPage() });
            ListaMenu.Add(new Models.ItemMenu() { Texto = "Sobre", Icone = "ic_help_outline_white_24dp.png", Page = new SobrePage() });
            ListaMenu.Add(new Models.ItemMenu() { Texto = "Sair", Icone = "ic_exit_to_app_white_24dp", Page = new LoginPage() });
        }
    }
}