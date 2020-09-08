using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Default implementation of repository layer.</para>
    ///     <para>This class type will added automatically by framework because
    ///     the class has attribute <see cref="Using"/>.</para>
    ///     <para><see cref="ApplicationFrame"/> uses this repository layer by default.</para>
    /// </summary>
    [Using]
    public class RepositoryLayer : ElementLayer<IRepository>, IRepositoryLayer
    {
        protected IApplicationFrame applicationFrame { get; private set; }

        protected IDatabaseLayer databaseLayer { get; private set; }

        protected IClientLayer clientLayer { get; private set; }

        protected sealed override void OnPrepare(Element _)
        {
            this.applicationFrame = this.GetRootElement<IApplicationFrame>();
            this.databaseLayer = this.applicationFrame.databaseLayer;
            this.clientLayer = this.applicationFrame.clientLayer;
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(RepositoryLayer _)
        {
        }


        public T GetRepository<T>() where T : IRepository
        {
            return this.GetElement<T>();
        }

        public IEnumerable<T> GetRepositories<T>() where T : IRepository
        {
            return this.GetElements<T>();
        }
    }
}