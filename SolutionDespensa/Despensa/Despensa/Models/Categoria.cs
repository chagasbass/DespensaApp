using Android.Runtime;
using Despensa.ViewModels;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;

namespace Despensa.Models
{
    [Preserve(AllMembers = true)]
    public class Categoria : BaseViewModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50), NotNull]
        public string Nome { get; set; }
        [MaxLength(100)]
        public string Informacao { get; set; }
        [MaxLength(20), NotNull]
        public string DataCadastro { get; set; }
        [NotNull]
        public bool Original { get; set; }
        [MaxLength(50), NotNull]
        public string Imagem { get; set; }

        public Categoria()
        {
            DataCadastro = DateTime.Now.ToString();
            Imagem = "categorias.png";
            Original = false;
        }

        public List<string> ValidarCategoria()
        {
            var notificacoes = new List<string>();

            if (string.IsNullOrEmpty(Nome))
                notificacoes.Add("Nome Inválido");
            if (string.IsNullOrEmpty(Informacao))
                notificacoes.Add("Informações não preenchidas");

            return notificacoes;
        }

        public void FormatarCamposDeItem()
        {
            Nome = char.ToUpper(Nome[0]) + Nome.Substring(1);
        }
    }
}