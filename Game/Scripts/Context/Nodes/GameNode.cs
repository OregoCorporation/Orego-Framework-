using UnityEngine;

namespace OregoFramework.Game
{
    /// <inheritdoc cref="IGameNode"/>
    public abstract class GameNode : MonoBehaviour, IGameNode
    {
        /// <summary>
        ///     <para>A parent game system reference.</para>
        /// </summary>
        protected IGameContext gameContext { get; private set; }

        /// <inheritdoc cref="IGameNode.OnRegistered"/>
        public void OnRegistered(IGameContext gameContext)
        {
            this.gameContext = gameContext;
            this.OnRegistered(this, gameContext);
        }

        protected virtual void OnRegistered(GameNode _, IGameContext gameContext)
        {
        }

        /// <inheritdoc cref="IGameNode.OnPrepareGame"/>
        public virtual void OnPrepareGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnReadyGame"/>
        public virtual void OnReadyGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnStartGame"/>
        public virtual void OnStartGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnPauseGame"/>
        public virtual void OnPauseGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnResumeGame"/>
        public virtual void OnResumeGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnFinishGame"/>
        public virtual void OnFinishGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnDestroyGame"/>
        public virtual void OnDestroyGame(object sender)
        {
        }

        /// <inheritdoc cref="IGameNode.OnDestroyGame"/>
        public virtual void OnUnregistered()
        {
        }
    }
}