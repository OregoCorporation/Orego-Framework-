using System.Collections.Generic;

namespace OregoFramework.Game
{
    /// <summary>
    ///     <para>Manages a group of game items.</para>
    /// </summary>
    /// <typeparam name="T">Game item type.</typeparam>
    public interface IGameItemsManager<T> : IGameNode
    {
        /// <summary>
        ///     <para>Adds game item into group.</para>
        /// </summary>
        /// <param name="item">Game item instance.</param>
        void RegisterItem(T item);

        /// <summary>
        ///     <para>Removes game item from group.</para>
        /// </summary>
        /// <param name="item">Game item instance.</param>
        void UnregisterItem(T item);

        /// <summary>
        ///     <para>Gets a game item group.</para>
        /// </summary>
        IEnumerable<T> GetItems();
    }
}