using System.Collections;

namespace OregoFramework.App
{
    public abstract class VersionUpdater : RepoElement, IVersionUpdater
    {
        public abstract IEnumerator CheckForUpdates();
    }
}