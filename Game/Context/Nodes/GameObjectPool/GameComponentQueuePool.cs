using System.Collections.Generic;
using UnityEngine;

namespace OregoFramework.Game
{
    public class GameComponentQueuePool<T> : GameObjectQueuePool where T : Component
    {
        protected readonly HashSet<T> allComponents;

        protected readonly Dictionary<T, GameObject> componentVsGameObjectMap;

        protected readonly Dictionary<GameObject, T> gameObjectVsComponentMap;

        public GameComponentQueuePool()
        {
            this.allComponents = new HashSet<T>();
            this.componentVsGameObjectMap = new Dictionary<T, GameObject>();
            this.gameObjectVsComponentMap = new Dictionary<GameObject, T>();
        }

        protected sealed override void OnRegisterGameObject(GameObjectQueuePool _, GameObject item)
        {
            var component = item.GetComponent<T>();
            this.allComponents.Add(component);
            this.gameObjectVsComponentMap.Add(item, component);
            this.componentVsGameObjectMap.Add(component, item);
            this.OnRegisterGameObject(this, item);
        }

        protected virtual void OnRegisterGameObject(GameComponentQueuePool<T> _, GameObject item)
        {
        }

        public IEnumerable<T> GetAsComponents()
        {
            return this.allComponents;
        }

        public T PeekAsComponent()
        {
            var item = this.Peek();
            return this.gameObjectVsComponentMap[item];
        }

        public T PopAsComponent()
        {
            var item = this.Pop();
            return this.gameObjectVsComponentMap[item];
        }

        public void PushAsComponent(T component)
        {
            var item = this.componentVsGameObjectMap[component];
            this.Push(item);
        }
    }
}