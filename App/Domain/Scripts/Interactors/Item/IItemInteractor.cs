using System;

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
}