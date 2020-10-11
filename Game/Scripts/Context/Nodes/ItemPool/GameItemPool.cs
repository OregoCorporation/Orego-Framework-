using System.Collections.Generic;
using OregoFramework.Util;

namespace OregoFramework.Game
{
    public abstract class GameItemPool<T> : GameItemManager<T>, IGameItemPool<T>
    {
        protected List<T> availableItems { get; }

        #region Lifecycle

        protected GameItemPool()
        {
            this.availableItems = new List<T>();
        }

        protected sealed override void OnRegisterItem(T item)
        {
            this.availableItems.Add(item);
            this.OnRegisterItem(this, item);
        }

        protected virtual void OnRegisterItem(GameItemPool<T> _, T item)
        {
        }

        protected sealed override void OnUnregisterItem(T item)
        {
            this.availableItems.Remove(item);
            this.OnUnregisterItem(this, item);
        }

        protected virtual void OnUnregisterItem(GameItemPool<T> _, T item)
        {
        }

        #endregion

        public void PushItem(T item)
        {
            this.availableItems.Add(item);
        }

        public T PopItem()
        {
            var next = this.Peek();
            this.availableItems.Remove(next);
            return next;
        }

        public T Peek()
        {
            return this.availableItems[Int.ZERO];
        }

        public bool IsEmpty()
        {
            return this.availableItems.IsEmpty();
        }

        public bool IsNotEmpty()
        {
            return this.availableItems.IsNotEmpty();
        }
    }
}