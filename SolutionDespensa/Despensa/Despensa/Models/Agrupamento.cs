using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Despensa.Models
{
    /// <summary>
    /// Classe responsável pelo agrupamento dos produtos por categoria na lista de produtos
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class Agrupamento<K,T>: ObservableCollection<T>
    {
        public K Categoria { get; set; }

        public Agrupamento(K categoria, IEnumerable<T>items)
        {
            Categoria = categoria;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}