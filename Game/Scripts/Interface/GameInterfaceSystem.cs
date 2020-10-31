using System;
using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Game
{
    /// <inheritdoc cref="IGameInterfaceSystem"/>
    public abstract class GameInterfaceSystem : MonoBehaviour, IGameInterfaceSystem
    {
        /// <para>Bound game context reference.</para>
        public IGameContext gameContext { get; private set; }

        protected IEnumerable<IGameInterface> registeredInterfaces
        {
            get { return this.registeredInterfaceMap.Values; }
        }

        /// <summary>
        ///     <para>Map of registered unique interfaces.</para>
        /// </summary>
        private readonly Dictionary<Type, IGameInterface> registeredInterfaceMap;

        protected GameInterfaceSystem()
        {
            this.registeredInterfaceMap = new Dictionary<Type, IGameInterface>();
        }

        /// <inheritdoc cref="IGameInterfaceSystem.RegisterInterface"/>
        public void RegisterInterface(IGameInterface gameInterface)
        {
            var type = gameInterface.GetType();
            this.registeredInterfaceMap.Add(type, gameInterface);
            gameInterface.OnRegistered(this);
        }

        /// <inheritdoc cref="IGameInterfaceSystem.UnregisterInterface"/>
        public void UnregisterInterface(IGameInterface gameInterface)
        {
            gameInterface.OnUnregistered();
            var type = gameInterface.GetType();
            this.registeredInterfaceMap.Remove(type);
        }

        /// <inheritdoc cref="IGameInterfaceSystem.GetInterface{T}"/>
        public T GetInterface<T>() where T : IGameInterface
        {
            return this.registeredInterfaceMap.Find<T, IGameInterface>();
        }

        /// <inheritdoc cref="IGameInterfaceSystem.GetInterfaces{T}"/>
        public IEnumerable<T> GetInterfaces<T>() where T : IGameInterface
        {
            return this.registeredInterfaceMap.FindAll<T, IGameInterface>();
        }

        /// <summary>
        ///     <para>Binds game context.</para>
        /// </summary>
        /// <param name="gameContext">Target game context.</param>
        public void BindGameContext(IGameContext gameContext)
        {
            if (this.gameContext != null)
            {
                throw new Exception("Game context has already bound!");
            }

            this.gameContext = gameContext;
            this.gameContext.OnGamePreparedEvent += this.OnGamePrepared;
            this.gameContext.OnGameReadyEvent += this.OnGameReady;
            this.gameContext.OnGameStartedEvent += this.OnGameStarted;
            this.gameContext.OnGamePausedEvent += this.OnGamePaused;
            this.gameContext.OnGameResumedEvent += this.OnGameResumed;
            this.gameContext.OnGameFinishedEvent += this.OnGameFinished;
        }

        /// <summary>
        ///     <para>Unbinds game context.</para>
        /// </summary>
        /// <param name="gameContext">Target game context.</param>
        public void UnbindGameContext()
        {
            if (this.gameContext is null)
            {
                return;
            }

            this.gameContext.OnGamePreparedEvent -= this.OnGamePrepared;
            this.gameContext.OnGameReadyEvent -= this.OnGameReady;
            this.gameContext.OnGameStartedEvent -= this.OnGameStarted;
            this.gameContext.OnGamePausedEvent -= this.OnGamePaused;
            this.gameContext.OnGameResumedEvent -= this.OnGameResumed;
            this.gameContext.OnGameFinishedEvent -= this.OnGameFinished;
            this.gameContext = null;
        }

        #region GameCallbacks

        /// <inheritdoc cref="IGameContext.PrepareGame"/>
        private void OnGamePrepared(object sender)
        {
            foreach (var gameInterface in this.registeredInterfaces)
            {
                gameInterface.OnGamePrepared(sender);
            }

            this.OnGamePrepared(this, sender);
        }

        protected virtual void OnGamePrepared(GameInterfaceSystem _, object sender)
        {
        }


        /// <inheritdoc cref="IGameContext.ReadyGame"/>
        private void OnGameReady(object sender)
        {
            foreach (var gameInterface in this.registeredInterfaces)
            {
                gameInterface.OnGameReady(sender);
            }

            this.OnGameReady(this, sender);
        }

        protected virtual void OnGameReady(GameInterfaceSystem _, object sender)
        {
        }

        /// <inheritdoc cref="IGameContext.StartGame"/>
        private void OnGameStarted(object sender)
        {
            foreach (var gameInterface in this.registeredInterfaces)
            {
                gameInterface.OnGameStarted(sender);
            }

            this.OnGameStarted(this, sender);
        }

        protected virtual void OnGameStarted(GameInterfaceSystem _, object sender)
        {
        }

        /// <inheritdoc cref="IGameContext.PauseGame"/>
        private void OnGamePaused(object sender)
        {
            foreach (var gameInterface in this.registeredInterfaces)
            {
                gameInterface.OnGamePaused(sender);
            }

            this.OnGamePaused(this, sender);
        }

        protected virtual void OnGamePaused(GameInterfaceSystem _, object sender)
        {
        }

        /// <inheritdoc cref="IGameContext.ResumeGame"/>
        private void OnGameResumed(object sender)
        {
            foreach (var gameInterface in this.registeredInterfaces)
            {
                gameInterface.OnGameResumed(sender);
            }

            this.OnGameResumed(this, sender);
        }

        protected virtual void OnGameResumed(GameInterfaceSystem _, object sender)
        {
        }

        /// <inheritdoc cref="IGameContext.FinishGame"/>
        private void OnGameFinished(object sender)
        {
            foreach (var gameInterface in this.registeredInterfaces)
            {
                gameInterface.OnGameFinished(sender);
            }

            this.OnGameFinished(this, sender);
        }

        protected virtual void OnGameFinished(GameInterfaceSystem _, object sender)
        {
        }

        #endregion
    }
}