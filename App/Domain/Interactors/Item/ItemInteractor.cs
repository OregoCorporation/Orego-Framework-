using System;

namespace OregoFramework.App
{
    public abstract class ItemInteractor<T, TRepository, TData> : Interactor,
        IItemInteractor<T>
        where TRepository : IReadyRepository<TData>
    {
        #region Event

        public event Action<object> OnInitializedEvent;

        public event Action<object, T> OnItemChangedEvent;

        #endregion

        protected TRepository repository { get; private set; }

        public override void OnPrepare()
        {
            base.OnPrepare();
            this.repository = this.FetchRepository();
        }

        protected virtual TRepository FetchRepository()
        {
            return this.repository = this.GetRepository<TRepository>();
        }

        #region OnReady

        public override void OnReady()
        {
            base.OnReady();
            this.repository.OnDataReadyEvent += this.OnRepositoryDataReady;
        }

        #endregion

        #region OnStop

        public override void OnFinish()
        {
            base.OnFinish();
            this.repository.OnDataReadyEvent -= this.OnRepositoryDataReady;
        }

        #endregion

        public void NotifyAboutObjectChanged(object sender, T item)
        {
            this.OnItemChangedEvent?.Invoke(sender, item);
        }

        protected void NotifyAboutObjectDataInitialized(object sender)
        {
            this.OnInitializedEvent?.Invoke(sender);
        }

        #region RepositoryCallback

        protected void OnRepositoryDataReady(TData data)
        {
            this.InitializeState(data);
            this.NotifyAboutObjectDataInitialized(this);
        }

        protected abstract void InitializeState(TData data);

        #endregion
    }
}