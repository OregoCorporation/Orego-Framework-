using System;

namespace OregoFramework.App
{
    public interface IItemInteractor<T> : IInteractor
    {
        event Action<object, T> OnItemChangedEvent;

        void NotifyAboutObjectChanged(object sender, T item);
    }
}