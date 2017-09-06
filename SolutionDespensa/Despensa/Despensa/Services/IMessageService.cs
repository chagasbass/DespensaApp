using Android.Runtime;
using System.Threading.Tasks;

namespace Despensa.Services
{
    [Preserve(AllMembers = true)]
    public interface IMessageService
    {
        Task MostrarDialog(string titulo, string mensagem);
        Task<bool> MostrarDialogComBotoes(string titulo, string mensagem);
    }
}