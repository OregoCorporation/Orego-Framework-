using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <inheritdoc cref="IDomainElement"/>
    public abstract class DomainElement : Element, IDomainElement
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame applicationFrame { get; private set; }
        
        /// <summary>
        ///     <para>A client layer reference.</para>
        /// </summary>
        protected IClientLayer clientLayer { get; private set; }

        /// <summary>
        ///     <para>A repository layer reference.</para>
        /// </summary>
        protected IRepositoryLayer repositoryLayer { get; private set; }
        
        /// <summary>
        ///     <para>A interactor layer reference.</para>
        /// </summary>
        protected IInteractorLayer interactorLayer { get; private set; }
        
        protected sealed override void OnPrepare(Element _)
        {
            this.applicationFrame = this.GetRootElement<IApplicationFrame>();
            this.clientLayer = this.applicationFrame.clientLayer;
            this.repositoryLayer = this.applicationFrame.repositoryLayer;
            this.interactorLayer = this.applicationFrame.interactorLayer;
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(DomainElement _)
        {
        }

        /// <inheritdoc cref="IClientLayer.GetClient{T}"/>
        protected T GetClient<T>() where T : IClient
        {
            return this.clientLayer.GetClient<T>();
        }

        /// <inheritdoc cref="IClientLayer.GetClients{T}"/>
        protected IEnumerable<T> GetClients<T>() where T : IClient
        {
            return this.clientLayer.GetClients<T>();
        }

        /// <inheritdoc cref="IRepositoryLayer.GetRepository{T}"/>
        protected T GetRepository<T>() where T : IRepository
        {
            return this.repositoryLayer.GetRepository<T>();
        }

        /// <inheritdoc cref="IRepositoryLayer.GetRepositories{T}"/>
        protected IEnumerable<T> GetRepositories<T>() where T : IRepository
        {
            return this.repositoryLayer.GetRepositories<T>();
        }

        /// <inheritdoc cref="IInteractor.GetInteractor{T}"/>
        protected T GetInteractor<T>() where T : IInteractor
        {
            return this.interactorLayer.GetInteractor<T>();
        }

        /// <inheritdoc cref="IInteractor.GetInteractors{T}"/>
        protected IEnumerable<T> GetInteractors<T>() where T : IInteractor
        {
            return this.interactorLayer.GetInteractors<T>();
        }
    }
}