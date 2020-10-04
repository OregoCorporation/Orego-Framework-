using System.Collections.Generic;
using UnityEngine;

namespace OregoFramework.Game
{
    public interface IGameObjectPool : IGameNode
    {
        IEnumerable<GameObject> items { get; }
        
        bool IsEmpty();

        bool IsNotEmpty();

        void Push(GameObject poolObject);

        GameObject Pop();

        void RegisterGameObject(GameObject item);
        
        void UnregisterGameObject(GameObject item);
    }
}