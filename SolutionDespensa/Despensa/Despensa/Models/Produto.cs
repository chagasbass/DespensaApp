using Despensa.ViewModels;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;

namespace Despensa.Models
{
    public class Produto:BaseViewModel
    {
         string _Nome;
         int _Quantidade;
         string _Medida;
         string _Marca;
         string _DataCadastro;
         Categoria _Categoria;
         string _Status;
         string _Detalhes;
         bool _Comprado;
        string _CorStatus;
        string _Imagem;
        
        public Produto()
        {
            DataCadastro = DateTime.Now.ToString();
            Comprado = true;
            Imagem = "produtos.png";
        }

        #region Propriedades

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
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

        [NotNull]
        public int Quantidade
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

        [Ignore]
        public string Detalhes
        {
            get { return _Detalhes; }

            set
            {
                SetValue(ref _Detalhes, value);
                OnPropertyChanged(nameof(_Detalhes));
            }
        }

        [NotNull]
        public bool Comprado
        {
            get { return _Comprado; }

            set
            {
                SetValue(ref _Comprado, value);
                OnPropertyChanged(nameof(_Comprado));
            }
        }

        [Ignore]
        public string CorStatus
        {
            get { return _CorStatus; }

            set
            {
                SetValue(ref _CorStatus, value);
                OnPropertyChanged(nameof(_CorStatus));
            }
        }

        [MaxLength(200), NotNull]
        public string Imagem
        {
            get { return _Imagem; }

            set
            {
                SetValue(ref _Imagem, value);
                OnPropertyChanged(nameof(_Imagem));
            }
        }

        #endregion

        public List<string> ValidarProduto()
        {
            var notificacoes = new List<string>();

            if (string.IsNullOrEmpty(Nome))
                notificacoes.Add("Nome Inválido");
            if (Quantidade < 0)
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