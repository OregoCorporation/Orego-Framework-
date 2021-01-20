using System.Collections.Generic;
using System.Linq;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Controls an item dictionary.</para>
    /// </summary>
    /// 
    /// <typeparam name="K">Dictionary key.</typeparam>
    /// <typeparam name="V">Dictionary value.</typeparam>
    public interface IItemMapInteractor<in K, V> : IItemInteractor<V>
    {
        /// <summary>
        ///     <para>Returns an item from dictionary by key.</para>
        /// </summary>
        /// 
        /// <param name="key">Key.</param>
        /// <returns>An item reference.</returns>
        V GetItem(K key);

        /// <summary>
        ///     <para>Returns all items in dictionary.</para>
        /// </summary>
        /// <returns>An item array.</returns>
        IEnumerable<V> GetItems();

        /// <summary>
        ///     <para>Returns item count in dictionary.</para>
        /// </summary>
        /// 
        /// <returns>Count value.</returns>
        int GetItemCount();
    }
    
    
    /// <summary>
    ///     <para>An item map interactor class.</para>
    /// </summary>
    /// <typeparam name="K">Dictionary key.</typeparam>
    /// <typeparam name="V">Dictionary value.</typeparam>
    /// <typeparam name="TRepository">Target repository type.</typeparam>
    /// <typeparam name="TData">Loaded data type.</typeparam>
    public abstract class ItemMapInteractor<K, V, TRepository, TData> :
        ItemInteractor<V, TRepository, IEnumerable<TData>>,
        IItemMapInteractor<K, V>
        where TRepository : ILoadRepository<IEnumerable<TData>>
    {
        /// <summary>
        ///     <para>An item dictionary.</para>
        /// </summary>
        protected readonly Dictionary<K, V> ItemMap;

        protected ItemMapInteractor()
        {
            this.ItemMap = new Dictionary<K, V>();
        }

        /// <summary>
        ///     Initializes an item dictionary by data set. 
        /// </summary>
        /// <param name="dataSet">Loaded user data set.</param>
        protected sealed override void Initialize(IEnumerable<TData> dataSet)
        {
            this.ItemMap.Clear();
            foreach (var data in dataSet)
            {
                var item = this.CreateItem(data);
                var id = this.ProvideItemId(item);
                this.ItemMap[id] = item;
            }
        }

        /// <summary>
        ///     <para>Creates an item from data entity.</para>
        /// </summary>
        /// 
        /// <param name="data">Data entity.</param>
        /// <returns>A new instance of item.</returns>
        protected abstract V CreateItem(TData data);

        /// <summary>
        ///     <para>Gets an id from required item.</para>
        /// </summary>
        /// 
        /// <param name="item">A target item.</param>
        /// <returns>Id value.</returns>
        protected abstract K ProvideItemId(V item);

        /// <inheritdoc cref="IItemMapInteractor{K,T}.GetItem"/>
        public V GetItem(K key)
        {
            return this.ItemMap[key];
        }
        
        /// <inheritdoc cref="IItemMapInteractor{K,T}.GetItems"/>
        public IEnumerable<V> GetItems()
        {
            return this.ItemMap.Values.ToList();
        }

        /// <inheritdoc cref="IItemMapInteractor{K,T}.GetItemCount"/>
        public int GetItemCount()
        {
            return this.ItemMap.Count;
        }
    }
}