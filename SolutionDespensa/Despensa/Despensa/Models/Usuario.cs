using Despensa.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;

namespace Despensa.Models
{
    public class Usuario : BaseViewModel
    {
        #region Propriedades Privadas

        private string _Nome;
        private string _Sobrenome;
        private string _Email;
        private string _Senha;
        private string _StatusUsuario;
        private string _DataCadastro;
        #endregion

        public Usuario()
        {
            DataCadastro = DateTime.Now.ToString();
            StatusUsuario = "A";
        }

        #region Propriedades Públicas

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

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

        [MaxLength(50),NotNull]
        public string Sobrenome
        {
            get { return _Sobrenome; }

            set
            {
                SetValue(ref _Sobrenome, value);
                OnPropertyChanged(nameof(_Nome));
            }
        }

        [MaxLength(50),NotNull]
        public string Email
        {
            get { return _Email; }
            set
            {
                SetValue(ref _Email, value);
                OnPropertyChanged(nameof(_Email));
            }
        }

        [MaxLength(10),NotNull]
        public string Senha
        {
            get { return _Senha; }
            set
            {
                SetValue(ref _Senha, value);
                OnPropertyChanged(nameof(_Email));
            }
        }

        [MaxLength(1), NotNull]
        public string StatusUsuario
        {
            get { return _StatusUsuario; }
            set
            {
                SetValue(ref _StatusUsuario, value);
                OnPropertyChanged(nameof(_StatusUsuario));
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

        #endregion

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

            return notificacoes;
        }
    }
}