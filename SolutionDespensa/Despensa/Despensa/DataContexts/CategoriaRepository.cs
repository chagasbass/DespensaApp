﻿using Despensa.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Despensa.DataContexts
{
    public  class CategoriaRepository
    {
        SQLiteAsyncConnection _Connection;

        public CategoriaRepository()
        {
            _Connection = DependencyService.Get<IDataContext>().GetConnection();
        }

        public async void CriarTabelas()
        {
            await _Connection.CreateTableAsync<Categoria>();
        }

        public async void CadastrarCategoriaAsync(Categoria categoria)
        {
            await _Connection.InsertAsync(categoria);
        }

        public async Task<Categoria> RecuperarCategoriaPorIdAsync(int  id)
        {
            var categorias = await _Connection.Table<Categoria>().ToListAsync();

            var categoria = categorias.Find(x => x.Id == id);

            return categoria;
        }

        public async Task<List<Categoria>> RecuperarCategoriasAsync()
        {
            var categorias = await _Connection.Table<Categoria>().ToListAsync();
          
            return categorias;
        }

        public async Task<Categoria> RecuperarCategoriaPorNomeAsync(string nome)
        {
            var categorias = await _Connection.Table<Categoria>().ToListAsync();

            var categoria = categorias.Find(x => x.Nome.ToUpper() == nome.ToUpper());

            return categoria;
        }

        public async Task<Categoria> AtualizarCategoriaAsync(Categoria categoria)
        {
            int retorno = await _Connection.UpdateAsync(categoria);

            if (retorno > 0)
                return categoria;

            return null;
        }

        public async void ExcluirCategoriaAsync(Categoria categoria)
        {
            await _Connection.DeleteAsync(categoria);
        }
    }
}