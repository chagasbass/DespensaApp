using Despensa.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Despensa.DataContexts
{
    public  class ProdutoRepository
    {
        SQLiteAsyncConnection _Connection;

        public ProdutoRepository()
        {
            _Connection = DependencyService.Get<IDataContext>().GetConnection();
        }

        public async void CriarTabelas()
        {
            await _Connection.CreateTableAsync<Produto>();
        }

        public async void CadastrarProdutoAsync(Produto produto)
        {
            await _Connection.InsertAsync(produto);
        }

        /// <summary>
        /// Recupera o produto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Produto> RecuperarProdutoPorIdAsync(int id)
        {
            var produtos = await _Connection.Table<Produto>().ToListAsync();

            var produto = produtos.Find(x => x.Id == id);
          
            return produto;
        }

        /// <summary>
        /// Recupera o produto por marca
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        public async Task<Produto> RecuperarProdutoPorMarcaAsync(string marca)
        {
            var produtos = await _Connection.Table<Produto>().ToListAsync();

            var produto = produtos.Find(x => x.Marca.ToUpper() == marca.ToUpper());

            return produto;
        }

        /// <summary>
        /// Recupera um produto por nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public async Task<Produto> RecuperarProdutoPorNomeAsync(string nome)
        {
            var produtos = await _Connection.Table<Produto>().ToListAsync();

            var produto = produtos.Find(x => x.Marca.ToUpper() == nome.ToUpper());

            return produto;
        }

        public async Task<Produto> RecuperarProdutoPorNomeEMarcaAsync(string nome,string marca)
        {
            var produtos = await _Connection.Table<Produto>().ToListAsync();

            var produto = produtos.Find(x => x.Nome.ToUpper() == nome.ToUpper() && x.Marca.ToUpper() == marca.ToUpper());

            return produto;
        }

        /// <summary>
        /// Recupera a lista de produtos
        /// </summary>
        /// <returns></returns>
        public async Task<List<Produto>> RecuperarProdutosAsync()
        {
            var produtos = await _Connection.Table<Produto>().ToListAsync();

            return produtos;
        }

        public async Task<Produto> AtualizarProdutoAsync(Produto produto)
        {
            int retorno = await _Connection.UpdateAsync(produto);

            if (retorno > 0)
                return produto;

            return null;
        }

        public async void ExcluirProdutoAsync(Produto produto)
        {
            await _Connection.DeleteAsync(produto);
        }
    }
}