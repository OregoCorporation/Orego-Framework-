using System;
using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Game
{
    /// <summary>
    ///     <inheritdoc cref="IGameContext"/>
    ///     <para>Standard game system implementation.</para>
    ///     <para>Keeps game components <see cref="IGameNode"/>.</para>
    /// </summary>
    public abstract class GameContext : MonoBehaviour, IGameContext, IGameNodeLayer
    {
        #region Event

        /// <inheritdoc cref="IGameContext.OnGameLoadedEvent"/>
        public abstract event Action<object> OnGameLoadedEvent;

        /// <inheritdoc cref="IGameContext.OnGamePreparedEvent"/>
        public abstract event Action<object> OnGamePreparedEvent;

        /// <inheritdoc cref="IGameContext.OnGameReadyEvent"/>
        public abstract event Action<object> OnGameReadyEvent;

        /// <inheritdoc cref="IGameContext.OnGameStartedEvent"/>
        public abstract event Action<object> OnGameStartedEvent;

        /// <inheritdoc cref="IGameContext.OnGamePausedEvent"/>
        public abstract event Action<object> OnGamePausedEvent;

        /// <inheritdoc cref="IGameContext.OnGameResumedEvent"/>
        public abstract event Action<object> OnGameResumedEvent;

        /// <inheritdoc cref="IGameContext.OnGameFinishedEvent"/>
        public abstract event Action<object> OnGameFinishedEvent;

        #endregion

        /// <inheritdoc cref="IGameContext.gameStatus"/>
        public GameStatus gameStatus { get; protected set; }

        /// <summary>
        ///     <para>Map of unique registered game components.
        ///     Key: a node type, Value: a node instance.</para>
        /// </summary>
        private readonly Dictionary<Type, IGameNode> registeredNodeMap;

        protected IEnumerable<IGameNode> registeredNodes
        {
            get { return this.registeredNodeMap.Values; }
        }

        protected GameContext()
        {
            this.gameStatus = GameStatus.CREATING;
            this.registeredNodeMap = new Dictionary<Type, IGameNode>();
        }

        /// <inheritdoc cref="IGameContext.LoadGame"/>
        public void LoadGame(object sender)
        {
            this.gameStatus = GameStatus.LOADING;
            this.OnLoadGame(this, sender);
        }

        protected virtual void OnLoadGame(GameContext _, object sender)
        {
        }

        /// <inheritdoc cref="IGameContext.PrepareGame"/>
        public void PrepareGame(object sender)
        {
            this.gameStatus = GameStatus.PREPARING;
            foreach (var node in this.registeredNodes)
            {
                node.OnPrepareGame(sender);
            }

            this.OnPrepareGame(this, sender);
        }

        protected virtual void OnPrepareGame(GameContext _, object sender)
        {
        }

        /// <inheritdoc cref="IGameContext.ReadyGame"/>
        public void ReadyGame(object sender)
        {
            this.gameStatus = GameStatus.READY;
            foreach (var node in this.registeredNodes)
            {
                node.OnReadyGame(sender);
            }

            this.OnReadyGame(this, sender);
        }

        protected virtual void OnReadyGame(GameContext _, object sender)
        {
        }

        /// <inheritdoc cref="IGameContext.StartGame"/>
        public void StartGame(object sender)
        {
            this.gameStatus = GameStatus.PLAYING;
            foreach (var node in this.registeredNodes)
            {
                node.OnStartGame(sender);
            }
        }

        protected virtual void OnStartGame(GameContext _, object sender)
        {
        }

        /// <inheritdoc cref="IGameContext.PauseGame"/>
        public void PauseGame(object sender)
        {
            this.gameStatus = GameStatus.PAUSING;
            foreach (var node in this.registeredNodes)
            {
                node.OnPauseGame(sender);
            }

            this.OnPauseGame(this, sender);
        }

        protected virtual void OnPauseGame(GameContext _, object sender)
        {
        }

        /// <inheritdoc cref="IGameContext.ResumeGame"/>
        public void ResumeGame(object sender)
        {
            this.gameStatus = GameStatus.PLAYING;
            foreach (var node in this.registeredNodes)
            {
                node.OnResumeGame(sender);
            }

            this.OnResumeGame(this, sender);
        }

        protected virtual void OnResumeGame(GameContext _, object sender)
        {
        }
        
        /// <inheritdoc cref="IGameContext.FinishGame"/>
        public void FinishGame(object sender)
        {
            this.gameStatus = GameStatus.FINISHING;
            foreach (var node in this.registeredNodes)
            {
                node.OnFinishGame(sender);
            }
        }

        protected virtual void OnFinishGame(GameContext _, object sender)
        {
        }

        /// <inheritdoc cref="IGameContext.DestroyGame"/>
        public void DestroyGame(object sender)
        {
            this.gameStatus = GameStatus.DESTROYING;
            foreach (var node in this.registeredNodes)
            {
                node.OnDestroyGame(sender);
            }
        }

        protected virtual void OnDestroyGame(GameContext _, object sender)
        {
        }

        /// <inheritdoc cref="IGameNodeLayer.RegisterNode"/>
        public void RegisterNode(IGameNode gameNode)
        {
            this.registeredNodeMap.AddByType(gameNode);
            gameNode.OnRegistered(this);
        }

        /// <inheritdoc cref="IGameNodeLayer.UnregisterNode"/>
        public virtual void UnregisterNode(IGameNode gameNode)
        {
            gameNode.OnUnregistered();
            this.registeredNodeMap.RemoveByType(gameNode);
        }

        /// <inheritdoc cref="IGameNodeLayer.GetNode{T}"/>
        public T GetNode<T>() where T : IGameNode
        {
            return this.registeredNodeMap.Find<T, IGameNode>();
        }

        /// <inheritdoc cref="IGameNodeLayer.GetNodes{T}"/>
        public IEnumerable<T> GetNodes<T>() where T : IGameNode
        {
            return this.registeredNodeMap.FindAll<T, IGameNode>();
        }
    }
}