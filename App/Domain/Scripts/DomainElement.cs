using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Works with business logic.</para>
    /// </summary>
    public interface IDomainElement : IElement
    {
    }
    
    /// <inheritdoc cref="IDomainElement"/>
    public abstract class DomainElement : Element, IDomainElement
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame Application { get; private set; }
        
        /// <summary>
        ///     <para>A client layer reference.</para>
        /// </summary>
        protected IClientLayer ClientLayer { get; private set; }

        /// <summary>
        ///     <para>A repository layer reference.</para>
        /// </summary>
        protected IRepositoryLayer RepositoryLayer { get; private set; }
        
        /// <summary>
        ///     <para>A interactor layer reference.</para>
        /// </summary>
        protected IInteractorLayer InteractorLayer { get; private set; }
        
        protected override void OnPrepare()
        {
            base.OnPrepare();
            this.Application = this.GetRootElement<IApplicationFrame>();
            this.ClientLayer = this.Application.ClientLayer;
            this.RepositoryLayer = this.Application.RepositoryLayer;
            this.InteractorLayer = this.Application.InteractorLayer;
        }
        
        /// <inheritdoc cref="IClientLayer.GetClient{T}"/>
        protected T GetClient<T>() where T : IClient
        {
            return this.ClientLayer.GetClient<T>();
        }

        /// <inheritdoc cref="IClientLayer.GetClients{T}"/>
        protected IEnumerable<T> GetClients<T>() where T : IClient
        {
            return this.ClientLayer.GetClients<T>();
        }

        /// <inheritdoc cref="IRepositoryLayer.GetRepository{T}"/>
        protected T GetRepository<T>() where T : IRepository
        {
            return this.RepositoryLayer.GetRepository<T>();
        }

        /// <inheritdoc cref="IRepositoryLayer.GetRepositories{T}"/>
        protected IEnumerable<T> GetRepositories<T>() where T : IRepository
        {
            return this.RepositoryLayer.GetRepositories<T>();
        }

        /// <inheritdoc cref="IInteractor.GetInteractor{T}"/>
        protected T GetInteractor<T>() where T : IInteractor
        {
            return this.InteractorLayer.GetInteractor<T>();
        }

        /// <inheritdoc cref="IInteractor.GetInteractors{T}"/>
        protected IEnumerable<T> GetInteractors<T>() where T : IInteractor
        {
            return this.InteractorLayer.GetInteractors<T>();
        }
    }
}