using System.Collections;
using OregoFramework.Util;

namespace OregoFramework.App
{
    /// <inheritdoc cref="IUpdateDataHandler"/>
    public abstract class UpdateDataHandler : RepoElement, IUpdateDataHandler
    {
        /// <inheritdoc cref="IUpdateDataHandler.CheckForUpdates"/>
        public abstract IEnumerator CheckForUpdates(Reference<bool> isUpdated);
    }
}