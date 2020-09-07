using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    public abstract class DomainElement : Element, IDomainElement
    {
        protected IApplicationFrame applicationFrame { get; private set; }

        protected IInteractorLayer interactorLayer { get; private set; }

        protected IClientLayer clientLayer { get; private set; }

        protected IRepositoryLayer repositoryLayer { get; private set; }

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

        protected T GetInteractor<T>() where T : IInteractor
        {
            return this.interactorLayer.GetInteractor<T>();
        }

        protected IEnumerable<T> GetInteractors<T>() where T : IInteractor
        {
            return this.interactorLayer.GetInteractors<T>();
        }

        protected T GetRepository<T>() where T : IRepository
        {
            return this.repositoryLayer.GetRepository<T>();
        }

        protected IEnumerable<T> GetRepositories<T>() where T : IRepository
        {
            return this.repositoryLayer.GetRepositories<T>();
        }

        protected T GetClient<T>() where T : IClient
        {
            return this.clientLayer.GetClient<T>();
        }

        protected IEnumerable<T> GetClients<T>() where T : IClient
        {
            return this.clientLayer.GetClients<T>();
        }
    }
}