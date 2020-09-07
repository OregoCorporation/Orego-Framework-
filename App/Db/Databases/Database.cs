using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Base abstract database implementation.</para>
    /// </summary>
    /// <typeparam name="T">Interface type of dao.</typeparam>
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

        public TDao GetDao<TDao>() where TDao : IDao
        {
            return this.GetElement<TDao>();
        }

        public IEnumerable<TDao> GetDaoSet<TDao>() where TDao : IDao
        {
            return this.GetElements<TDao>();
        }
    }
}