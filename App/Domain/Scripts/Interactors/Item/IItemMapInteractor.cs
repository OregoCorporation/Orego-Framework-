using System.Collections.Generic;

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
}