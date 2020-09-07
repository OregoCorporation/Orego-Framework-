using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Default implementation of database layer.</para>
    ///     <para>This class type will added automatically by framework because
    ///     the class has attribute <see cref="Using"/>.</para>
    ///     <para><see cref="ApplicationFrame"/> uses this database layer by default.</para>
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

        public T GetDatabase<T>() where T : IDatabase
        {
            return this.GetElement<T>();
        }

        public IEnumerable<T> GetDatabases<T>() where T : IDatabase
        {
            return this.GetElements<T>();
        }
    }
}