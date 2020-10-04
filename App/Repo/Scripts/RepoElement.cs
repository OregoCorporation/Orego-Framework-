using Elementary;

namespace OregoFramework.App
{
    /// <inheritdoc cref="IRepoElement"/>
    public abstract class RepoElement : Element, IRepoElement
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame applicationFrame { get; private set; }

        /// <summary>
        ///     <para>A repository layer reference.</para>
        /// </summary>
        protected IRepositoryLayer repositoryLayer { get; private set; }

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
            this.repositoryLayer = this.applicationFrame.repositoryLayer;
            this.databaseLayer = this.applicationFrame.databaseLayer;
            this.clientLayer = this.applicationFrame.clientLayer;
            this.OnPrepare(this);
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