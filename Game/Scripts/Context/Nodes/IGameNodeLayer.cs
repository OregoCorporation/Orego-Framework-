using System.Collections.Generic;

namespace OregoFramework.Game
{
    /// <summary>
    ///     <para>Contains unique game nodes <see cref="IGameNode"/>.</para>
    /// </summary>
    public interface IGameNodeLayer
    {
        /// <summary>
        ///     <para>Gets a registered unique node by type.</para>
        /// </summary>
        /// <typeparam name="T">Node type.</typeparam>
        /// <returns>Instance.</returns>
        T GetNode<T>() where T : IGameNode;

        /// <summary>
        ///     <para>Gets registered unique nodes by base type.</para>
        /// </summary>
        /// <typeparam name="T">Base node type.</typeparam>
        /// <returns>Group of nodes.</returns>
        IEnumerable<T> GetNodes<T>() where T : IGameNode;

        /// <summary>
        ///     <para>Registers unique node into layer.</para>
        /// </summary>
        /// <param name="gameNode">Registered node.</param>
        void RegisterNode(IGameNode gameNode);
        
        /// <summary>
        ///     <para>Unregisters unique node from layer.</para>
        /// </summary>
        /// <param name="gameNode">Unregistered node.</param>
        void UnregisterNode(IGameNode gameNode);
    }
}