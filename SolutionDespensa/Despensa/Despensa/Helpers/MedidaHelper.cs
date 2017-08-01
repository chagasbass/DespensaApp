using System.Collections.Generic;

namespace Despensa.Helpers
{
    /// <summary>
    ///Classe helper que retorna uma lista de string para preenchimento da medidas
    /// </summary>
    public static class MedidaHelper
    {
        public static List<string> RetornarMedidas()
        {
            return  new List<string>()
            {
                "Kg","unidade","Litro(s)","Pacote(s)"
            };
        }
    }
}