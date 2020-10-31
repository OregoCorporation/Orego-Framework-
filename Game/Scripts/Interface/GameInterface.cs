using UnityEngine;

namespace OregoFramework.Game
{
    /// <inheritdoc cref="IGameInterface"/>
    public abstract class GameInterface : MonoBehaviour, IGameInterface
    {
        /// <summary>
        ///     <para>A parent interface system reference.</para>
        /// </summary>
        protected IGameInterfaceSystem interfaceSystem { get; private set; }

        /// <summary>
        ///     <para>Gets game context reference thats is bounded to parent interface system.</para>
        /// </summary>
        protected IGameContext gameContext
        {
            get { return this.interfaceSystem.gameContext; }
        }

        #region Lifecycle

        /// <inheritdoc cref="IGameInterface.OnRegistered"/>
        public virtual void OnRegistered(IGameInterfaceSystem system)
        {
            this.interfaceSystem = system;
        }

        /// <inheritdoc cref="IGameInterface.OnGamePrepared"/>
        public virtual void OnGamePrepared(object sender)
        {
        }

        /// <inheritdoc cref="IGameInterface.OnGameReady"/>
        public virtual void OnGameReady(object sender)
        {
        }

        /// <inheritdoc cref="IGameInterface.OnGameStarted"/>
        public virtual void OnGameStarted(object sender)
        {
        }

        /// <inheritdoc cref="IGameInterface.OnGamePaused"/>
        public virtual void OnGamePaused(object sender)
        {
        }

        /// <inheritdoc cref="IGameInterface.OnGameResumed"/>
        public virtual void OnGameResumed(object sender)
        {
        }

        /// <inheritdoc cref="IGameInterface.OnGameFinished"/>
        public virtual void OnGameFinished(object sender)
        {
        }

        /// <inheritdoc cref="IGameInterface.OnUnregistered"/>
        public virtual void OnUnregistered()
        {
        }

        #endregion
    }
}