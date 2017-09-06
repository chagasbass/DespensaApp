using Android.Runtime;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;

namespace Despensa.Models
{
    [Preserve(AllMembers = true)]
    public class Usuario
    {
        #region Propriedades Privadas

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50), NotNull]
        public string Nome { get; set; }
        [MaxLength(50), NotNull]
        public string Sobrenome { get; set; }
        [MaxLength(50), NotNull]
        public string Email { get; set; }
        [MaxLength(10), NotNull]
        public string Senha { get; set; }
        [MaxLength(1), NotNull]
        public string StatusUsuario { get; set; }
        [MaxLength(20)]
        public string DataCadastro { get; set; }
        #endregion

        public Usuario()
        {
            DataCadastro = DateTime.Now.ToString();
            StatusUsuario = "A";
        }

        public List<string> ValidarUsuario()
        {
            var notificacoes = new List<string>();

            if (string.IsNullOrEmpty(Nome))
                notificacoes.Add("Nome Inválido");
            if (string.IsNullOrEmpty(Sobrenome))
                notificacoes.Add("Sobrenome Inválido");
            if (string.IsNullOrEmpty(Email))
                notificacoes.Add("Email Inválido");
            if (string.IsNullOrEmpty(Senha))
                notificacoes.Add("Senha Inválida");
            if (Senha.Length > 10)
                notificacoes.Add("Máximo de 10 caractéres para Senha");

            return notificacoes;
        }
    }
}