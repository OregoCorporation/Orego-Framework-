using System;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Broadcasts event when a user data is loaded.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadyRepository<out T> : IRepository
    {
        /// <summary>
        ///     <para>Invoke this event when user data is loaded!</para>
        ///     <param name="T">An user data</param>
        /// </summary>
        event Action<T> OnDataReadyEvent;
    }
}