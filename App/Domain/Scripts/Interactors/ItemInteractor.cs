using System;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Controls an item.</para>
    /// </summary>
    /// 
    /// <typeparam name="T">An item type.</typeparam>
    public interface IItemInteractor<T> : IInteractor
    {
        /// <summary>
        ///     <para>Invoke this event when an item has changed.</para>
        /// </summary>
        /// 
        /// <param name="sender">Who is calling this method</param>
        /// <param name="item">An item reference.</param>
        event Action<object, T> OnItemChangedEvent;

        /// <summary>
        ///     <para>Broadcasts event that item has changed.</para>
        ///     <para>Insert event invokation <see cref="OnItemChangedEvent"/> into body of this method.</para>
        /// </summary>
        /// 
        /// <param name="sender">Who is calling this method.</param>
        /// <param name="item">An item reference.</param>
        void NotifyAboutItemChanged(object sender, T item);
    }
    
    /// <summary>
    ///     <para>A base item interactor class.</para>
    /// </summary>
    /// 
    /// <typeparam name="T">An item type.</typeparam>
    /// <typeparam name="TRepository">Target repository type.</typeparam>
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
        protected TRepository Repository { get; private set; }

        protected override void OnPrepare()
        {
            base.OnPrepare();
            this.Repository = this.ProvideRepository();
        }
        
        /// <summary>
        ///     <para>Provides target repository reference.</para>
        /// </summary>
        /// 
        /// <returns>A repository reference.</returns>
        protected virtual TRepository ProvideRepository()
        {
            return this.Repository = this.GetRepository<TRepository>();
        }

        protected override void OnReady()
        {
            base.OnReady();
            this.Repository.OnDataLoadedEvent += this.OnRepositoryDataLoaded;
        }
        
        protected override void OnFinish()
        {
            base.OnFinish();
            this.Repository.OnDataLoadedEvent -= this.OnRepositoryDataLoaded;
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