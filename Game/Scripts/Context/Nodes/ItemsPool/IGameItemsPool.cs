namespace OregoFramework.Game
{
    /// <summary>
    ///     <para>Pool of game items.</para>
    /// </summary>
    /// <typeparam name="T">Game item type.</typeparam>
    public interface IGameItemsPool<T> : IGameNode
    {
        /// <summary>
        ///     <para>Returns pool is empty or not.</para>
        /// </summary>
        bool IsEmpty();
        
        /// <summary>
        ///     <para>Push game item into pool.</para>
        /// </summary>
        void PushItem(T item);

        /// <summary>
        ///     <para>Retrieves game item from pool.</para>
        /// </summary>
        T PopItem();

        /// <summary>
        ///     <para>Adds game item into pool.</para>
        /// </summary>
        /// <param name="item">Game item instance.</param>
        void RegisterItem(T item);
        
        /// <summary>
        ///     <para>Removes game item from pool.</para>
        /// </summary>
        /// <param name="item">Game item instance.</param>
        void UnregisterItem(T item);
    }
}