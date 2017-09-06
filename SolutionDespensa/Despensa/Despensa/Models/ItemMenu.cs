using Android.Runtime;
using Despensa.ViewModels;
using Xamarin.Forms;

namespace Despensa.Models
{
    /// <summary>
    /// Classe que representa o item do menu
    /// </summary>
    /// 
    [Preserve(AllMembers = true)]
    public class ItemMenu : BaseViewModel
    {
        string _Texto;
        string _Icone;
        Page _Page;
        
        public string Texto
        {
            get { return _Texto; }

            set
            {
                SetValue(ref _Texto, value);
                OnPropertyChanged(nameof(_Texto));
            }
        }

        public string Icone
        {
            get { return _Icone; }

            set
            {
                SetValue(ref _Icone, value);
                OnPropertyChanged(nameof(_Icone));
            }
        }

        public Page Page
        {
            get { return _Page; }

            set
            {
                SetValue(ref _Page, value);
                OnPropertyChanged(nameof(_Page));
            }
        }
    }
}
