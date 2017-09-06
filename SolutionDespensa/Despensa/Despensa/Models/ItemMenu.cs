using Android.Runtime;
using Xamarin.Forms;

namespace Despensa.Models
{
    /// <summary>
    /// Classe que representa o item do menu
    /// </summary>
    /// 
    [Preserve(AllMembers = true)]
    public class ItemMenu
    {
        public string Texto { get; set; }
        public string Icone { get; set; }
        public Page Pagina { get; set; }

        public ItemMenu()
        {

        }
    }
}