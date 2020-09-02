using System.Collections.Generic;
using System.Data;
using System.Linq;
using OregoFramework.App;
using UnityEngine;

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

        #region InitializeState

        protected sealed override void InitializeState(IEnumerable<TData> dataSet)
        {
            foreach (var data in dataSet)
            {
                var tObject = this.CreateItem(data);
                var id = this.GetItemId(tObject);
                this.objectMap[id] = tObject;
            }
        }

        protected abstract T CreateItem(TData data);

        protected abstract K GetItemId(T item);

        #endregion

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