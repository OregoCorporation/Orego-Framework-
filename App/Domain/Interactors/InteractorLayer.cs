using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Default implementation of domain layer.</para>
    ///     <para>This class type will added automatically by framework because
    ///     the class has attribute <see cref="Using"/>.</para>
    ///     <para><see cref="ApplicationFrame"/> uses this domain layer by default.</para>
    /// </summary>
    [Using]
    public class InteractorLayer : ElementLayer<IInteractor>, IInteractorLayer
    {
        protected IApplicationFrame applicationFrame { get; private set; }

        protected IClientLayer clientLayer { get; private set; }

        protected IRepositoryLayer repositoryLayer { get; private set; }

        protected sealed override void OnPrepare(Element _)
        {
            this.applicationFrame = this.GetRootElement<IApplicationFrame>();
            this.clientLayer = this.applicationFrame.clientLayer;
            this.repositoryLayer = this.applicationFrame.repositoryLayer;
            this.OnPrepare(this);
        }

        protected virtual void OnPrepare(InteractorLayer _)
        {
        }

        public T GetInteractor<T>() where T : IInteractor
        {
            return this.GetElement<T>();
        }

        public IEnumerable<T> GetInteractors<T>() where T : IInteractor
        {
            return this.GetElements<T>();
        }
    }
}