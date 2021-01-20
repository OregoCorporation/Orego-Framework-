using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Checks and updates a user data.</para>
    /// </summary>
    public interface IUpdateDataHandler : IRepoElement
    {
        /// <summary>
        ///     <para>Checks and updates data asynchronously.</para>
        /// </summary>
        /// 
        /// <param name="isUpdated">Boolean reference. Put a boolean result into the reference.</param>
        /// <returns>Is data updated or not.</returns>
        IEnumerator CheckForUpdates(Reference<bool> isUpdated = null);
    }
    
    /// <inheritdoc cref="IUpdateDataHandler"/>
    public abstract class UpdateDataHandler : RepoElement, IUpdateDataHandler
    {
        /// <inheritdoc cref="IUpdateDataHandler.CheckForUpdates"/>
        public abstract IEnumerator CheckForUpdates(Reference<bool> isUpdated = null);
    }
}