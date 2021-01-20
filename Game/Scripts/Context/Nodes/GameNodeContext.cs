using System;
using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Game
{
    public interface IGameNodeContext : IGameContext, IGameNodeContainer, IGameNodeLayer
    {
    }

    /// <summary>
    ///     <inheritdoc cref="IGameContext"/>
    ///     <para>Node game system implementation.</para>
    /// </summary>
    public abstract class GameNodeContext : MonoBehaviour, IGameNodeContext
    {
        #region Event

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

        /// <inheritdoc cref="IGameContext.Status"/>
        public GameStatus Status { get; protected set; }

        /// <summary>
        ///     <para>Map of unique registered game components.
        ///     Key: a node type, Value: a node instance.</para>
        /// </summary>
        private readonly Dictionary<Type, IGameNode> registeredNodeMap;

        protected IEnumerable<IGameNode> RegisteredNodes
        {
            get { return this.registeredNodeMap.Values; }
        }

        #region Lifecycle

        protected GameNodeContext()
        {
            this.Status = GameStatus.CREATING;
            this.registeredNodeMap = new Dictionary<Type, IGameNode>();
        }

        /// <inheritdoc cref="IGameContext.PrepareGame"/>
        public virtual void PrepareGame(object sender)
        {
            this.Status = GameStatus.PREPARING;
            foreach (var node in this.RegisteredNodes)
            {
                node.OnPrepareGame(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.ReadyGame"/>
        public virtual void ReadyGame(object sender)
        {
            this.Status = GameStatus.READY;
            foreach (var node in this.RegisteredNodes)
            {
                node.OnReadyGame(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.StartGame"/>
        public virtual void StartGame(object sender)
        {
            this.Status = GameStatus.PLAYING;
            foreach (var node in this.RegisteredNodes)
            {
                node.OnStartGame(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.PauseGame"/>
        public virtual void PauseGame(object sender)
        {
            this.Status = GameStatus.PAUSING;
            foreach (var node in this.RegisteredNodes)
            {
                node.OnPauseGame(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.ResumeGame"/>
        public virtual void ResumeGame(object sender)
        {
            this.Status = GameStatus.PLAYING;
            foreach (var node in this.RegisteredNodes)
            {
                node.OnResumeGame(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.FinishGame"/>
        public virtual void FinishGame(object sender)
        {
            this.Status = GameStatus.FINISHING;
            foreach (var node in this.RegisteredNodes)
            {
                node.OnFinishGame(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.DestroyGame"/>
        public virtual void DestroyGame(object sender)
        {
            this.Status = GameStatus.DESTROYING;
            foreach (var node in this.RegisteredNodes)
            {
                node.OnDestroyGame(sender);
            }
        }

        #endregion

        /// <inheritdoc cref="IGameNodeLayer.RegisterNode"/>
        public virtual void RegisterNode(IGameNode gameNode)
        {
            this.registeredNodeMap.AddByType(gameNode);
            gameNode.OnRegistered(this, this);
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