using System;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Base item interactor class.</para>
    /// </summary>
    /// 
    /// <typeparam name="T">An item type.</typeparam>
    /// <typeparam name="TRepository">Target repository.</typeparam>
    /// <typeparam name="TData">Loaded data type.</typeparam>
    public abstract class ItemInteractor<T, TRepository, TData> : Interactor,
        IItemInteractor<T>
        where TRepository : ILoadRepository<TData>
    {
        #region Event

        /// <summary>
        ///     <para>Called when item state is initialized.</para>
        /// </summary>
        public event Action OnInitializedEvent;

        /// <inheritdoc cref="IItemInteractor{T}.OnItemChangedEvent"/>
        public event Action<object, T> OnItemChangedEvent;

        #endregion

        /// <summary>
        ///     <para>A load repository reference <see cref="ILoadRepository{T}"/>
        ///     this interactor is working with.</para>
        /// </summary>
        protected TRepository repository { get; private set; }

        protected sealed override void OnPrepare(DomainElement _)
        {
            this.repository = this.ProvideRepository();
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(ItemInteractor<T, TRepository, TData> _)
        {
        }

        /// <summary>
        ///     <para>Provides target repository reference.</para>
        /// </summary>
        /// 
        /// <returns>A repository reference.</returns>
        protected virtual TRepository ProvideRepository()
        {
            return this.repository = this.GetRepository<TRepository>();
        }

        protected sealed override void OnReady(Element _)
        {
            this.repository.OnDataLoadedEvent += this.OnRepositoryDataLoaded;
            this.OnReady(this);
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

        /// <inheritdoc cref="IItemInteractor{T}.NotifyAboutItemChanged"/>
        public void NotifyAboutItemChanged(object sender, T item)
        {
            this.OnItemChangedEvent?.Invoke(sender, item);
        }

        #region RepositoryCallback

        protected void OnRepositoryDataLoaded(TData data)
        {
            this.Initialize(data);
            this.OnInitializedEvent?.Invoke();
        }

        /// <summary>
        ///     Initializes an item state. 
        /// </summary>
        /// <param name="data">Loaded user data.</param>
        protected abstract void Initialize(TData data);

        #endregion
    }
}