﻿using Despensa.DataContexts;
using Despensa.Services;
using Despensa.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Despensa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarCategoriaPage : ContentPage
    {
        readonly CategoriaRepository CategoriaRepo;
        readonly IPageService PageService;

        private CadastrarCategoriaViewModel ViewModel
        {
            get { return BindingContext as CadastrarCategoriaViewModel; }
            set { BindingContext = value; }
        }

        public CadastrarCategoriaPage()
        {
            try
            {
                InitializeComponent();

                CategoriaRepo = new CategoriaRepository();
                PageService = new PageService();

                BindingContext = new CadastrarCategoriaViewModel(CategoriaRepo,this.Navigation, PageService);
                ViewModel.NovaCategoria = new Models.Categoria();
            }
            catch (System.Exception ex)
            {
                var e = ex.Message;
            }
        }
    }
}