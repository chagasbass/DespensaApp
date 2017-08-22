
using Despensa.DataContexts;
using Despensa.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Despensa
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDetail { get; set; }
        public static Stack<Page> ListaDePaginas { get; set; } = new Stack<Page>();
        public static Page PaginaAtual { get; set; }
        public static Page UltimaPagina { get; set; }

        public App()
        {
            InitializeComponent();
            RegistrarDependencias();

            MainPage = new NavigationPage(new Despensa.MainPage());
        }

        private void RegistrarDependencias()
        {
            DependencyService.Register<IMessageService, MessageService>();
            DependencyService.Register<INavigationService, NavigationService>();
        }

        public async static Task NavigateMasterDetail(Page page)
        {
            MasterDetail.IsPresented = false;//esconde a barra quando navegar para outra página!
            MasterDetail.Detail = new NavigationPage(page);
        }

        protected override void OnStart()
        {
            DataContextInitializer.InicializarBancoDeDados();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}