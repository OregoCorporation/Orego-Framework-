using System.Collections.Generic;

namespace OregoFramework.Game
{
    public interface IGameNodeContainer
    {
        /// <summary>
        ///     <para>Registers node into container.</para>
        /// </summary>
        /// <param name="gameNode">Registered node.</param>
        void RegisterNode(IGameNode gameNode);

        /// <summary>
        ///     <para>Unregisters node from container.</para>
        /// </summary>
        /// <param name="gameNode">Unregistered node.</param>
        void UnregisterNode(IGameNode gameNode);
    }
    
    public class GameNodeContainer : GameNode, IGameNodeContainer
    {
        private readonly HashSet<IGameNode> registeredNodes;

        protected GameNodeContainer()
        {
            this.registeredNodes = new HashSet<IGameNode>();
        }

        /// <inheritdoc cref="IGameNode.OnPrepareGame"/>
        protected override void OnPrepareGame(object sender)
        {
            base.OnPrepareGame(sender);
            foreach (var node in this.registeredNodes)
            {
                node.OnPrepareGame(sender);
            }
        }

        /// <inheritdoc cref="IGameNode.OnReadyGame"/>
        protected override void OnReadyGame(object sender)
        {
            base.OnReadyGame(sender);
            foreach (var node in this.registeredNodes)
            {
                node.OnReadyGame(sender);
            }
        }

        /// <inheritdoc cref="IGameNode.OnStartGame"/>
        protected override void OnStartGame(object sender)
        {
            base.OnStartGame(sender);
            foreach (var node in this.registeredNodes)
            {
                node.OnStartGame(sender);
            }
        }

        /// <inheritdoc cref="IGameNode.OnPauseGame"/>
        protected override void OnPauseGame(object sender)
        {
            base.OnPauseGame(sender);
            foreach (var node in this.registeredNodes)
            {
                node.OnPauseGame(sender);
            }
        }

        /// <inheritdoc cref="IGameNode.OnResumeGame"/>
        protected override void OnResumeGame(object sender)
        {
            base.OnResumeGame(sender);
            foreach (var node in this.registeredNodes)
            {
                node.OnResumeGame(sender);
            }
        }

        /// <inheritdoc cref="IGameNode.OnFinishGame"/>
        protected override void OnFinishGame(object sender)
        {
            base.OnFinishGame(sender);
            foreach (var node in this.registeredNodes)
            {
                node.OnFinishGame(sender);
            }
        }

        protected override void OnDestroyGame(object sender)
        {
            foreach (var node in this.registeredNodes)
            {
                node.OnDestroyGame(sender);
            }

            base.OnDestroyGame(sender);
        }

        /// <inheritdoc cref="IGameNodeLayer.RegisterNode"/>
        public virtual void RegisterNode(IGameNode gameNode)
        {
            this.registeredNodes.Add(gameNode);
            gameNode.OnRegistered(this, this.GameContext);
        }

        /// <inheritdoc cref="IGameNodeLayer.UnregisterNode"/>
        public virtual void UnregisterNode(IGameNode gameNode)
        {
            gameNode.OnUnregistered();
            this.registeredNodes.Remove(gameNode);
        }
    }
}