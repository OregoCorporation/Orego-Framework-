using System;
using Elementary;

namespace OregoFramework.App
{
    public abstract class ItemInteractor<T, TRepository, TData> : Interactor,
        IItemInteractor<T>
        where TRepository : ILoadRepository<TData>
    {
        #region Event

        public event Action<object> OnInitializedEvent;

        public event Action<object, T> OnItemChangedEvent;

        #endregion

        protected TRepository repository { get; private set; }

        protected sealed override void OnPrepare(DomainElement _)
        {
            this.repository = this.ProvideRepository();
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(ItemInteractor<T, TRepository, TData> _)
        {
        }

        protected virtual TRepository ProvideRepository()
        {
            return this.repository = this.GetRepository<TRepository>();
        }

        protected sealed override void OnReady(Element _)
        {
            this.repository.OnDataLoadedEvent += this.OnRepositoryDataLoaded;
            this.OnPrepare(this);
        }

        protected virtual void OnReady(ItemInteractor<T, TRepository, TData> _)
        {
        }

        protected sealed override void OnFinish(Element _)
        {
            this.repository.OnDataLoadedEvent -= this.OnRepositoryDataLoaded;
            this.OnFinish(this);
        }

        protected virtual void OnFinish(ItemInteractor<T, TRepository, TData> _)
        {
        }

        public void NotifyAboutItemChanged(object sender, T item)
        {
            this.OnItemChangedEvent?.Invoke(sender, item);
        }

        protected void NotifyAboutInitialized(object sender)
        {
            this.OnInitializedEvent?.Invoke(sender);
        }

        #region RepositoryCallback

        protected void OnRepositoryDataLoaded(TData data)
        {
            this.Initialize(data);
            this.NotifyAboutInitialized(this);
        }

        protected abstract void Initialize(TData data);

        #endregion
    }
}