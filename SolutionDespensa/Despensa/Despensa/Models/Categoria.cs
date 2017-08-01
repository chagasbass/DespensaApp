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

        private string _Nome;
        private string _DataCadastro;

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

        [MaxLength(20)]
        public string DataCadastro
        {
            get { return _DataCadastro; }

            set
            {
                SetValue(ref _DataCadastro, value);
                OnPropertyChanged(nameof(_DataCadastro));
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
    }
}