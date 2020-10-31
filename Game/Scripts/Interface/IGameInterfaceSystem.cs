using System.Collections.Generic;

namespace OregoFramework.Game
{
    /// <summary>
    ///     <para>Keeps unique game interfaces.</para>
    /// </summary>
    public interface IGameInterfaceSystem
    {
        /// <summary>
        ///     <para>An attached game context.</para>
        /// </summary>
        IGameContext gameContext { get; }

        /// <summary>
        ///     <para>Adds an unique interface into this system.</para>
        /// </summary>
        /// <param name="gameInterface">An interface instance.</param>
        void RegisterInterface(IGameInterface gameInterface);

        /// <summary>
        ///     <para>Removes an unique interface from this system.</para>
        /// </summary>
        /// <param name="gameInterface">An interface instance.</param>
        void UnregisterInterface(IGameInterface gameInterface);

        /// <summary>
        ///     <para>Gets a registered unique interface by type.</para>
        /// </summary>
        /// <typeparam name="T">Interface type.</typeparam>
        T GetInterface<T>() where T : IGameInterface;

        /// <summary>
        ///     <para>Gets registered unique interfaces by base type.</para>
        /// </summary>
        /// <typeparam name="T">Base interface type.</typeparam>
        IEnumerable<T> GetInterfaces<T>() where T : IGameInterface;
    }
}