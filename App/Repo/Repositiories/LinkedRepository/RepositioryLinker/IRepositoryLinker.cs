using System.Collections;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Links user data blocks in repositories that depends of each other.</para>
    /// </summary>
    public interface IRepositoryLinker : IRepoElement
    {
        /// <summary>
        ///     <para>Links data in repository layer asynchronously.</para>
        /// </summary>
        IEnumerator LinkData();
    }
}