using System.Threading.Tasks;

namespace Despensa.Services
{
    public interface IMessageService
    {
        Task MostrarDialog(string titulo, string mensagem);
        Task<bool> MostrarDialogComBotoes(string titulo, string mensagem);
    }
}