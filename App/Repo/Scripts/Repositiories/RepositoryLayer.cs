using System.Collections.Generic;
using Elementary;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>A repository layer interface.</para>
    ///     <para>Keeps dictionary of unique repositories <see cref="IRepository"/>.</para>
    /// </summary>
    public interface IRepositoryLayer : IRepoElement
    {
        /// <summary>
        ///     <para>Returns a required repository of "T" type.</para>
        /// </summary>
        T GetRepository<T>() where T : IRepository;

        /// <summary>
        ///     <para>Returns a required repositories of "T" type.</para>
        /// </summary>
        IEnumerable<T> GetRepositories<T>() where T : IRepository;
    }
    
    /// <inheritdoc cref="IRepositoryLayer"/>
    [Using]
    public class RepositoryLayer : ElementLayer<IRepository>, IRepositoryLayer
    {
        /// <summary>
        ///     <para>An application frame reference.</para>
        /// </summary>
        protected IApplicationFrame Application { get; private set; }

        /// <summary>
        ///     <para>A database layer reference.</para>
        /// </summary>
        protected IDatabaseLayer DatabaseLayer { get; private set; }

        /// <summary>
        ///     <para>A client layer reference.</para>
        /// </summary>
        protected IClientLayer ClientLayer { get; private set; }

        protected override void OnPrepare()
        {
            base.OnPrepare();
            this.Application = this.GetRootElement<IApplicationFrame>();
            this.DatabaseLayer = this.Application.DatabaseLayer;
            this.ClientLayer = this.Application.ClientLayer;
        }

        /// <inheritdoc cref="IRepositoryLayer.GetRepository{T}"/>
        public T GetRepository<T>() where T : IRepository
        {
            return this.GetElement<T>();
        }

        /// <inheritdoc cref="IRepositoryLayer.GetRepositories{T}"/>
        public IEnumerable<T> GetRepositories<T>() where T : IRepository
        {
            return this.GetElements<T>();
        }
    }
}