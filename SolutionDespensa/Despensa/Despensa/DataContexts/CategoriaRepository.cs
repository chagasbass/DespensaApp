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
                lista.Add(new Categoria() { Nome = "Bebidas", Informacao = "Água, sucos, refrigerantes, vinhos, cervejas, entre outras.", Original = true });
                lista.Add(new Categoria() { Nome = "Mantimentos de mercearia", Informacao = "Óleos, azeites, mostardas, ketchups, maioneses. Molhos, patês, azeitonas, latinhas de atum e sardinha. Temperos diversos. Manteiga.", Original = true });
                lista.Add(new Categoria() { Nome = "Mantimentos secos", Informacao = "Água Massas, arroz, feijão, farinhas, lentilha, legumes enlatados, sopas de pacote, macarrão instantâneo, amendoins, frutas secas, açúcar, sal.", Original = true });
                lista.Add(new Categoria() { Nome = "Alimentos em conserva ", Informacao = "Carnes", Original = true });
                lista.Add(new Categoria() { Nome = "Doces", Informacao = "Chocolates, biscoitos, creme de leite, leite condensado, gelatinas, adoçantes, geléias.", Original = true });

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
            var categorias =  _Connection.Table<Categoria>();

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