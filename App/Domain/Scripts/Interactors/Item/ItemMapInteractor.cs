using System.Collections.Generic;
using System.Linq;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>An item map interactor class.</para>
    /// </summary>
    /// <typeparam name="K">Dictionary key.</typeparam>
    /// <typeparam name="T">Dictionary value.</typeparam>
    /// <typeparam name="TRepository">Target repository type.</typeparam>
    /// <typeparam name="TData">Loaded data type.</typeparam>
    public abstract class ItemMapInteractor<K, T, TRepository, TData> :
        ItemInteractor<T, TRepository, IEnumerable<TData>>,
        IItemMapInteractor<K, T>
        where TRepository : ILoadRepository<IEnumerable<TData>>
    {
        /// <summary>
        ///     <para>An item dictionary.</para>
        /// </summary>
        protected readonly Dictionary<K, T> itemMap;

        protected ItemMapInteractor()
        {
            this.itemMap = new Dictionary<K, T>();
        }

        /// <summary>
        ///     Initializes an item dictionary by data set. 
        /// </summary>
        /// <param name="dataSet">Loaded user data set.</param>
        protected sealed override void Initialize(IEnumerable<TData> dataSet)
        {
            this.itemMap.Clear();
            foreach (var data in dataSet)
            {
                var item = this.CreateItem(data);
                var id = this.RetrieveItemId(item);
                this.itemMap[id] = item;
            }
        }

        /// <summary>
        ///     <para>Creates an item from data entity.</para>
        /// </summary>
        /// 
        /// <param name="data">Data entity.</param>
        /// <returns>A new instance of item.</returns>
        protected abstract T CreateItem(TData data);

        /// <summary>
        ///     <para>Gets an id from required item.</para>
        /// </summary>
        /// 
        /// <param name="item">A target item.</param>
        /// <returns>Id value.</returns>
        protected abstract K RetrieveItemId(T item);

        /// <inheritdoc cref="IItemMapInteractor{K,T}.GetItem"/>
        public T GetItem(K key)
        {
            return this.itemMap[key];
        }
        
        /// <inheritdoc cref="IItemMapInteractor{K,T}.GetItems"/>
        public IEnumerable<T> GetItems()
        {
            return this.itemMap.Values.ToList();
        }

        /// <inheritdoc cref="IItemMapInteractor{K,T}.GetItemCount"/>
        public int GetItemCount()
        {
            return this.itemMap.Count;
        }
    }
}