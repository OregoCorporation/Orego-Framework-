using System;
using System.Collections.Generic;
using OregoFramework.Util;
using UnityEngine;

namespace OregoFramework.Game
{
    public abstract class GameContext : MonoBehaviour, IGameContext, IGameNodeLayer
    {
        #region Event

        public abstract event Action<object> OnGameLoadedEvent;

        public abstract event Action<object> OnGamePreparedEvent;

        public abstract event Action<object> OnGameReadyEvent;

        public abstract event Action<object> OnGameStartedEvent;

        public abstract event Action<object> OnGamePausedEvent;

        public abstract event Action<object> OnGameResumedEvent;

        public abstract event Action<object> OnGameFinishedEvent;

        #endregion

        public GameStatus gameStatus { get; protected set; }

        private readonly Dictionary<Type, IGameNode> nodeMap;

        protected IEnumerable<IGameNode> nodes
        {
            get { return this.nodeMap.Values; }
        }

        protected GameContext()
        {
            this.gameStatus = GameStatus.CREATING;
            this.nodeMap = new Dictionary<Type, IGameNode>();
        }

        public void LoadGame(object sender)
        {
            this.gameStatus = GameStatus.LOADING;
            this.OnLoadGame(this, sender);
        }

        protected virtual void OnLoadGame(GameContext _, object sender)
        {
        }

        public void PrepareGame(object sender)
        {
            this.gameStatus = GameStatus.PREPARING;
            foreach (var node in this.nodes)
            {
                node.OnPrepareGame(sender);
            }

            this.OnPrepareGame(this, sender);
        }

        protected virtual void OnPrepareGame(GameContext _, object sender)
        {
        }

        public void ReadyGame(object sender)
        {
            this.gameStatus = GameStatus.READY;
            foreach (var node in this.nodes)
            {
                node.OnReadyGame(sender);
            }

            this.OnReadyGame(this, sender);
        }

        protected virtual void OnReadyGame(GameContext _, object sender)
        {
        }

        public void StartGame(object sender)
        {
            this.gameStatus = GameStatus.PLAYING;
            foreach (var node in this.nodes)
            {
                node.OnStartGame(sender);
            }
        }

        protected virtual void OnStartGame(GameContext _, object sender)
        {
        }

        public void PauseGame(object sender)
        {
            this.gameStatus = GameStatus.PAUSING;
            foreach (var node in this.nodes)
            {
                node.OnPauseGame(sender);
            }

            this.OnPauseGame(this, sender);
        }

        protected virtual void OnPauseGame(GameContext _, object sender)
        {
        }

        public virtual void ResumeGame(object sender)
        {
            this.gameStatus = GameStatus.PLAYING;
            foreach (var node in this.nodes)
            {
                node.OnResumeGame(sender);
            }

            this.OnResumeGame(this, sender);
        }

        protected virtual void OnResumeGame(GameContext _, object sender)
        {
        }

        public virtual void FinishGame(object sender)
        {
            this.gameStatus = GameStatus.FINISHING;
            foreach (var node in this.nodes)
            {
                node.OnFinishGame(sender);
            }
        }

        protected virtual void OnFinishGame(GameContext _, object sender)
        {
        }

        public void DestroyGame(object sender)
        {
            this.gameStatus = GameStatus.DESTROYING;
            foreach (var node in this.nodes)
            {
                node.OnDestroyGame(sender);
            }
        }

        protected virtual void OnDestroyGame(GameContext _, object sender)
        {
        }

        public void RegisterNode(IGameNode gameNode)
        {
            this.nodeMap.AddByType(gameNode);
            gameNode.OnAttachGame(this);
        }

        public virtual void UnregisterNode(IGameNode gameNode)
        {
            gameNode.OnDetachGame();
            this.nodeMap.RemoveByType(gameNode);
        }

        public T GetNode<T>() where T : IGameNode
        {
            return this.nodeMap.Find<T, IGameNode>();
        }

        public IEnumerable<T> GetNodes<T>() where T : IGameNode
        {
            return this.nodeMap.FindAll<T, IGameNode>();
        }
    }
}