using System.Collections;

namespace OregoFramework.App
{
    public interface IVersionUpdater : IRepoElement
    {
        IEnumerator CheckForUpdates();
    }
}