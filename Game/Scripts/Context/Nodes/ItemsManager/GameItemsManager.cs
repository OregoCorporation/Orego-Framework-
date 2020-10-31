using System.Collections.Generic;

namespace OregoFramework.Game
{
    /// <inheritdoc cref="IGameItemsManager{T}"/>
    public abstract class GameItemsManager<T> : GameNode, IGameItemsManager<T>
    {
        /// <summary>
        ///     <para>Registered game items.</para>
        /// </summary>
        protected HashSet<T> registeredItems { get; }

        protected GameItemsManager()
        {
            this.registeredItems = new HashSet<T>();
        }

        /// <inheritdoc cref="IGameItemsManager{T}.RegisterItem"/>
        public void RegisterItem(T item)
        {
            this.registeredItems.Add(item);
            this.OnRegisterItem(item);
        }

        protected virtual void OnRegisterItem(T item)
        {
        }

        /// <inheritdoc cref="IGameItemsManager{T}.UnregisterItem"/>
        public void UnregisterItem(T item)
        {
            this.registeredItems.Remove(item);
            this.OnUnregisterItem(item);
        }

        protected virtual void OnUnregisterItem(T item)
        {
        }

        /// <inheritdoc cref="IGameItemsManager{T}.GetItems"/>
        public virtual IEnumerable<T> GetItems()
        {
            return this.registeredItems;
        }
    }
}