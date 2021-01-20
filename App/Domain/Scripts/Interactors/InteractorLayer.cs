using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>An interactor layer interface.</para>
    ///     <para>Keeps dictionary of unique interactors <see cref="IInteractor"/>.</para>
    /// </summary>
    public interface IInteractorLayer : IDomainElement
    {
        /// <summary>
        ///     <para>Returns a required interactor of "T" type.</para>
        /// </summary>
        T GetInteractor<T>() where T : IInteractor;

        /// <summary>
        ///     <para>Returns a required interactors of "T" type.</para>
        /// </summary>
        IEnumerable<T> GetInteractors<T>() where T : IInteractor;
    }
    
    /// <summary>
    ///     <para>An interactor layer class.</para>
    /// </summary>
    [Using]
    public class InteractorLayer : ElementLayer<IInteractor>, IInteractorLayer
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame Application { get; private set; }

        /// <summary>
        ///     <para>A client layer reference.</para>
        /// </summary>
        protected IClientLayer ClientLayer { get; private set; }

        /// <summary>
        ///     <para>A repository layer reference.</para>
        /// </summary>
        protected IRepositoryLayer RepositoryLayer { get; private set; }

        protected override void OnPrepare()
        {
            base.OnPrepare();
            this.Application = this.GetRootElement<IApplicationFrame>();
            this.ClientLayer = this.Application.ClientLayer;
            this.RepositoryLayer = this.Application.RepositoryLayer;
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