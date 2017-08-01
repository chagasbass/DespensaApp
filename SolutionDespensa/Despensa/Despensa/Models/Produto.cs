using Despensa.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;

namespace Despensa.Models
{
    public class Produto:BaseViewModel
    {
        private string _Nome;
        private string _Quantidade;
        private string _Medida;
        private string _Marca;
        private string _DataCadastro;
        private Categoria _Categoria;
        private string _Status;
        
        public Produto()
        {
            DataCadastro = DateTime.Now.ToString();
        }

        #region Propriedades

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int IdCategoria { get; set; }

        [MaxLength(50), NotNull]
        public string Nome
        {
            get { return _Nome; }

            set
            {
                SetValue(ref _Nome, value);
                OnPropertyChanged(nameof(_Nome));
            }
        }

        [MaxLength(50), NotNull]
        public string Quantidade
        {
            get { return _Quantidade; }

            set
            {
                SetValue(ref _Quantidade, value);
                OnPropertyChanged(nameof(_Quantidade));
            }
        }

        [MaxLength(50), NotNull]
        public string Medida
        {
            get { return _Medida; }

            set
            {
                SetValue(ref _Medida, value);
                OnPropertyChanged(nameof(_Medida));
            }
        }

        [MaxLength(50), NotNull]
        public string Marca
        {
            get { return _Marca; }

            set
            {
                SetValue(ref _Marca, value);
                OnPropertyChanged(nameof(_Marca));
            }
        }

        [MaxLength(50), NotNull]
        public string DataCadastro
        {
            get { return _DataCadastro; }

            set
            {
                SetValue(ref _DataCadastro, value);
                OnPropertyChanged(nameof(_DataCadastro));
            }
        }
        [Ignore]
        public Categoria Categoria
        {
            get { return _Categoria; }

            set
            {
                SetValue(ref _Categoria, value);
                OnPropertyChanged(nameof(_Categoria));
            }
        }

        [MaxLength(20), NotNull]
        public string Status
        {
            get { return _Status; }

            set
            {
                SetValue(ref _Status, value);
                OnPropertyChanged(nameof(_Status));
            }
        }

        #endregion

        public List<string> ValidarProduto()
        {
            var notificacoes = new List<string>();

            if (string.IsNullOrEmpty(Nome))
                notificacoes.Add("Nome Inválido");
            if (string.IsNullOrEmpty(Quantidade))
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
    }
}