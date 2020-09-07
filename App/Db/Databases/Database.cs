using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A Base database class.</para>
    /// </summary>
    public abstract class Database<T> : ElementLayer<T>, IDatabase where T : IDao
    {
        protected IApplicationFrame applicationFrame { get; private set; }

        protected sealed override void OnPrepare(Element _)
        {
            this.applicationFrame = this.GetRootElement<IApplicationFrame>();
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(Database<T> _)
        {
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