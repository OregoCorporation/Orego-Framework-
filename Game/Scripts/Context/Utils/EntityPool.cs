using System.Collections.Generic;
using OregoFramework.Util;

namespace OregoFramework.Game
{
    public abstract class EntityPool<T> : EntityManager<T>
    {
        protected List<T> availableEntities { get; }

        #region Lifecycle

        protected EntityPool()
        {
            this.availableEntities = new List<T>();
        }

        protected sealed override void OnRegisterEntity(T entity)
        {
            this.availableEntities.Add(entity);
            this.OnRegisterEntity(this, entity);
        }

        protected virtual void OnRegisterEntity(EntityPool<T> _, T entity)
        {
        }

        protected sealed override void OnUnregisterEntity(T entity)
        {
            this.availableEntities.Remove(entity);
            this.OnUnregisterEntity(this, entity);
        }

        protected virtual void OnUnregisterEntity(EntityPool<T> _, T entity)
        {
        }

        #endregion

        public void PushEntity(T entity)
        {
            this.availableEntities.Add(entity);
        }

        public T PopEntity()
        {
            var next = this.PeekEntity();
            this.availableEntities.Remove(next);
            return next;
        }

        public T PeekEntity()
        {
            return this.availableEntities[Int.ZERO];
        }

        public int AvailableEntityCount()
        {
            return this.availableEntities.Count;
        }
    }
}