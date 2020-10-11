using System.Collections.Generic;

namespace OregoFramework.Game
{
    public interface IGameNodeLayer
    {
        T GetNode<T>() where T : IGameNode;

        IEnumerable<T> GetNodes<T>() where T : IGameNode;

        void RegisterNode(IGameNode gameNode);

        void UnregisterNode(IGameNode gameNode);
    }
}