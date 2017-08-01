using Despensa.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuHamburguerPage : ContentPage
    {
        private MenuItemViewModel ViewModel
        {
            get { return BindingContext as MenuItemViewModel; }
            set { BindingContext = value; }
        }

        public MenuHamburguerPage()
        {
            InitializeComponent();
            BindingContext = new MenuItemViewModel();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (ViewModel.ItemSelecionado == null)
                    return;

                ViewModel.SelecionarItemCommand.Execute(null);

                ViewModel.ItemSelecionado = null;
            }
            catch (System.Exception ex)
            {
                var E = ex.Message;
            }
        }
    }
}