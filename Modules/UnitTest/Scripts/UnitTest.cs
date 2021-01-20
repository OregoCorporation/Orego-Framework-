#if UNITY_EDITOR
using Elementary;
using OregoFramework.App;

namespace OregoFramework.Module
{
    public abstract class UnitTest
    {
        private IElementContext _elementaryContext;
        
        private IElementContext elementaryContext
        {
            get
            {
                if (this._elementaryContext == null)
                {
                    this._elementaryContext = this.ProvideElementaryContext();
                }

                return this._elementaryContext;
            }
        }

        
        private IApplicationFrame _applicationFrame;

        private IApplicationFrame applicationFrame
        {
            get
            {
                if (this._applicationFrame == null)
                {
                    this._applicationFrame = this.elementaryContext.GetRootElement<IApplicationFrame>();
                }

                return this._applicationFrame;
            }
        }

        private IDatabaseLayer _databaseLayer;

        protected IDatabaseLayer databaseLayer
        {
            get
            {
                if (this._databaseLayer == null)
                {
                    this._databaseLayer = this.applicationFrame.DatabaseLayer;
                }

                return this._databaseLayer;
            }
        }

        private IRepositoryLayer _repositoryLayer;

        protected IRepositoryLayer repositoryLayer
        {
            get
            {
                if (this._repositoryLayer == null)
                {
                    this._repositoryLayer = this.applicationFrame.RepositoryLayer;
                }

                return this._repositoryLayer;
            }
        }

        private IInteractorLayer _interactorLayer;

        protected IInteractorLayer interactorLayer
        {
            get
            {
                if (this._interactorLayer == null)
                {
                    this._interactorLayer = this.applicationFrame.InteractorLayer;
                }

                return this._interactorLayer;
            }
        }
        
        protected abstract IElementContext ProvideElementaryContext();

        protected T GetDatabase<T>() where T : IDatabase
        {
            return this.databaseLayer.GetDatabase<T>();
        }

        protected T GetRepository<T>() where T : IRepository
        {
            return this.repositoryLayer.GetRepository<T>();
        }

        protected T GetInteractor<T>() where T : IInteractor
        {
            return this.interactorLayer.GetInteractor<T>();
        }
    }
}
#endif