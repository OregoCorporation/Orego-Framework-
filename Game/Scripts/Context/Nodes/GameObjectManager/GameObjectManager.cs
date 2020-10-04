using System.Collections.Generic;
using OregoFramework.Game;

namespace OregoFramework.Util
{
    public abstract class GameObjectManager<T> : EnableGameNode
    {
        protected HashSet<T> gameObjectSet { get; }

        protected GameObjectManager()
        {
            this.gameObjectSet = new HashSet<T>();
        }

        public IEnumerable<T> GetObjects()
        {
            return this.gameObjectSet;
        }

        #region OnPrepareGame

        protected sealed override void OnPrepareGame(GameNode _, object sender)
        {
            this.OnBeforePrepareGame(this, sender);
            var gameObjects = this.ProvideGameObjects();
            foreach (var item in gameObjects)
            {
                this.OnPrepareGameObject(this, sender, item);
                this.gameObjectSet.Add(item);
            }

            this.OnAfterPrepareGame(this, sender);
        }

        protected virtual void OnBeforePrepareGame(GameObjectManager<T> _, object sender)
        {
        }

        protected abstract IEnumerable<T> ProvideGameObjects();

        protected virtual void OnPrepareGameObject(GameObjectManager<T> _, object sender, T item)
        {
        }

        protected virtual void OnAfterPrepareGame(GameObjectManager<T> _, object sender)
        {
        }

        #endregion

        #region OnReadyGame

        protected sealed override void OnReadyGame(GameNode _, object sender)
        {
            this.OnBeforeReadyGame(this, sender);
            foreach (var item in this.gameObjectSet)
            {
                this.OnReadyGameObject(this, sender, item);
            }

            this.OnAfterReadyGame(this, sender);
        }

        protected virtual void OnBeforeReadyGame(GameObjectManager<T> _, object sender)
        {
        }

        protected virtual void OnReadyGameObject(GameObjectManager<T> _, object sender, T item)
        {
        }

        protected virtual void OnAfterReadyGame(GameObjectManager<T> _, object sender)
        {
        }

        #endregion

        #region OnStartGame

        protected sealed override void OnStartGame(EnableGameNode _, object sender)
        {
            this.OnBeforeStartGame(this, sender);
            foreach (var item in this.gameObjectSet)
            {
                this.OnStartGameObject(this, sender, item);
            }

            this.OnAfterStartGame(this, sender);
        }

        protected virtual void OnBeforeStartGame(GameObjectManager<T> _, object sender)
        {
        }

        protected virtual void OnStartGameObject(GameObjectManager<T> _, object sender, T item)
        {
        }

        protected virtual void OnAfterStartGame(GameObjectManager<T> _, object sender)
        {
        }

        #endregion

        #region OnPauseGame

        protected sealed override void OnPauseGame(EnableGameNode _, object sender)
        {
            this.OnBeforePauseGame(this, sender);
            foreach (var item in this.gameObjectSet)
            {
                this.OnPauseGameObject(this, sender, item);
            }

            this.OnAfterPauseGame(this, sender);
        }

        protected virtual void OnBeforePauseGame(GameObjectManager<T> _, object sender)
        {
        }

        protected virtual void OnPauseGameObject(GameObjectManager<T> _, object sender, T item)
        {
        }

        protected virtual void OnAfterPauseGame(GameObjectManager<T> _, object sender)
        {
        }

        #endregion

        #region OnResumeGame

        protected sealed override void OnResumeGame(EnableGameNode _, object sender)
        {
            this.OnBeforeResumeGame(this, sender);
            foreach (var item in this.gameObjectSet)
            {
                this.OnResumeGameObject(this, sender, item);
            }

            this.OnAfterResumeGame(this, sender);
        }


        protected virtual void OnResumeGameObject(GameObjectManager<T> _, object sender, T item)
        {
        }

        protected virtual void OnBeforeResumeGame(GameObjectManager<T> _, object sender)
        {
        }

        protected virtual void OnAfterResumeGame(GameObjectManager<T> _, object sender)
        {
        }

        #endregion

        #region OnFinishGame

        protected sealed override void OnFinishGame(EnableGameNode _, object sender)
        {
            this.OnBeforeFinishGame(this, sender);
            foreach (var item in this.gameObjectSet)
            {
                this.OnFinishGameObject(item);
            }

            this.OnAfterFinishGame(this, sender);
        }

        protected virtual void OnBeforeFinishGame(GameObjectManager<T> _, object sender)
        {
        }

        protected virtual void OnAfterFinishGame(GameObjectManager<T> _, object sender)
        {
        }

        protected virtual void OnFinishGameObject(T other)
        {
            this.gameObjectSet.Remove(other);
        }

        #endregion
    }
}