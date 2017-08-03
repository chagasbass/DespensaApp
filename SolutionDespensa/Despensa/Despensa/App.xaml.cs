
using Java.Lang;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Despensa
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDetail { get; set; }
        

        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new Despensa.MainPage());
        }

        public async static Task NavigateMasterDetail(Page page)
        {
            try
            {
                MasterDetail.IsPresented = false;//esconde a barra quando navegar para outra página!
                MasterDetail.Detail = new NavigationPage(page);
            }
            catch (System.Exception ex)
            {
                var m = ex.Message;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
