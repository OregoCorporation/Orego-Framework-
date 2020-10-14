using System;
using System.Collections.Generic;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A mutable item map interactor class.</para>
    /// </summary>
    /// <typeparam name="K">Dictionary key.</typeparam>
    /// <typeparam name="V">Dictionary value.</typeparam>
    /// <typeparam name="TRepository">Target repository type.</typeparam>
    /// <typeparam name="TData">Loaded data type.</typeparam>
    public abstract class MutableItemMapInteractor<K, V, TRepository, TData> :
        ItemMapInteractor<K, V, TRepository, TData>,
        IMutableItemMapInteractor<K, V>
        where TRepository : ILoadRepository<IEnumerable<TData>>
    {
        #region Event

        /// <inheritdoc cref="IMutableItemMapInteractor{K,T}.OnItemAddedEvent"/>
        public event Action<object, V> OnItemAddedEvent;
        
        /// <inheritdoc cref="IMutableItemMapInteractor{K,T}.OnItemRemovedEvent"/>
        public event Action<object, V> OnItemRemovedEvent;

        #endregion

        /// <inheritdoc cref="IMutableItemMapInteractor{K,T}.NotifyAboutItemAdded"/>
        public void NotifyAboutItemAdded(object sender, V item)
        {
            this.OnItemAddedEvent?.Invoke(sender, item);
        }

        /// <inheritdoc cref="IMutableItemMapInteractor{K,T}.NotifyAboutItemRemoved"/>
        public void NotifyAboutItemRemoved(object sender, V item)
        {
            this.OnItemRemovedEvent?.Invoke(sender, item);
        }
    }
}