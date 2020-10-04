using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Game
{
    public abstract class GameObjectQueuePool : GameNode, IGameObjectPool
    {
        protected readonly HashSet<GameObject> allGameObjects;

        protected readonly List<GameObject> availableGameObjects;

        protected GameObjectQueuePool()
        {
            this.allGameObjects = new HashSet<GameObject>();
            this.availableGameObjects = new List<GameObject>();
        }

        public IEnumerable<GameObject> items
        {
            get { return this.allGameObjects; }
        }

        public void RegisterGameObject(GameObject item)
        {
            this.allGameObjects.Add(item);
            this.availableGameObjects.Add(item);
            this.OnRegisterGameObject(this, item);
        }

        protected virtual void OnRegisterGameObject(GameObjectQueuePool _, GameObject item)
        {
        }

        public void UnregisterGameObject(GameObject item)
        {
            this.allGameObjects.Remove(item);
            this.availableGameObjects.Remove(item);
            this.OnRegisterGameObject(this, item);
        }

        protected virtual void OnUnregisterGameObject(GameObjectQueuePool _, GameObject item)
        {
        }
        
        public void Push(GameObject poolObject)
        {
            this.availableGameObjects.Add(poolObject);
        }

        public GameObject Pop()
        {
            var next = this.Peek();
            this.availableGameObjects.Remove(next);
            return next;
        }

        public GameObject Peek()
        {
            return this.availableGameObjects[Int.ZERO];
        }

        public bool IsEmpty()
        {
            return this.availableGameObjects.IsEmpty();
        }

        public bool IsNotEmpty()
        {
            return this.availableGameObjects.IsNotEmpty();
        }
    }
}