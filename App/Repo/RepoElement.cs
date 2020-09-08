using Elementary;

namespace OregoFramework.App
{
    /// <inheritdoc cref="IRepoElement"/>
    public abstract class RepoElement : Element, IRepoElement
    {
        protected IApplicationFrame applicationFrame { get; private set; }

        protected IRepositoryLayer repositoryLayer { get; private set; }

        protected IDatabaseLayer databaseLayer { get; private set; }

        protected IClientLayer clientLayer { get; private set; }

        protected sealed override void OnPrepare(Element _)
        {
            this.applicationFrame = this.GetRootElement<IApplicationFrame>();
            this.repositoryLayer = this.applicationFrame.repositoryLayer;
            this.databaseLayer = this.applicationFrame.databaseLayer;
            this.clientLayer = this.applicationFrame.clientLayer;
        }

        protected virtual void OnPrepare(RepoElement _)
        {
        }

        /// <inheritdoc cref="IDatabaseLayer.GetDatabase{T}"/>
        protected T GetDatabase<T>() where T : IDatabase
        {
            return this.databaseLayer.GetDatabase<T>();
        }

        /// <inheritdoc cref="IClientLayer.GetClient{T}"/>
        protected T GetClient<T>() where T : IClient
        {
            return this.clientLayer.GetClient<T>();
        }
    }
}