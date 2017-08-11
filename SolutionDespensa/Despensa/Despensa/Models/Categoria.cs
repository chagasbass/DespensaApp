using Despensa.ViewModels;
using SQLite;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;

namespace Despensa.Models
{
    public class Categoria : BaseViewModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        string _Nome;
        string _DataCadastro;
        string _Informacao;
        bool _Original;
        string _Imagem;

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

        [MaxLength(20), NotNull]
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

        public bool Original
        {
            get { return _Original; }

            set
            {
                SetValue(ref _Original, value);
                OnPropertyChanged(nameof(_Original));
            }
        }

        [MaxLength(50), NotNull]
        public string Imagem
        {
            get { return _Imagem; }

            set
            {
                SetValue(ref _Imagem, value);
                OnPropertyChanged(nameof(_Imagem));
            }
        }

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

            return notificacoes;
        }

        public void FormatarCamposDeItem()
        {
            Nome = char.ToUpper(Nome[0]) + Nome.Substring(1);
        }
    }
}