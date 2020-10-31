using System;
using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Game
{
    public abstract class GameInterfaceSystem : MonoBehaviour, IGameInterfaceSystem
    {
        public IGameContext gameContext { get; private set; }

        protected IEnumerable<IGameInterface> registeredInterfaces
        {
            get { return this.registeredInterfaceMap.Values; }
        }

        private readonly Dictionary<Type, IGameInterface> registeredInterfaceMap;

        protected GameInterfaceSystem()
        {
            this.registeredInterfaceMap = new Dictionary<Type, IGameInterface>();
        }

        public virtual void RegisterInterface(IGameInterface gameInterface)
        {
            var type = gameInterface.GetType();
            this.registeredInterfaceMap.Add(type, gameInterface);
            gameInterface.OnRegistered(this);
        }

        public virtual void UnregisterInterface(IGameInterface gameInterface)
        {
            gameInterface.OnUnregistered();
            var type = gameInterface.GetType();
            this.registeredInterfaceMap.Remove(type);
        }

        public T GetInterface<T>() where T : IGameInterface
        {
            return this.registeredInterfaceMap.Find<T, IGameInterface>();
        }

        public IEnumerable<T> GetInterfaces<T>() where T : IGameInterface
        {
            return this.registeredInterfaceMap.FindAll<T, IGameInterface>();
        }

        public virtual void BindGameContext(IGameContext gameContext)
        {
            this.gameContext = gameContext;
            this.gameContext.OnGamePreparedEvent += this.OnGamePrepared;
            this.gameContext.OnGameReadyEvent += this.OnGameReady;
            this.gameContext.OnGameStartedEvent += this.OnGameStarted;
            this.gameContext.OnGamePausedEvent += this.OnGamePaused;
            this.gameContext.OnGameResumedEvent += this.OnGameResumed;
            this.gameContext.OnGameFinishedEvent += this.OnGameFinished;
        }

        public virtual void UnbindGameContext()
        {
            this.gameContext.OnGamePreparedEvent -= this.OnGamePrepared;
            this.gameContext.OnGameReadyEvent -= this.OnGameReady;
            this.gameContext.OnGameStartedEvent -= this.OnGameStarted;
            this.gameContext.OnGamePausedEvent -= this.OnGamePaused;
            this.gameContext.OnGameResumedEvent -= this.OnGameResumed;
            this.gameContext.OnGameFinishedEvent -= this.OnGameFinished;
            this.gameContext = null;
        }

        #region GameCallbacks

        private void OnGamePrepared(object sender)
        {
            foreach (var view in this.registeredInterfaces)
            {
                view.OnGamePrepared(sender);
            }
            
            this.OnGamePrepared(this, sender);
        }

        private void OnGameReady(object sender)
        {
            foreach (var view in this.registeredInterfaces)
            {
                view.OnGameReady(sender);
            }
            
            this.OnGameReady(this, sender);
        }

        private void OnGameStarted(object sender)
        {
            foreach (var view in this.registeredInterfaces)
            {
                view.OnGameStarted(sender);
            }
            
            this.OnGameStarted(this, sender);
        }

        private void OnGamePaused(object sender)
        {
            foreach (var view in this.registeredInterfaces)
            {
                view.OnGamePaused(sender);
            }
            
            this.OnGamePaused(this, sender);
        }

        private void OnGameResumed(object sender)
        {
            foreach (var view in this.registeredInterfaces)
            {
                view.OnGameResumed(sender);
            }
            
            this.OnGameResumed(this, sender);
        }

        private void OnGameFinished(object sender)
        {
            foreach (var view in this.registeredInterfaces)
            {
                view.OnGameFinished(sender);
            }
            
            this.OnGameFinished(this, sender);
        }

        #endregion

        protected virtual void OnGamePrepared(GameInterfaceSystem _, object sender)
        {
        }

        protected virtual void OnGameReady(GameInterfaceSystem _, object sender)
        {
        }

        protected virtual void OnGameStarted(GameInterfaceSystem _, object sender)
        {
        }

        protected virtual void OnGamePaused(GameInterfaceSystem _, object sender)
        {
        }

        protected virtual void OnGameResumed(GameInterfaceSystem _, object sender)
        {
        }

        protected virtual void OnGameFinished(GameInterfaceSystem _, object sender)
        {
        }
    }
}