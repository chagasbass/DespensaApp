using Despensa.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System;

namespace Despensa.ViewModels
{
    public  class SobreViewModel:BaseViewModel
    {
        public ICommand CancelarSelecaoCommand { get; private set; }

        public ObservableCollection<ModeloDeInformacao> Conteudo { get; private set; } = new ObservableCollection<ModeloDeInformacao>();
        INavigation _Navigation;
        ModeloDeInformacao _Selecionado;

        public ModeloDeInformacao Selecionado
        {
            get { return _Selecionado; }
            set { SetValue(ref _Selecionado, value); }
        }

        public SobreViewModel(INavigation Navigation)
        {
            _Navigation = Navigation;
            CancelarSelecaoCommand = new Command(CancelarSelecao);
            InicializarConteudo();
        }

        private void CancelarSelecao() => Selecionado = null;


        private void InicializarConteudo()
        {
            Conteudo.Add(new ModeloDeInformacao() { Titulo = "Versão", Descricao = "1.0" });
            Conteudo.Add(new ModeloDeInformacao() { Titulo = "Criado em", Descricao = "14/08/2017" });
            Conteudo.Add(new ModeloDeInformacao() { Titulo = "Autor", Descricao = "Thiago Chagas" });
            Conteudo.Add(new ModeloDeInformacao() { Titulo = "Termos de Uso", Descricao = "Livre" });
        }
    }
}
