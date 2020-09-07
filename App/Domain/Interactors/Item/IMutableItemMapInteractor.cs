using System;

namespace OregoFramework.App
{
    public interface IMutableItemMapInteractor<in K, T> : IItemMapInteractor<K, T>
    {
        #region Event

        event Action<object, T> OnItemAddedEvent;

        event Action<object, T> OnItemRemovedEvent;

        #endregion

        void NotifyAboutItemAdded(object sender, T item);

        void NotifyAboutItemRemoved(object sender, T item);
    }
}