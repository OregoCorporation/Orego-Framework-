namespace OregoFramework.Game
{
    public interface IGameItemPool<T> : IGameNode
    {
        bool IsEmpty();

        bool IsNotEmpty();

        void PushItem(T item);

        T PopItem();

        void RegisterItem(T item);
        
        void UnregisterItem(T item);
    }
}