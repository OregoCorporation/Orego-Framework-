using System.Collections;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Links user data blocks in repositories that depends of each other.</para>
    /// </summary>
    public interface IRepositoryLinker : IRepoElement
    {
        /// <summary>
        ///     <para>Links repositories asynchronously.</para>
        /// </summary>
        IEnumerator ResolveDataInRepositories();
    }
}