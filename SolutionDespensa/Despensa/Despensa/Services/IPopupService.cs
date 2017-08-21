namespace Despensa.Services
{
    public interface IPopupService
    {
        void MostrarToast(string mensagem);
        void MostrarSnackbar(string mensagem);
    }
}
