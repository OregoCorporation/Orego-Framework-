using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A database layer interface.</para>
    ///     <para>Keeps dictionary of unique databases <see cref="IDatabase"/>.</para>
    /// </summary>
    public interface IDatabaseLayer : IElement
    {
        /// <summary>
        ///     <para>Returns a required database of "T" type.</para>
        /// </summary>
        T GetDatabase<T>() where T : IDatabase;
        
        /// <summary>
        ///     <para>Returns required databases of "T" type.</para>
        /// </summary>
        IEnumerable<T> GetDatabases<T>() where T : IDatabase;
    }
    
    /// <summary>
    ///     <para>A database layer class.</para>
    /// </summary>
    [Using]
    public class DatabaseLayer : ElementLayer<IDatabase>, IDatabaseLayer
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame Application { get; private set; }
        
        protected override void OnPrepare()
        {
            base.OnPrepare();
            this.Application = this.GetRootElement<IApplicationFrame>();
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