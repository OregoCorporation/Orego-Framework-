using System.Collections;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Check for updates user data.</para>
    /// </summary>
    public interface IUpdateDataSystem : IRepoElement
    {
        /// <summary>
        ///     <para>Updates data in repository layer asynchronously.</para>
        /// </summary>
        IEnumerator CheckForUpdates();
    }
}