using Despensa.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;

namespace Despensa.Models
{
    public  class Categoria: BaseViewModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

         string _Nome;
         string _DataCadastro;
        string _Informacao;

        [MaxLength(50),NotNull]
        public string Nome
        {
            get { return _Nome; }

            set
            {
                SetValue(ref _Nome, value);
                OnPropertyChanged(nameof(_Nome));
            }
        }

        [MaxLength(20),NotNull]
        public string DataCadastro
        {
            get { return _DataCadastro; }

            set
            {
                SetValue(ref _DataCadastro, value);
                OnPropertyChanged(nameof(_DataCadastro));
            }
        }

        [MaxLength(100)]
        public string Informacao
        {
            get { return _Informacao; }

            set
            {
                SetValue(ref _Informacao, value);
                OnPropertyChanged(nameof(_Informacao));
            }
        }

        public Categoria()
        {
            DataCadastro = DateTime.Now.ToString();
        }

        public List<string> ValidarCategoria()
        {
            var notificacoes = new List<string>();

            if (string.IsNullOrEmpty(Nome))
                notificacoes.Add("Nome Inválido");

            return notificacoes;
        }

        public void FormatarCamposDeItem()
        {
            Nome = char.ToUpper(Nome[0]) + Nome.Substring(1);
        }
    }
}