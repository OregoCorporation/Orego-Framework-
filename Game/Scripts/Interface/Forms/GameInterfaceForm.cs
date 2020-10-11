using System;
using UnityEngine;

namespace OregoFramework.Game
{
    public abstract class GameInterfaceForm : MonoBehaviour, IGameInterfaceForm
    {
        protected IGameInterface gameInterface { get; private set; }

        private IGameContext _gameContext;

        protected IGameContext gameContext
        {
            get
            {
                if (this._gameContext != null)
                {
                    return this._gameContext;
                }

                if (this.gameInterface is null)
                {
                    throw new Exception("Game interface is not provided!");
                }
                
                this._gameContext = this.gameInterface.currentGameContext;
                return this._gameContext;
            }
        }

        #region Lifecycle

        public virtual void OnRegistered(IGameInterface gameInterface)
        {
            this.gameInterface = gameInterface;
        }

        public virtual void OnGamePrepared(object sender)
        {
        }

        public virtual void OnGameReady(object sender)
        {
        }

        public virtual void OnGameStarted(object sender)
        {
        }

        public virtual void OnGamePaused(object sender)
        {
        }

        public virtual void OnGameResumed(object sender)
        {
        }

        public virtual void OnGameFinished(object sender)
        {
        }

        public virtual void OnUnregistered()
        {
        }

        #endregion
    }
}