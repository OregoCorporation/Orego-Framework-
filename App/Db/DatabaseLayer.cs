using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A database layer class.</para>
    /// </summary> 
    [Using]
    public class DatabaseLayer : ElementLayer<IDatabase>, IDatabaseLayer
    {
        protected IApplicationFrame applicationFrame { get; private set; }

        protected sealed override void OnPrepare(Element _)
        {
            this.applicationFrame = this.GetRootElement<IApplicationFrame>();
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(DatabaseLayer _)
        {
        }

        /// <inheritdoc cref="IDatabaseLayer.GetDatabase{T}"/>
        public T GetDatabase<T>() where T : IDatabase
        {
            return this.GetElement<T>();
        }
        
        /// <inheritdoc cref="IDatabaseLayer.GetDatabases{T}"/>
        public IEnumerable<T> GetDatabases<T>() where T : IDatabase
        {
            return this.GetElements<T>();
        }
    }
}