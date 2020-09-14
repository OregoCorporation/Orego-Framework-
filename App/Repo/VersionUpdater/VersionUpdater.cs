using System.Collections;

namespace OregoFramework.App
{
    /// <inheritdoc cref="IVersionUpdater"/>
    public abstract class VersionUpdater : RepoElement, IVersionUpdater
    {
        /// <inheritdoc cref="IVersionUpdater.CheckForUpdates"/>
        public abstract IEnumerator CheckForUpdates();
    }
}