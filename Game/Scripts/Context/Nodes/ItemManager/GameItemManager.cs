using System.Collections.Generic;

namespace OregoFramework.Game
{
    public abstract class GameItemManager<T> : GameNode, IGameItemManager<T>
    {
        protected HashSet<T> registeredItems { get; }

        protected GameItemManager()
        {
            this.registeredItems = new HashSet<T>();
        }

        public void RegisterItem(T item)
        {
            this.registeredItems.Add(item);
            this.OnRegisterItem(item);
        }

        protected virtual void OnRegisterItem(T item)
        {
        }

        public void UnregisterItem(T item)
        {
            this.registeredItems.Remove(item);
            this.OnUnregisterItem(item);
        }

        protected virtual void OnUnregisterItem(T item)
        {
        }

        public virtual IEnumerable<T> GetItems()
        {
            return this.registeredItems;
        }
    }
}