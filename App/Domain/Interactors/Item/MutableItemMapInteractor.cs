using System;
using System.Collections.Generic;

namespace OregoFramework.App
{
    public abstract class MutableItemMapInteractor<K, T, TRepository, TData> :
        ItemMapInteractor<K, T, TRepository, TData>,
        IMutableItemMapInteractor<K, T>
        where TRepository : IReadyRepository<IEnumerable<TData>>

    {
        #region Event

        public event Action<object, T> OnItemAddedEvent;

        public event Action<object, T> OnItemRemovedEvent;

        #endregion

        public void NotifyAboutItemAdded(object sender, T item)
        {
            this.OnItemAddedEvent?.Invoke(sender, item);
        }

        public void NotifyAboutItemRemoved(object sender, T item)
        {
            this.OnItemRemovedEvent?.Invoke(sender, item);
        }
    }
}