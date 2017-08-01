using System.Threading.Tasks;
using Xamarin.Forms;

namespace Despensa.Services
{
    public class PageService : IPageService
    {
        Page PageApplication;

        public PageService()
        {
            PageApplication = Application.Current.MainPage;
        }

        public async Task DisplayAlert(string title, string message, string ok)
        {
            await PageApplication.DisplayAlert(title, message, ok);
        }

        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await PageApplication.DisplayAlert(title, message, ok, cancel);
        }
    }
}