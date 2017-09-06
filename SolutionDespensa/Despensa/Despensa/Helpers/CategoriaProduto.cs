using Android.Runtime;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Despensa.Helpers
{
    [Preserve(AllMembers = true)]
    public class CategoriaProduto<K, T> : ObservableCollection<T>
    {
        public K Chave { get; private set; }

        public CategoriaProduto(K chave, IEnumerable<T> items)
        {
            Chave = chave;
            foreach (var item in items)
                this.Items.Add(item);
        }

        public CategoriaProduto()
        {
        }
    }
}