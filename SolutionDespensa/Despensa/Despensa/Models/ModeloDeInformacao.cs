using Android.Runtime;

namespace Despensa.Models
{
    [Preserve(AllMembers = true)]
    public class ModeloDeInformacao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public ModeloDeInformacao()
        {

        }
    }
}