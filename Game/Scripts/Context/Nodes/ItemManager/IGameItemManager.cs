using System.Collections.Generic;

namespace OregoFramework.Game
{
    public interface IGameItemManager<T> : IGameNode
    {
        void RegisterItem(T item);

        void UnregisterItem(T item);

        IEnumerable<T> GetItems();
    }
}