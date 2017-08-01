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

        public Models.ItemMenu ItemSelecionado
        {
            get { return _ItemSelecionado; }
            set { SetValue(ref _ItemSelecionado, value); }
        }

        public MenuItemViewModel()
        {
            //SelecionarItemCommand = new Command<Models.MenuItem>(async vm => await SelecionarItem(vm));
            SelecionarItemCommand = new Command(SelecionarItem);
            CriarListaDeMenu();
        }

        private async void SelecionarItem()
        {
            await App.NavigateMasterDetail(ItemSelecionado.Page);
        }

        private void CriarListaDeMenu()
        {
            ListaMenu.Add(new Models.ItemMenu() { Texto = "Produtos", Icone = "ic_menuLateral.png", Page = new ListagemDeProdutosPage() });
            ListaMenu.Add(new Models.ItemMenu() { Texto = "Categorias", Icone = "ic_menuLateral.png", Page = new ListagemDeCategoriasPage() });
        }
    }
}