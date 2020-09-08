using System.Collections.Generic;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Repository layer interface.</para>
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
}