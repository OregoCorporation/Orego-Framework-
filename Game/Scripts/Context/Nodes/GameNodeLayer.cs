using System;
using System.Collections.Generic;
using OregoFramework.Util;

namespace OregoFramework.Game
{
    public abstract class GameNodeLayer : GameNode, IGameNodeLayer
    {
        private readonly Dictionary<Type, IGameNode> nodeMap;

        private IEnumerable<IGameNode> nodes
        {
            get { return this.nodeMap.Values; }
        }

        protected GameNodeLayer()
        {
            this.nodeMap = new Dictionary<Type, IGameNode>();
        }

        protected sealed override void OnPrepareGame(GameNode _, object sender)
        {
            foreach (var node in this.nodes)
            {
                node.OnPrepareGame(sender);
            }

            this.OnPrepareGame(this, sender);
        }

        protected virtual void OnPrepareGame(GameNodeLayer _, object sender)
        {
        }

        protected sealed override void OnReadyGame(GameNode _, object sender)
        {
            foreach (var node in this.nodes)
            {
                node.OnReadyGame(sender);
            }

            this.OnReadyGame(this, sender);
        }

        protected virtual void OnReadyGame(GameNodeLayer _, object sender)
        {
        }

        protected sealed override void OnStartGame(GameNode _, object sender)
        {
            foreach (var node in this.nodes)
            {
                node.OnStartGame(sender);
            }

            this.OnStartGame(this, sender);
        }

        protected virtual void OnStartGame(GameNodeLayer _, object sender)
        {
        }

        protected sealed override void OnPauseGame(GameNode _, object sender)
        {
            foreach (var node in this.nodes)
            {
                node.OnPauseGame(sender);
            }

            this.OnPauseGame(this, sender);
        }

        protected virtual void OnPauseGame(GameNodeLayer _, object sender)
        {
        }

        protected sealed override void OnResumeGame(GameNode _, object sender)
        {
            foreach (var node in this.nodes)
            {
                node.OnResumeGame(sender);
            }

            this.OnResumeGame(this, sender);
        }

        protected virtual void OnResumeGame(GameNodeLayer _, object sender)
        {
        }

        protected sealed override void OnFinishGame(GameNode _, object sender)
        {
            foreach (var node in this.nodes)
            {
                node.OnFinishGame(sender);
            }

            this.OnFinishGame(this, sender);
        }

        protected virtual void OnFinishGame(GameNodeLayer _, object sender)
        {
        }

        public T GetNode<T>() where T : IGameNode
        {
            return this.nodeMap.Find<T, IGameNode>();
        }

        public IEnumerable<T> GetNodes<T>() where T : IGameNode
        {
            return this.nodeMap.FindAll<T, IGameNode>();
        }

        public void RegisterNode(IGameNode gameNode)
        {
            this.nodeMap.AddByType(gameNode);
            gameNode.OnRegistered(this.gameContext);
        }

        public void UnregisterNode(IGameNode gameNode)
        {
            gameNode.OnUnregistered();
            this.nodeMap.RemoveByType(gameNode);
        }
    }
}