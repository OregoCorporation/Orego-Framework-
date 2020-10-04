using UnityEngine;

namespace OregoFramework.Game
{
    public abstract class GameInterface : MonoBehaviour, IGameInterface
    {
        public IGameContext currentGameContext { get; private set; }

        public virtual void BindGameContext(IGameContext gameContext)
        {
            this.currentGameContext = gameContext;
            this.currentGameContext.OnGamePreparedEvent += this.OnGamePrepared;
            this.currentGameContext.OnGameReadyEvent += this.OnGameReady;
            this.currentGameContext.OnGameStartedEvent += this.OnGameStarted;
            this.currentGameContext.OnGamePausedEvent += this.OnGamePaused;
            this.currentGameContext.OnGameResumedEvent += this.OnGameResumed;
            this.currentGameContext.OnGameFinishedEvent += this.OnGameFinished;
        }

        public virtual void UnbindGameContext()
        {
            this.currentGameContext.OnGamePreparedEvent -= this.OnGamePrepared;
            this.currentGameContext.OnGameReadyEvent -= this.OnGameReady;
            this.currentGameContext.OnGameStartedEvent -= this.OnGameStarted;
            this.currentGameContext.OnGamePausedEvent -= this.OnGamePaused;
            this.currentGameContext.OnGameResumedEvent -= this.OnGameResumed;
            this.currentGameContext.OnGameFinishedEvent -= this.OnGameFinished;
            this.currentGameContext = null;
        }

        protected virtual void OnGamePrepared(object sender)
        {
        }

        protected virtual void OnGameReady(object sender)
        {
        }

        protected virtual void OnGameStarted(object sender)
        {
        }

        protected virtual void OnGamePaused(object sender)
        {
        }

        protected virtual void OnGameResumed(object sender)
        {
        }

        protected virtual void OnGameFinished(object sender)
        {
        }
    }
}