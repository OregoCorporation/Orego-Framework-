using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>An interactor layer class.</para>
    /// </summary>
    [Using]
    public class InteractorLayer : ElementLayer<IInteractor>, IInteractorLayer
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame applicationFrame { get; private set; }

        /// <summary>
        ///     <para>A client layer reference.</para>
        /// </summary>
        protected IClientLayer clientLayer { get; private set; }

        /// <summary>
        ///     <para>A repository layer reference.</para>
        /// </summary>
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

        /// <inheritdoc cref="IInteractorLayer.GetInteractor{T}"/>
        public T GetInteractor<T>() where T : IInteractor
        {
            return this.GetElement<T>();
        }
        
        /// <inheritdoc cref="IInteractorLayer.GetInteractors{T}"/>
        public IEnumerable<T> GetInteractors<T>() where T : IInteractor
        {
            return this.GetElements<T>();
        }
    }
}