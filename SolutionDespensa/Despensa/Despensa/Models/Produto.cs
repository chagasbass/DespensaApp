using Android.Runtime;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;

namespace Despensa.Models
{
    [Preserve(AllMembers = true)]
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int IdCategoria { get; set; }
        [MaxLength(50), NotNull]
        public string Nome { get; set; }
        [NotNull]
        public string Quantidade { get; set; }
        [MaxLength(50), NotNull]
        public string Medida { get; set; }
        [MaxLength(50), NotNull]
        public string Marca { get; set; }
        [MaxLength(50), NotNull]
        public string DataCadastro { get; set; }
        [Ignore]
        public Categoria Categoria { get; set; }
        [MaxLength(20), NotNull]
        public string Status { get; set; }
        [Ignore]
        public string Detalhes { get; set; }
        [NotNull]
        public bool Comprado { get; set; }
        [Ignore]
        public string CorStatus { get; set; }
        [MaxLength(200), NotNull]
        public string Imagem { get; set; }

        
        public Produto()
        {
            DataCadastro = DateTime.Now.ToString();
            Comprado = true;
            Imagem = "produtos.png";
        }

        public List<string> ValidarProduto()
        {
            var notificacoes = new List<string>();

            if (string.IsNullOrEmpty(Nome))
                notificacoes.Add("Nome Inválido");
            if (int.Parse(Quantidade) < 0)
                notificacoes.Add("Quantidade Inválida");
            if (string.IsNullOrEmpty(Medida))
                notificacoes.Add("Medida Inválida");
            if (string.IsNullOrEmpty(Marca))
                notificacoes.Add("Marca Inválida");
            if (Categoria == null)
                notificacoes.Add("Categoria Inválida");
            if (string.IsNullOrEmpty(Status))
                notificacoes.Add("Status Inválido");

            return notificacoes;
        }

        public void  CriarDetalhes()
        {
            Detalhes = string.Empty;
            Detalhes =  string.Concat(Quantidade, " ", Medida);
        }

        public void TrocarStatus(string status)
        {
            switch (status)
            {
                case "Acabando":
                    CorStatus = "#caa052";
                    break;
                case "Acabou":
                    CorStatus = "Red";
                    break;
                default:
                    break;
            }
        }

        public void FormatarCamposDeItem()
        {
            Marca = char.ToUpper(Marca[0]) + Marca.Substring(1);
            Nome = char.ToUpper(Nome[0]) + Nome.Substring(1);
        }
    }
}