using UnityEngine;

namespace OregoFramework.Game
{
    public abstract class GameView : MonoBehaviour, IGameView
    {
        protected IGameInterface gameInterface { get; private set; }

        protected T GetGameContext<T>() where T : IGameContext
        {
            return this.gameInterface.GetGameContext<T>();
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