using System.Collections;
using System.Collections.Generic;

namespace OregoFramework.Game
{
    /// <summary>
    ///     <para>Manages game entity collection.</para>
    /// </summary>
    /// <typeparam name="T">Game entity type.</typeparam>
    public interface IEntityManager<T> : IGameNode
    {
        /// <summary>
        ///     <para>Adds a game entity intoo collection.</para>
        /// </summary>
        /// <param name="entity">Game entity instance.</param>
        void RegisterEntity(T entity);

        /// <summary>
        ///     <para>Removes a game entity from collection</para>
        /// </summary>
        /// <param name="entity">Game entity instance.</param>
        void UnregisterEntity(T entity);

        /// <summary>
        ///     <para>Gets a game entity array of "T" type.</para>
        /// </summary>
        IEnumerable<T> GetEntities();
        
        /// <summary>
        ///     <para>Gets an entity count of collection.</para>
        /// </summary>
        int GetEntityCount();
    }
    
    /// <inheritdoc cref="IEntityManager{T}"/>
    public abstract class EntityManager<T> : GameNode, IEntityManager<T>, IEnumerable<T>
    {
        /// <summary>
        ///     <para>Registered game entities.</para>
        /// </summary>
        protected HashSet<T> registeredEntities { get; }

        protected EntityManager()
        {
            this.registeredEntities = new HashSet<T>();
        }

        /// <inheritdoc cref="IEntityManager{T}.RegisterEntity"/>
        public void RegisterEntity(T entity)
        {
            this.registeredEntities.Add(entity);
            this.OnRegisterEntity(entity);
        }

        protected virtual void OnRegisterEntity(T item)
        {
        }

        /// <inheritdoc cref="IEntityManager{T}.UnregisterEntity"/>
        public void UnregisterEntity(T entity)
        {
            this.registeredEntities.Remove(entity);
            this.OnUnregisterEntity(entity);
        }

        protected virtual void OnUnregisterEntity(T item)
        {
        }

        /// <inheritdoc cref="IEntityManager{T}.GetEntities"/>
        public virtual IEnumerable<T> GetEntities()
        {
            return this.registeredEntities;
        }

        public int GetEntityCount()
        {
            return this.registeredEntities.Count;
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            foreach (var entity in this.registeredEntities)
            {
                yield return entity;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}