using Android.Runtime;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Despensa.Models
{
    /// <summary>
    /// Classe responsável pelo agrupamento dos produtos por categoria na lista de produtos
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// 
    [Preserve(AllMembers = true)]
    public class Agrupamento<TKey,TITem>: ObservableCollection<TITem>
    {
        public TKey Categoria { get; set; }

        public Agrupamento(TKey categoria, IEnumerable<TITem>items)
        {
            Categoria = categoria;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}