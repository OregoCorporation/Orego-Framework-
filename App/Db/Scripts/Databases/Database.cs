using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Database interface.</para>
    ///     <para>Keeps dictionary of unique data access objects (DAO) <see cref="IDao"/>.</para>
    /// </summary>
    public interface IDatabase : IElement
    {
        /// <summary>
        ///     <para>Returns a required data access object of "T" type.</para>
        /// </summary>
        T GetDao<T>() where T : IDao;

        /// <summary>
        ///     <para>Returns a required data access objects of "T" type.</para>
        /// </summary>
        IEnumerable<T> GetDaoSet<T>() where T : IDao;
    }
    
    /// <summary>
    ///     <para>A Base database class.</para>
    /// </summary>
    /// <typeparam name="T">Keeps unique data access objects (DAO) of "T" type.</typeparam>
    public abstract class Database<T> : ElementLayer<T>, IDatabase where T : IDao
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame Application { get; private set; }
        
        protected IDatabaseLayer DatabaseLayer { get; private set; }

        protected override void OnPrepare()
        {
            base.OnPrepare();
            this.Application = this.GetRootElement<IApplicationFrame>();
            this.DatabaseLayer = this.Application.DatabaseLayer;
        }
        
        /// <inheritdoc cref="IDatabase.GetDao{T}"/>
        public TDao GetDao<TDao>() where TDao : IDao
        {
            return this.GetElement<TDao>();
        }

        /// <inheritdoc cref="IDatabase.GetDaoSet{T}"/>
        public IEnumerable<TDao> GetDaoSet<TDao>() where TDao : IDao
        {
            return this.GetElements<TDao>();
        }
    }
}