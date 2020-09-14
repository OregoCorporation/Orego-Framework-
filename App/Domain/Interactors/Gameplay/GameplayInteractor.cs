using System;
using OregoFramework.Game;

namespace OregoFramework.App
{
    /// <summary>
    ///     Controls a game context.
    /// </summary>
    /// <typeparam name="T">Game context type.</typeparam>
    public abstract class GameplayInteractor<T> : Interactor 
        where T : IGameContext
    {
        #region Event

        /// <summary>
        ///     <para>Invoke this event when a game context is created.</para>
        /// </summary>
        public abstract event Action<object, T> OnGameCreatedEvent;

        /// <summary>
        ///     <para>Invoke this event when a game context is disposed.</para>
        /// </summary>
        public abstract event Action<object, T> OnGameDisposedEvent;

        #endregion

        /// <summary>
        ///     <para>Current game context.</para>
        /// </summary>
        public T gameContext { get; protected set; }

        /// <summary>
        ///     <para>Creates a game context.</para>
        /// </summary>
        /// <param name="sender">Who is calling this method.</param>
        public abstract void CreateGame(object sender);
        
        /// <summary>
        ///     <para>Dispose a game context.</para>
        /// </summary>
        /// <param name="sender">Who is calling this method.</param>
        public abstract void DisposeGame(object sender);
    }
}