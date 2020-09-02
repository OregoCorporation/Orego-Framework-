#if UNITY_EDITOR
using Elementary;
using OregoFramework.App;

namespace OregoFramework.Unit
{
    public abstract class Tester
    {
        private IElementContext _context;
        
        private IElementContext context
        {
            get
            {
                if (this._context == null)
                {
                    this._context = this.ProvideContext();
                }

                return this._context;
            }
        }

        
        private IApplication _application;

        private IApplication application
        {
            get
            {
                if (this._application == null)
                {
                    this._application = this.context.GetRootElement<IApplication>();
                }

                return this._application;
            }
        }

        private IDatabaseLayer _databaseLayer;

        protected IDatabaseLayer databaseLayer
        {
            get
            {
                if (this._databaseLayer == null)
                {
                    this._databaseLayer = this.application.databaseLayer;
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
                    this._repositoryLayer = this.application.repositoryLayer;
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
                    this._interactorLayer = this.application.interactorLayer;
                }

                return this._interactorLayer;
            }
        }
        
        protected abstract IElementContext ProvideContext();

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