using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Works with data entities.</para>
    /// </summary>
    public interface IRepoElement : IElement
    {
    }
    
    /// <inheritdoc cref="IRepoElement"/>
    public abstract class RepoElement : Element, IRepoElement
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame Application { get; private set; }

        /// <summary>
        ///     <para>A repository layer reference.</para>
        /// </summary>
        protected IRepositoryLayer RepositoryLayer { get; private set; }

        /// <summary>
        ///     <para>A database layer reference.</para>
        /// </summary>
        protected IDatabaseLayer DatabaseLayer { get; private set; }

        /// <summary>
        ///     <para>A client layer reference.</para>
        /// </summary>
        protected IClientLayer ClientLayer { get; private set; }

        protected override void OnPrepare()
        {
            base.OnPrepare();
            this.Application = this.GetRootElement<IApplicationFrame>();
            this.RepositoryLayer = this.Application.RepositoryLayer;
            this.DatabaseLayer = this.Application.DatabaseLayer;
            this.ClientLayer = this.Application.ClientLayer;
        }
        
        /// <inheritdoc cref="IDatabaseLayer.GetDatabase{T}"/>
        protected T GetDatabase<T>() where T : IDatabase
        {
            return this.DatabaseLayer.GetDatabase<T>();
        }

        /// <inheritdoc cref="IClientLayer.GetClient{T}"/>
        protected T GetClient<T>() where T : IClient
        {
            return this.ClientLayer.GetClient<T>();
        }
    }
}