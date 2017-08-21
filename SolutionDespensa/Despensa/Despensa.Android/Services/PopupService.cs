
using Android.App;
using Android.Widget;
using Despensa.Services;
using Android.Support.Design.Widget;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using Despensa.Droid.Services;

[assembly: Dependency(typeof(PopupService))]
namespace Despensa.Droid.Services
{
    public class PopupService : IPopupService
    {
        public void MostrarSnackbar(string mensagem)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            Android.Views.View activityRootView = activity.FindViewById(Android.Resource.Id.Content);
            Snackbar.Make(activityRootView, mensagem, Snackbar.LengthLong).Show();
        }

        public void MostrarToast(string mensagem)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            Toast.MakeText(Forms.Context, mensagem, ToastLength.Long).Show();
        }
    }
}