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
        protected IApplication application { get; private set; }

        public override void OnPrepare()
        {
            base.OnPrepare();
            this.application = this.GetRootElement<IApplication>();
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