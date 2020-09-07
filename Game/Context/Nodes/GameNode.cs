using UnityEngine;

namespace OregoFramework.Game
{
    public abstract class GameNode : MonoBehaviour, IGameNode
    {
        protected IGameContext gameContext { get; private set; }

        public void OnAttachGame(IGameContext gameContext)
        {
            this.gameContext = gameContext;
            this.OnAttachGame(this, gameContext);
        }

        protected virtual void OnAttachGame(GameNode _, IGameContext gameContext)
        {
        }

        public void OnPrepareGame(object sender)
        {
            this.OnPrepareGame(this, sender);
        }
        
        protected virtual void OnPrepareGame(GameNode _, object sender)
        {
        }

        public void OnReadyGame(object sender)
        {
            this.OnReadyGame(this, sender);
        }

        protected virtual void OnReadyGame(GameNode _, object sender)
        {
        }

        public void OnStartGame(object sender)
        {
            this.OnStartGame(this, sender);
        }

        protected virtual void OnStartGame(GameNode _, object sender)
        {
        }

        public void OnPauseGame(object sender)
        {
            this.OnPauseGame(this, sender);
        }
        
        protected virtual void OnPauseGame(GameNode _, object sender)
        {
        }

        public void OnResumeGame(object sender)
        {
            this.OnResumeGame(this, sender);
        }

        protected virtual void OnResumeGame(GameNode _, object sender)
        {
        }

        public void OnFinishGame(object sender)
        {
            this.OnFinishGame(this, sender);
        }

        protected virtual void OnFinishGame(GameNode _, object sender)
        {
        }

        public void OnDestroyGame(object sender)
        {
            this.OnDestroyGame(this, sender);
        }

        protected virtual void OnDestroyGame(GameNode _, object sender)
        {
        }

        public void OnDetachGame()
        {
            this.OnDetachGame(this);
        }

        protected virtual void OnDetachGame(GameNode _)
        {
        }
    }
}