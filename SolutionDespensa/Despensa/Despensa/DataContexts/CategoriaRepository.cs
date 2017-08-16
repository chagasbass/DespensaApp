using Despensa.Models;
using SQLite.Net;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Despensa.DataContexts
{
    public class CategoriaRepository
    {
        SQLiteConnection _Connection;

        public CategoriaRepository()
        {
            _Connection = DependencyService.Get<IDataContext>().GetConnection();
        }

        public void InicializarCategorias()
        {
            var listaDeOriginais = _Connection.Table<Categoria>().Where(x => x.Original == true);

            if (listaDeOriginais.Count() == 0)
            {
                var lista = new List<Categoria>();
                lista.Add(new Categoria() { Nome = "Bebidas", Informacao = "Água, sucos, refrigerantes, vinhos, cervejas, entre outras.", Original = true,Imagem="bebidas.png" });
                lista.Add(new Categoria() { Nome = "Enlatados", Informacao = "Óleos,azeites,milho, ervilha,mostardas, ketchups, maioneses. Molhos, patês, azeitonas, latinhas de atum e sardinha.", Original = true ,Imagem="enlatados.png"});
                lista.Add(new Categoria() { Nome = "Mantimentos secos", Informacao = "Água Massas, arroz, feijão, farinhas, lentilha, legumes enlatados, sopas de pacote, macarrão instantâneo, amendoins, frutas secas, açúcar, sal.", Original = true, Imagem="mantimentos.png" });
                lista.Add(new Categoria() { Nome = "Carnes", Informacao = "Frango, Carne bovina, Carne de Porco, Peixes", Original = true, Imagem = "carnes.png" });
                lista.Add(new Categoria() { Nome = "Leite e Derivados", Informacao = "Chocolates, biscoitos, creme de leite, leite condensado, gelatinas, adoçantes, geléias.", Original = true,Imagem= "leiteDerivados.png" });
                lista.Add(new Categoria() { Nome = "Verduras", Informacao = "Verduras e Legumes", Original = true ,Imagem="verduras.png"});
                lista.Add(new Categoria() { Nome = "Frutas", Informacao = "Frutas", Original = true, Imagem = "frutas.png" });
                lista.Add(new Categoria() { Nome = "Temperos", Informacao = "Temperos e especiarias", Original = true, Imagem = "temperos.png" });

                foreach (var item in lista)
                    _Connection.Insert(item);
            }
        }

        public void CadastrarCategoria(Categoria categoria)
        {
            _Connection.Insert(categoria);
        }

        public Categoria RecuperarCategoriaPorId(int id)
        {
            var categoria = _Connection.Table<Categoria>().Where(x => x.Id == id).FirstOrDefault();

            return categoria;
        }

        public IEnumerable<Categoria> RecuperarCategorias()
        {
            var categorias =  _Connection.Table<Categoria>().OrderBy(x=> x.Nome);

            return categorias;
        }

        public Categoria RecuperarCategoriaPorNome(string nome)
        {
            var categoria = _Connection.Table<Categoria>().Where(x => x.Nome.ToUpper() == nome.ToUpper()).FirstOrDefault();

            return categoria;
        }

        public Categoria AtualizarCategoria(Categoria categoria)
        {
            int retorno =  _Connection.Update(categoria);

            if (retorno > 0)
                return categoria;

            return null;
        }

        public  void ExcluirCategoria(Categoria categoria)
        {
           _Connection.Delete(categoria);
        }
    }
}