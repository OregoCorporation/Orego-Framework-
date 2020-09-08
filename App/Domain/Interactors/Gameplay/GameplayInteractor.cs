using System;
using OregoFramework.Game;

namespace OregoFramework.App
{
    /// <summary>
    ///     Maintains a game context.
    /// </summary>
    /// <typeparam name="T">Game context type.</typeparam>
    public abstract class GameplayInteractor<T> : Interactor 
        where T : IGameContext
    {
        #region Event

        public abstract event Action<object, T> OnGameCreatedEvent;

        public abstract event Action<object, T> OnGameDestroyedEvent;

        #endregion

        public T gameContext { get; protected set; }

        public abstract void CreateGame(object sender);
        
        public abstract void DestroyGame(object sender);
    }
}