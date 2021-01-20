using System;
using System.Collections.Generic;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Controls a mutable item dictionary.</para>
    /// </summary>
    /// 
    /// <typeparam name="K">Dictionary key.</typeparam>
    /// <typeparam name="V">Dictionary value.</typeparam>
    public interface IMutableItemMapInteractor<in K, V> : IItemMapInteractor<K, V>
    {
        #region Event

        /// <summary>
        ///     <para>Invoke this event when an item has added into dictionary.</para>
        /// </summary>
        /// 
        /// <param name="sender">Who is calling this method</param>
        /// <param name="item">An item reference.</param>
        event Action<object, V> OnItemAddedEvent;

        /// <summary>
        ///     <para>Invoke this event when an item has removed from dictionary.</para>
        /// </summary>
        /// 
        /// <param name="sender">Who is calling this method</param>
        /// <param name="item">An item reference.</param>
        event Action<object, V> OnItemRemovedEvent;

        #endregion

        /// <summary>
        ///     <para>Broadcasts event that item has added into dictionary.</para>
        ///     <para>Insert event invokation <see cref="OnItemAddedEvent"/> into body of this method.</para>
        /// </summary>
        /// 
        /// <param name="sender">Who is calling this method.</param>
        /// <param name="item">An item reference.</param>
        void NotifyAboutItemAdded(object sender, V item);

        /// <summary>
        ///     <para>Broadcasts event that item has removed from dictionary.</para>
        ///     <para>Insert event invokation <see cref="OnItemRemovedEvent"/> into body of this method.</para>
        /// </summary>
        /// 
        /// <param name="sender">Who is calling this method.</param>
        /// <param name="item">An item reference.</param>
        void NotifyAboutItemRemoved(object sender, V item);
    }
    
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