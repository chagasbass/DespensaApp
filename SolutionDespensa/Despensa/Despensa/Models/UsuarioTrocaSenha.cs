using Android.Runtime;
using System;

namespace Despensa.Models
{
    /// <summary>
    /// Modelo para esquecimento e troca de senha de um usuário
    /// </summary>
    /// 
    [Preserve(AllMembers = true)]
    public class UsuarioTrocaSenha
    {
        public string Email { get; set; }
        public string Codigo { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmacaoDeSenha { get; set; }

        public UsuarioTrocaSenha()
        {

        }
     
        public void GerarCodigo()
        {
            Codigo = Guid.NewGuid().ToString().Substring(0, 6);
        }
    }
}