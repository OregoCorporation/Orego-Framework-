using System;
using System.Collections.Generic;
using OregoFramework.Util;

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
    }
    
    /// <inheritdoc cref="IGameNodeLayer"/>
    public abstract class GameNodeLayer : GameNodeContainer, IGameNodeLayer
    {
        /// <summary>
        ///     <para>Map of unique registered game components.
        ///     Key: a node type, Value: a node instance.</para>
        /// </summary>
        private readonly Dictionary<Type, IGameNode> nodeMap;
        
        protected GameNodeLayer()
        {
            this.nodeMap = new Dictionary<Type, IGameNode>();
        }

        /// <inheritdoc cref="IGameNodeLayer.GetNode{T}"/>
        public T GetNode<T>() where T : IGameNode
        {
            return this.nodeMap.Find<T, IGameNode>();
        }

        /// <inheritdoc cref="IGameNodeLayer.GetNodes{T}"/>
        public IEnumerable<T> GetNodes<T>() where T : IGameNode
        {
            return this.nodeMap.FindAll<T, IGameNode>();
        }

        public override void RegisterNode(IGameNode gameNode)
        {
            base.RegisterNode(gameNode);
            var type = gameNode.GetType();
            this.nodeMap.Add(type, gameNode);
        }

        public override void UnregisterNode(IGameNode gameNode)
        {
            var type = gameNode.GetType();
            this.nodeMap.Remove(type);
            base.UnregisterNode(gameNode);
        }
    }
}