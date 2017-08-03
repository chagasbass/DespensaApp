using Despensa.DataContexts;
using Despensa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Despensa.ViewModels
{
    public class AtualizarCategoriaViewModel:BaseViewModel
    {
        public ICommand AtualizarCategoriaCommand { get; private set; }

        readonly Page _Page;
        readonly CategoriaRepository _CategoriaRepository;

        private Categoria _CategoriaAtualizada;

        public Categoria CategoriaAtualizada
        {
            get { return _CategoriaAtualizada; }
            set { SetValue(ref _CategoriaAtualizada, value); }
        }

        public AtualizarCategoriaViewModel(Page Page, CategoriaRepository CategoriaRepository)
        {
            _Page = Page;
            _CategoriaRepository = CategoriaRepository;

            AtualizarCategoriaCommand = new Command(AtualizarContato);
        }

        private async void AtualizarContato()
        {
            await _CategoriaRepository.AtualizarCategoriaAsync(CategoriaAtualizada);

            await _Page.DisplayAlert("Atenção", "Categoria atualizada com sucesso", "OK");

            await _Page.Navigation.PopAsync();
        }
    }
}
