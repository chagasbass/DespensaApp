using Despensa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despensa.Models
{
    public class ModeloDeInformacao:BaseViewModel
    {
        string _Titulo;
        string _Descricao;

        public ModeloDeInformacao()
        {

        }

        public string Titulo
        {
            get { return _Titulo; }

            set
            {
                SetValue(ref _Titulo, value);
                OnPropertyChanged(nameof(_Titulo));
            }
        }

        public string Descricao
        {
            get { return _Descricao; }

            set
            {
                SetValue(ref _Descricao, value);
                OnPropertyChanged(nameof(_Descricao));
            }
        }

    }
}
