using Despensa.ViewModels;
using System;

namespace Despensa.Models
{
    /// <summary>
    /// Modelo para esquecimento e troca de senha de um usuário
    /// </summary>
    public class UsuarioTrocaSenha:BaseViewModel
    {
        string _Email;
        string _Codigo;
        string _NovaSenha;
        string _ConfirmacaoDeSenha;

        public UsuarioTrocaSenha()
        {

        }

        public string Email
        {
            get { return _Email; }
            set
            {
                SetValue(ref _Email, value);
                OnPropertyChanged(nameof(_Email));
            }
        }

        public string Codigo
        {
            get { return _Codigo; }
            set
            {
                SetValue(ref _Codigo, value);
                OnPropertyChanged(nameof(_Codigo));
            }
        }

        public string NovaSenha
        {
            get { return _NovaSenha; }
            set
            {
                SetValue(ref _NovaSenha, value);
                OnPropertyChanged(nameof(_NovaSenha));
            }
        }

        public string ConfirmacaoDeSenha
        {
            get { return _ConfirmacaoDeSenha; }
            set
            {
                SetValue(ref _ConfirmacaoDeSenha, value);
                OnPropertyChanged(nameof(_ConfirmacaoDeSenha));
            }
        }

        public void GerarCodigo()
        {
            Codigo = Guid.NewGuid().ToString().Substring(0, 6);
        }


    }
}
