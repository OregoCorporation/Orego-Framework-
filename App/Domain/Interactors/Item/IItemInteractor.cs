using System;

namespace OregoFramework.App
{
    public interface IItemInteractor<T> : IInteractor
    {
        event Action<object, T> OnItemChangedEvent;

        void NotifyAboutItemChanged(object sender, T item);
    }
}