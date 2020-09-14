using System.Collections;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Checks and updates a user data.</para>
    /// </summary>
    public interface IVersionUpdater : IRepoElement
    {
        /// <summary>
        ///     <para>Checks and updates a user data asynchronously.</para>
        /// </summary>
        IEnumerator CheckForUpdates();
    }
}