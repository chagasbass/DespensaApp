using Despensa.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SobrePage : ContentPage
    {
        private SobreViewModel ViewModel
        {
            get { return BindingContext as SobreViewModel; }
            set { BindingContext = value; }
        }

        public SobrePage()
        {
            InitializeComponent();
            ViewModel = new SobreViewModel();
            BindingContext = ViewModel;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e) => ViewModel.CancelarSelecaoCommand.Execute(null);
    }
}