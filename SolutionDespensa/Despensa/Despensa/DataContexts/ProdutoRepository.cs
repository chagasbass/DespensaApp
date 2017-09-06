using Android.Runtime;
using Despensa.Models;
using SQLite.Net;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Despensa.DataContexts
{
    [Preserve(AllMembers = true)]
    public class ProdutoRepository
    {
        SQLiteConnection _Connection;

        public ProdutoRepository()
        {
            _Connection = DependencyService.Get<IDataContext>().GetConnection();
        }

        public void CadastrarProduto(Produto produto)
        {
            _Connection.Insert(produto);
        }

        /// <summary>
        /// Recupera o produto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Produto RecuperarProdutoPorId(int id)
        {
            var produto = _Connection.Table<Produto>().Where(x => x.Id == id).FirstOrDefault();

            return produto;
        }

        /// <summary>
        /// Recupera o produto por marca
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        public Produto RecuperarProdutoPorMarca(string marca)
        {
            var produto = _Connection.Table<Produto>().Where(x => x.Marca.ToUpper() == marca.ToUpper()).FirstOrDefault();

            return produto;
        }

        /// <summary>
        /// Recupera um produto por nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public Produto RecuperarProdutoPorNome(string nome)
        {
            var produto = _Connection.Table<Produto>().Where(x => x.Marca.ToUpper() == nome.ToUpper()).FirstOrDefault();

            return produto;
        }

        public Produto RecuperarProdutoPorNomeEMarca(string nome, string marca)
        {
            var produto = _Connection.Table<Produto>().Where(x => x.Nome.ToUpper() == nome.ToUpper() && x.Marca.ToUpper() == marca.ToUpper()).FirstOrDefault();

            return produto;
        }

        /// <summary>
        /// Recupera a lista de produtos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Produto> RecuperarProdutos()
        {
            var lista = _Connection.Table<Produto>();
            return lista.OrderBy(c => c.Nome);
        }


        public IEnumerable<Produto> RecuperarProdutosParaCompra()
        {
            var produtos = _Connection.Table<Produto>().Where(x => x.Status == "Acabou" || x.Status == "Acabando");

            foreach (var item in produtos)
                item.Comprado = false;

            return produtos.OrderBy(c=> c.Status);
        }

        public Produto AtualizarProduto(Produto produto)
        {
            int retorno = _Connection.Update(produto);

            if (retorno > 0)
                return produto;

            return null;
        }

        public void AtualizarProdutosEmLote(List<Produto> lote) => _Connection.UpdateAll(lote);

        public void ExcluirProdutoAsync(Produto produto) => _Connection.Delete(produto);
    }
}