using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <inheritdoc cref="IRepositoryLayer"/>
    [Using]
    public class RepositoryLayer : ElementLayer<IRepository>, IRepositoryLayer
    {
        protected IApplicationFrame applicationFrame { get; private set; }

        protected IDatabaseLayer databaseLayer { get; private set; }

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