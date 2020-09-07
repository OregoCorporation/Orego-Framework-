using System.Collections.Generic;
using System.Linq;

namespace OregoFramework.App
{
    public abstract class ItemMapInteractor<K, T, TRepository, TData> :
        ItemInteractor<T, TRepository, IEnumerable<TData>>,
        IItemMapInteractor<K, T>
        where TRepository : IReadyRepository<IEnumerable<TData>>
    {
        protected readonly Dictionary<K, T> objectMap;

        protected ItemMapInteractor()
        {
            this.objectMap = new Dictionary<K, T>();
        }

        protected sealed override void Initialize(IEnumerable<TData> dataSet)
        {
            foreach (var data in dataSet)
            {
                var item = this.CreateItem(data);
                var id = this.GetItemId(item);
                this.objectMap[id] = item;
            }
        }

        protected abstract T CreateItem(TData data);

        protected abstract K GetItemId(T item);

        public T GetItem(K key)
        {
            return this.objectMap[key];
        }

        public IEnumerable<T> GetItems()
        {
            return this.objectMap.Values.ToList();
        }

        public int GetItemCount()
        {
            return this.objectMap.Count;
        }
    }
}