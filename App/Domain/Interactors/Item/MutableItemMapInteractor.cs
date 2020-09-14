using System;
using System.Collections.Generic;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A mutable item map interactor class.</para>
    /// </summary>
    /// <typeparam name="K">Dictionary key.</typeparam>
    /// <typeparam name="T">Dictionary value.</typeparam>
    /// <typeparam name="TRepository">Target repository type.</typeparam>
    /// <typeparam name="TData">Loaded data type.</typeparam>
    public abstract class MutableItemMapInteractor<K, T, TRepository, TData> :
        ItemMapInteractor<K, T, TRepository, TData>,
        IMutableItemMapInteractor<K, T>
        where TRepository : ILoadRepository<IEnumerable<TData>>
    {
        #region Event

        /// <inheritdoc cref="IMutableItemMapInteractor{K,T}.OnItemAddedEvent"/>
        public event Action<object, T> OnItemAddedEvent;
        
        /// <inheritdoc cref="IMutableItemMapInteractor{K,T}.OnItemRemovedEvent"/>
        public event Action<object, T> OnItemRemovedEvent;

        #endregion

        /// <inheritdoc cref="IMutableItemMapInteractor{K,T}.NotifyAboutItemAdded"/>
        public void NotifyAboutItemAdded(object sender, T item)
        {
            this.OnItemAddedEvent?.Invoke(sender, item);
        }

        /// <inheritdoc cref="IMutableItemMapInteractor{K,T}.NotifyAboutItemRemoved"/>
        public void NotifyAboutItemRemoved(object sender, T item)
        {
            this.OnItemRemovedEvent?.Invoke(sender, item);
        }
    }
}