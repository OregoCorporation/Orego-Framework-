using System;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Keeps event when a user data is loaded.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILoadRepository<out T> : IRepository
    {
        /// <summary>
        ///     <para>Invoke this event when user data is loaded.</para>
        /// </summary>
        /// <param name="data">Loaded data.</param>
        event Action<T> OnDataLoadedEvent;
    }
}