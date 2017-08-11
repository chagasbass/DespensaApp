using Despensa.DataContexts;
using Despensa.Services;
using Despensa.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            
            BindingContext = new MainViewModel(this.Navigation);
        }

        protected override bool OnBackButtonPressed()
        {
            if (Device.OS == TargetPlatform.Android)
                DependencyService.Get<IAndroidMethods>();

            return base.OnBackButtonPressed();
        }
    }
}
