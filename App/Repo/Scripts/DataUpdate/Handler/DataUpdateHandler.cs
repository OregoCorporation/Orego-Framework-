using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <summary>
    ///     <para>Checks and updates a user data.</para>
    /// </summary>
    public interface IDataUpdateHandler : IRepoElement
    {
        /// <summary>
        ///     <para>Checks and updates data asynchronously.</para>
        /// </summary>
        /// 
        /// <param name="isUpdated">Boolean reference. Put a boolean result into the reference.</param>
        /// <returns>Is data updated or not.</returns>
        IEnumerator CheckForUpdates(Reference<bool> isUpdated = null);
    }
    
    /// <inheritdoc cref="IDataUpdateHandler"/>
    public abstract class DataUpdateHandler : RepoElement, IDataUpdateHandler
    {
        /// <inheritdoc cref="IDataUpdateHandler.CheckForUpdates"/>
        public abstract IEnumerator CheckForUpdates(Reference<bool> isUpdated = null);
    }
}