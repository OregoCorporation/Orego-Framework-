using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <inheritdoc cref="IRepositoryLayer"/>
    [Using]
    public class RepositoryLayer : ElementLayer<IRepository>, IRepositoryLayer
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame applicationFrame { get; private set; }

        /// <summary>
        ///     <para>A database layer reference.</para>
        /// </summary>
        protected IDatabaseLayer databaseLayer { get; private set; }

        /// <summary>
        ///     <para>A client layer reference.</para>
        /// </summary>
        protected IClientLayer clientLayer { get; private set; }

        protected sealed override void OnPrepare(Element _)
        {
            this.applicationFrame = this.GetRootElement<IApplicationFrame>();
            this.databaseLayer = this.applicationFrame.databaseLayer;
            this.clientLayer = this.applicationFrame.clientLayer;
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(RepositoryLayer _)
        {
        }

        /// <inheritdoc cref="IRepositoryLayer.GetRepository{T}"/>
        public T GetRepository<T>() where T : IRepository
        {
            return this.GetElement<T>();
        }

        /// <inheritdoc cref="IRepositoryLayer.GetRepositories{T}"/>
        public IEnumerable<T> GetRepositories<T>() where T : IRepository
        {
            return this.GetElements<T>();
        }
    }
}