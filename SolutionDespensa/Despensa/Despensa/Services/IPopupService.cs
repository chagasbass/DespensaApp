using Android.Runtime;

namespace Despensa.Services
{
    [Preserve(AllMembers = true)]
    public interface IPopupService
    {
        void MostrarToast(string mensagem);
        void MostrarSnackbar(string mensagem);
    }
}
