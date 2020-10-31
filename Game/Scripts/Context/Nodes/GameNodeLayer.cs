using System;
using System.Collections.Generic;
using OregoFramework.Util;

namespace OregoFramework.Game
{
    /// <inheritdoc cref="IGameNodeLayer"/>
    public abstract class GameNodeLayer : GameNode, IGameNodeLayer
    {
        /// <summary>
        ///     <para>Map of unique registered game components.
        ///     Key: a node type, Value: a node instance.</para>
        /// </summary>
        private readonly Dictionary<Type, IGameNode> registeredNodeMap;

        protected IEnumerable<IGameNode> registeredNodes
        {
            get { return this.registeredNodeMap.Values; }
        }

        protected GameNodeLayer()
        {
            this.registeredNodeMap = new Dictionary<Type, IGameNode>();
        }

        /// <inheritdoc cref="IGameNode.OnPrepareGame"/>
        public sealed override void OnPrepareGame(object sender)
        {
            foreach (var node in this.registeredNodes)
            {
                node.OnPrepareGame(sender);
            }

            this.OnPrepareGame(this, sender);
        }

        protected virtual void OnPrepareGame(GameNodeLayer _, object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnReadyGame"/>
        public sealed override void OnReadyGame(object sender)
        {
            foreach (var node in this.registeredNodes)
            {
                node.OnReadyGame(sender);
            }

            this.OnReadyGame(this, sender);
        }

        protected virtual void OnReadyGame(GameNodeLayer _, object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnStartGame"/>
        public sealed override void OnStartGame(object sender)
        {
            foreach (var node in this.registeredNodes)
            {
                node.OnStartGame(sender);
            }

            this.OnStartGame(this, sender);
        }

        protected virtual void OnStartGame(GameNodeLayer _, object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnPauseGame"/>
        public sealed override void OnPauseGame(object sender)
        {
            foreach (var node in this.registeredNodes)
            {
                node.OnPauseGame(sender);
            }

            this.OnPauseGame(this, sender);
        }

        protected virtual void OnPauseGame(GameNodeLayer _, object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnResumeGame"/>
        public sealed override void OnResumeGame(object sender)
        {
            foreach (var node in this.registeredNodes)
            {
                node.OnResumeGame(sender);
            }

            this.OnResumeGame(this, sender);
        }

        protected virtual void OnResumeGame(GameNodeLayer _, object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnFinishGame"/>
        public sealed override void OnFinishGame(object sender)
        {
            foreach (var node in this.registeredNodes)
            {
                node.OnFinishGame(sender);
            }

            this.OnFinishGame(this, sender);
        }

        protected virtual void OnFinishGame(GameNodeLayer _, object sender)
        {
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

        /// <inheritdoc cref="IGameNodeLayer.RegisterNode"/>
        public void RegisterNode(IGameNode gameNode)
        {
            this.registeredNodeMap.AddByType(gameNode);
            gameNode.OnRegistered(this.gameContext);
        }

        /// <inheritdoc cref="IGameNodeLayer.UnregisterNode"/>
        public void UnregisterNode(IGameNode gameNode)
        {
            gameNode.OnUnregistered();
            this.registeredNodeMap.RemoveByType(gameNode);
        }
    }
}